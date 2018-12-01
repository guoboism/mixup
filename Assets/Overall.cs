using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Overall : MonoBehaviour
{

    //singleton
    public static Overall self;

    //entites
    public Board board;
    public PieceRender cur_piece_render;
    public PieceRender main_piece_render;
    public PieceRender mouse_piece;

    //UI 
    public Image bar;
    public CanvasGroup cg_game;
    public CanvasGroup cg_gameover;
    public GameObject bar_group; 

    //data
    public Piece cur_piece;
    public Piece main_piece;
    public int match_count;
    public int level;
    public float time;
    public float beepped_time;
    public float time_max;
     
    public PSScript pss;
    public SoundFX sfx;
    public AudioSource ass_bgm;

    //const
    public const float CELL = 0.8f;

    public bool busy = false;
 
    // Use this for initialization
    void Awake()
    {
        self = this;
        PieceLib.Init(); 
        StartLevel();
    }

    public void StartLevel()
    {
        board.Init();
        ass_bgm.DOFade(0, 0);
        ass_bgm.DOFade(1, 1);


        sfx.Play(sfx.ac_gamestart);
         
        match_count = 0;
        level = 0;
        time_max = 15;
        beepped_time = 5;
        time = time_max;


        main_piece = new Piece();
        main_piece_render.data = main_piece;

        GetNewPiece();
        UpdateCamera();
    }

    public void Restart() { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            StartCoroutine(GameOver());
        }

        float d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f) {
            cur_piece.Rotate();
            mouse_piece.Render();
        } else if (d < 0f) {
            cur_piece.RotateBack();
            mouse_piece.Render();
        }

        if (!busy) {
            if (Input.GetMouseButtonDown(0)) {
                //try to place piece
                TryToPlace();
            }
             
            time -= Time.deltaTime;
            time = Mathf.Max(0, time);
            if (time < beepped_time) {
                sfx.Play(sfx.ac_countdown);
                bar_group.transform.DOScale(1.2f, 0);
                bar_group.transform.DOScale(1f, 0.5f); 
                beepped_time--;
            }

            if (time <= 0) {
                time = time_max;
                beepped_time = 5;

                //do it
                Vector2Int? res = board.FindEmpty(main_piece);
                if (res == null) {
                    //should game over already
                    StartCoroutine(GameOver());
                } else {
                    sfx.Play(sfx.ac_thunder);
                    sfx.Play(sfx.ac_bomb);
                    Camera.main.transform.DOShakePosition(0.1f, 0.5f);
                    pss.Play((Vector2)res * CELL, Color.white);

                    board.Remove((Vector2Int)res);

                    Vector2Int? res2 = board.FindEmpty(main_piece);
                    if (res2 == null) {
                        //should game over already
                        StartCoroutine(GameOver());
                    }
                }
            }

            bar.fillAmount = time / time_max;
        }
 
        if (cur_piece != null)
        {
            Vector3 mp = Input.mousePosition;
            mp.z = 10;
            Vector3 wp = Camera.main.ScreenToWorldPoint(mp);
            //mouse_piece.transform.position = wp;
            mouse_piece.transform.position =
            Vector3.Lerp(mouse_piece.transform.position, wp, 0.2f);
        }
    }

    public void LevelUp() {

        level++;
        PieceLib.AddToLevel(level);
        time_max *= 0.9f;
        if (time_max < 5) time_max = 5;
    }

    public void TryToPlace()
    {

        Vector3 mp = Input.mousePosition;
        mp.z = 10;
        Vector3 wp = Camera.main.ScreenToWorldPoint(mp);

        //check each block for the cur_piece with the current wp
        Vector2Int wp_int = new Vector2Int(
            Mathf.RoundToInt(wp.x / CELL),
            Mathf.RoundToInt(wp.y / CELL)
        );

        List<Vector2Int> list = new List<Vector2Int>();
        list.AddRange(cur_piece.blocks.Keys);
        foreach (Vector2Int i in list)
        {
            Vector2Int final_int = wp_int + i;
            //Debug.Log("final_int" + final_int);
            if (board.hasPiece(final_int) == false)
            {
                sfx.Play(sfx.ac_negative);
                return;
            }

            if (main_piece.hasPiece(final_int))
            {
                if (main_piece.blocks[final_int].bColor == BColor.Black)
                {
                    main_piece_render.Ani_CannotPut(final_int);
                    sfx.Play(sfx.ac_negative);
                    return;
                }
            }
        }

        //ok let put it
        sfx.Play(sfx.ac_placeDown);
        time = time_max;
        beepped_time = 5;
        main_piece.MergeWith(cur_piece, wp_int);
        main_piece_render.Render();

        GetNewPiece(); 
        StartCoroutine(AfterPlaceCheck());

    }

    public void GetNewPiece()
    {
        //Debug.Log("GetNewPiece");
        cur_piece = PieceLib.GenNewPiece();
        //Debug.Log(cur_piece == null);

        cur_piece_render.data = cur_piece;
        cur_piece_render.Render();

        mouse_piece.transform.position = Vector2.zero;
        mouse_piece.data = cur_piece;
        mouse_piece.Render();
    }

    public IEnumerator AfterPlaceCheck()
    {
        //any vertical line that are same 3 or more times
        //any hozontail line that are same 3 ore more times

        list_match.Clear();

        //Debug.Log("AfterPlaceCheck");
        foreach (int x in main_piece.x_indexes)
        {
            //Debug.Log("CheckLineX" + x);
            CheckLineX(x);
        }

        foreach (int y in main_piece.y_indexes)
        {
            //Debug.Log("CheckLineY" + y);
            CheckLineY(y);
        }

        if (list_match.Count > 0)
        {

            busy = true;

            yield return new WaitForSeconds(0.3f);
             
            //got sth
            int money_got = 0;
            sfx.Play(sfx.ac_match_all);
            foreach (Vector2Int v in list_match)
            {

                sfx.Play(sfx.ac_match);
                main_piece_render.Ani_Matech(v);
                yield return new WaitForSeconds(0.1f);

                main_piece.RemoveB(v);
                money_got++; 
            }

            yield return new WaitForSeconds(0.6f);

            match_count++;
            if (match_count > level ) {
                match_count = 0;
                LevelUp();

                board.Grow();
                sfx.Play(sfx.ac_grow);
                UpdateCamera();
            }
        }

        //check explode
        List<Vector2Int> list = new List<Vector2Int>();
        list.AddRange(main_piece.blocks.Keys);

        foreach (Vector2Int p in list)
        {
            Block b = main_piece.blocks[p];
            if (b.times >= 4)
            {

                busy = true;

                main_piece_render.Ani_BeforeBoom(p);
                sfx.Play(sfx.ac_countdown);

                yield return new WaitForSeconds(0.5f);

                sfx.Play(sfx.ac_bomb);
                Camera.main.transform.DOShakePosition(0.1f, 0.5f);
                pss.Play((Vector2)p * CELL, Color.white);

                main_piece.RemoveB(p);
                board.Remove(p);
            }
        }

        busy = false; 
        main_piece_render.Render();

        //yield return null;
        //check for game over
        Vector2Int? res = board.FindEmpty(main_piece);
        if (res == null) {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver() {

        mouse_piece.gameObject.SetActive(false);

        ass_bgm.DOFade(0, 1);


        //busy
        busy = true;
        Debug.Log("Game Over");

        List<Vector2Int> list = new List<Vector2Int>();
        list.AddRange(main_piece.blocks.Keys);

        foreach (Vector2Int p in list) {

            Block b = main_piece.blocks[p];
            b.bColor = BColor.Black;
            main_piece.SetBC(p, b);

            sfx.Play(sfx.ac_gameover);

            main_piece_render.Ani_GameOver(p);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.2f);

        //ui fade out
        //zoom over
        cg_game.DOFade(0, 1);
        cg_game.interactable = false;
        UpdateCamera_Overall();

        yield return new WaitForSeconds(1);

        //show game over
        //show text
        sfx.Play(sfx.ac_gameover_all);

        cg_gameover.interactable = true;
        cg_gameover.DOFade(0, 0);
        cg_gameover.DOFade(1, 1);
       
        //show retry button

    }
        

    void CheckLineX(int x)
    {
        BColor curbc = BColor.NONE;
        int series = 0;
        for (int y = main_piece.min.y; y <= main_piece.max.y; y++)
        {
            Vector2Int vp = new Vector2Int(x, y);
            if (main_piece.hasPiece(vp) == false)
            {

                //now we check if we got a seies
                if (curbc != BColor.NONE && series >= 3)
                {
                    if (curbc != BColor.Black)
                    {
                        //mark those blocks
                        for (int k = 0; k < series; k++)
                        {
                            RegForMatch(new Vector2Int(x, y - k - 1));
                        }
                    }
                }

                curbc = BColor.NONE;
                series = 0;
                continue;
            }

            if (main_piece.blocks[vp].bColor == curbc)
            {
                series++;

            }
            else
            {

                //now we check if we got a seies
                if (curbc != BColor.NONE && series >= 3)
                {
                    if (curbc != BColor.Black)
                    {
                        //mark those blocks
                        for (int k = 0; k < series; k++)
                        {
                            RegForMatch(new Vector2Int(x, y - k - 1));
                        }
                    }
                }

                curbc = main_piece.blocks[vp].bColor;
                series = 1;
            }
        }

        //now we check if we got a seies
        if (curbc != BColor.NONE && series >= 3)
        {
            if (curbc != BColor.Black)
            {
                //mark those blocks
                for (int k = 0; k < series; k++)
                {
                    RegForMatch(new Vector2Int(x, main_piece.max.y - k));
                }
            }
        }
    }

    void CheckLineY(int y)
    {

        BColor curbc = BColor.NONE;
        int series = 0;
        for (int x = main_piece.min.x; x <= main_piece.max.x; x++)
        {
            Vector2Int vp = new Vector2Int(x, y);
            if (main_piece.hasPiece(vp) == false)
            {

                //now we check if we got a seies
                if (curbc != BColor.NONE && series >= 3)
                {
                    if (curbc != BColor.Black)
                    {
                        //mark those blocks
                        for (int k = 0; k < series; k++)
                        {
                            RegForMatch(new Vector2Int(x - k - 1, y));
                        }
                    }
                }

                curbc = BColor.NONE;
                series = 0;
                continue;
            }

            if (main_piece.blocks[vp].bColor == curbc)
            {
                series++;
            }
            else
            {
                //now we check if we got a seies
                if (curbc != BColor.NONE && series >= 3)
                {
                    if (curbc != BColor.Black)
                    {
                        //mark those blocks
                        for (int k = 0; k < series; k++)
                        {
                            RegForMatch(new Vector2Int(x - k - 1, y));
                        }
                    }
                }

                curbc = main_piece.blocks[vp].bColor;
                series = 1;
            }
        }

        //now we check if we got a seies
        if (curbc != BColor.NONE && series >= 3)
        {
            if (curbc != BColor.Black)
            {
                //mark those blocks
                for (int k = 0; k < series; k++)
                {
                    RegForMatch(new Vector2Int(main_piece.max.x - k, y));
                }
            }
        }
    }

    List<Vector2Int> list_match = new List<Vector2Int>();
    void RegForMatch(Vector2Int vint)
    {
        //Debug.Log("got" + vint);
        if (list_match.Contains(vint) == false)
            list_match.Add(vint);
    }

    public void UpdateCamera()
    {

        //center
        Vector3 center = new Vector3(
            (board.min.x + board.max.x) * 0.5f,
            (board.min.y + board.max.y) * 0.5f,
            -10
        );

        //size
        float h = (board.max.y - board.min.y + 1 + 2) * CELL * 0.5f + 2;

        //Camera.main.transform.position = center;
        //Camera.main.orthographicSize = h;

        Camera.main.transform.DOMove(center, 0.5f);
        Camera.main.DOOrthoSize(h, 0.5f);

    }

    public void UpdateCamera_Overall() {


        //size
        float h = Camera.main.orthographicSize + 2;

        //Camera.main.transform.position = center;
        //Camera.main.orthographicSize = h;
         
        Camera.main.DOOrthoSize(h, 0.5f);

    }
}
