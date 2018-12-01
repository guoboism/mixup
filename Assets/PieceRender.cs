using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceRender : MonoBehaviour
{

    public Piece data;

    public GameObject tp_piece;
    public Dictionary<Vector2Int, PieceBlock> pieces = new Dictionary<Vector2Int, PieceBlock>();

    public void Render()
    {
        //clear
        foreach (PieceBlock mc in pieces.Values)
        {
            Destroy(mc.gameObject);
        }
        pieces.Clear();

        foreach (Vector2Int pos in data.blocks.Keys)
        {
            Block b = data.blocks[pos];

            GameObject newGo = Instantiate<GameObject>(tp_piece.gameObject, transform);
            newGo.SetActive(true);
            newGo.transform.localPosition = (Vector2)pos * Overall.CELL;

            PieceBlock pb = newGo.GetComponent<PieceBlock>();
            pb.Render(b);
            pieces.Add(pos, pb);
        }
    }


    public void Ani_Matech(Vector2Int p) {
        PieceBlock pb = pieces[p];
        pb.Ani_Match();

    }

    public void Ani_BeforeBoom(Vector2Int p) {
        PieceBlock pb = pieces[p];
        pb.Ani_BeforeBoom();

    }

    public void Ani_CannotPut(Vector2Int p) {
        PieceBlock pb = pieces[p];
        pb.Ani_CannotPut();
    }

    public void Ani_GameOver(Vector2Int p) {
        PieceBlock pb = pieces[p];
        pb.Ani_GameOver();
    }
}
