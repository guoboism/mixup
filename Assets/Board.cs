using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{
    public GameObject tp_board;

    public Dictionary<Vector2Int, GameObject> blocks;

    public Vector2Int min = new Vector2Int();
    public Vector2Int max = new Vector2Int();


    // Use this for initialization
    public void Init()
    {
        blocks = new Dictionary<Vector2Int, GameObject>();

        AddPiece(new Vector2Int(-1, 0));
        AddPiece(new Vector2Int(0, 0));
        AddPiece(new Vector2Int(1, 0));

        AddPiece(new Vector2Int(-1, 1));
        AddPiece(new Vector2Int(0, 1));
        AddPiece(new Vector2Int(1, 1));

        AddPiece(new Vector2Int(-1, -1));
        AddPiece(new Vector2Int(0, -1));
        AddPiece(new Vector2Int(1, -1));
         
    }

    public void AddPiece(Vector2Int p)
    {
        tp_board.gameObject.SetActive(false);
        GameObject newGo = Instantiate<GameObject>(tp_board, transform); 
        newGo.transform.position = (Vector2)p * Overall.CELL; 

        newGo.transform.DOScale(0, 0);
        newGo.transform.DOScale(1, 0.5f);

        newGo.SetActive(true);
        blocks.Add(p, newGo);

        if (p.x < min.x) { min.x = p.x; }
        if (p.y < min.y) { min.y = p.y; }
        if (p.x > max.x) { max.x = p.x; }
        if (p.y > max.y) { max.y = p.y; }
    }

    public void Remove(Vector2Int p)
    {
        Destroy(blocks[p].gameObject);
        blocks.Remove(p);
    }
    public bool hasPiece(Vector2Int p)
    {
        return blocks.ContainsKey(p); 
    }


    public void Grow()
    {
        Vector2Int p = GetBorder();
        AddPiece(p);
    }

    public Vector2Int? FindEmpty(Piece mainP) {
        foreach (Vector2Int p in blocks.Keys) {
            if (mainP.blocks.ContainsKey(p)) {
                continue;
            } 
            return p;
        }
        return null;
    }

    Vector2Int GetBorder()
    {

        List<Vector2Int> keys = new List<Vector2Int>();
        keys.AddRange(blocks.Keys);

        Vector2Int[] dirs = new Vector2Int[4] {
            new Vector2Int(0,1),
            new Vector2Int(0,-1),
            new Vector2Int(1,0),
            new Vector2Int(-1,0),
         };

        while (true)
        {
            Vector2Int p = keys[Random.Range(0, keys.Count)];
            foreach (Vector2Int item in dirs)
            {
                if (blocks.ContainsKey(item + p) == false)
                {
                    //bingo!
                    return item + p;
                }
            }
            //sbreak;
        }

        //return new Vector2Int();
    }

}
