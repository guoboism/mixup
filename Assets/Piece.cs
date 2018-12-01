using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BColor {
    NONE = -1,
    Yellow = 2,
    Cyan = 3,
    Teal = 4,
    Red = 5,
    Green = 6,
    Blue = 7,
    Black = 8
}

public struct Block {
    public BColor bColor;
    public int times;

    public Block(BColor bc_) {
        bColor = bc_;
        times = 1;
    }
}

public class Piece {

    public Dictionary<Vector2Int, Block> blocks = new Dictionary<Vector2Int, Block>();
    public List<int> x_indexes = new List<int>();//which vertial lines contains any block
    public List<int> y_indexes = new List<int>();//which horizontal lines contains any block
    public Vector2Int min = new Vector2Int();
    public Vector2Int max = new Vector2Int();

    public void AddB(Vector2Int p) {
        blocks.Add(p, new Block(BColor.NONE));
        if (!x_indexes.Contains(p.x)) x_indexes.Add(p.x);
        if (!y_indexes.Contains(p.y)) y_indexes.Add(p.y);
        if (p.x < min.x) { min.x = p.x; }
        if (p.y < min.y) { min.y = p.y; }
        if (p.x > max.x) { max.x = p.x; }
        if (p.y > max.y) { max.y = p.y; }
    }

    public void AddB(Vector2Int p, Block b) {
        blocks.Add(p, b);
        if (!x_indexes.Contains(p.x)) x_indexes.Add(p.x);
        if (!y_indexes.Contains(p.y)) y_indexes.Add(p.y);
        if (p.x < min.x) { min.x = p.x; }
        if (p.y < min.y) { min.y = p.y; }
        if (p.x > max.x) { max.x = p.x; }
        if (p.y > max.y) { max.y = p.y; }
    }

    public void RemoveB(Vector2Int p) {
        blocks.Remove(p);
    }

    public void SetBC(Vector2Int p, Block b) {
        blocks[p] = b;
    }

    public bool hasPiece(Vector2Int v2) {
        return blocks.ContainsKey(v2);
    }

    public void MergeWith(Piece other, Vector2Int offset) {
        foreach (Vector2Int key in other.blocks.Keys) {
            if (blocks.ContainsKey(key + offset)) {
                Block res = AddBC(blocks[key + offset], other.blocks[key]);
                SetBC(key + offset, res);
            } else {
                AddB(key + offset, other.blocks[key]);
            }
        }
    }

    Block AddBC(Block b1, Block b2) {

        BColor bc1 = b1.bColor;
        BColor bc2 = b2.bColor;
        Block res = b1;

        if (bc1 == bc2) {
            b1.times++;
            return b1;
        }

        int big;
        int small;
        Block block_big;
        if ((int)bc1 > (int)bc2) {
            big = (int)bc1;
            small = (int)bc2;
            block_big = b1;
        } else {
            big = (int)bc2;
            small = (int)bc1;
            block_big = b2;
        }

        if (big > 4 && small <= 4) {
            res.bColor = BColor.Black;
            if (big == 5 && (small == 2 || small == 3)) { res.times++; res.bColor = (BColor)big; }
            if (big == 6 && (small == 2 || small == 4)) { res.times++; res.bColor = (BColor)big; }
            if (big == 7 && (small == 4 || small == 3)) { res.times++; res.bColor = (BColor)big; }

        } else {
            int sum = big + small;
            if (sum > 7) {
                sum = 8;//black
            }
            res.times = 1;
            res.bColor = (BColor)sum;
        }

        return res;
    }

    public void Rotate() {

        List<Vector2Int> keys = new List<Vector2Int>();
        List<Block> bs = new List<Block>();
        keys.AddRange(blocks.Keys);

        foreach (Vector2Int k in keys) {
            bs.Add(blocks[k]); 
        }

        blocks.Clear();
        for (int i = 0; i < keys.Count; i++) {
            Vector2Int temp = keys[i];
            keys[i] = new Vector2Int(-temp.y, temp.x);
            blocks.Add(keys[i], bs[i]);
        }
    }
    public void RotateBack() {

        List<Vector2Int> keys = new List<Vector2Int>();
        List<Block> bs = new List<Block>();
        keys.AddRange(blocks.Keys);

        foreach (Vector2Int k in keys) {
            bs.Add(blocks[k]);
        }

        blocks.Clear();
        for (int i = 0; i < keys.Count; i++) {
            Vector2Int temp = keys[i];
            keys[i] = new Vector2Int(temp.y, -temp.x);
            blocks.Add(keys[i], bs[i]);
        }
    }
}

public class PieceLib {

    public static Dictionary<string, Piece> dic;
    public static List<string> keys;
    public static List<BColor> colors;
    public static List<int> nums;

    public static void Init() {
        dic = new Dictionary<string, Piece>();
        keys = new List<string>();

        AddPieceType(
            new Vector2Int[] {
                new Vector2Int(0, 0)
            }, "s"
        );

        AddPieceType(
           new Vector2Int[] {
                new Vector2Int(0, 0)
           }, "s2"
       );

        AddPieceType(
          new Vector2Int[] {
                new Vector2Int(0, 0)
          }, "s3"
        );

        colors = new List<BColor>();
        colors.Add(BColor.Cyan);
        colors.Add(BColor.Teal);

        nums = new List<int>();
        nums.Add(1);
        nums.Add(1);
        nums.Add(1);
        nums.Add(1);
        nums.Add(1);
    }

    public static void AddToLevel(int level) {
        switch (level) {
            case 1: 
                AddPieceType(
                   new Vector2Int[] {
                        new Vector2Int(0, 0),
                        new Vector2Int(1, 0)
                   }, "2h"
               );
                break;
            case 2:
                colors.Add(BColor.Yellow);
                break;
            case 3:
                AddPieceType(
                  new Vector2Int[] {
                        new Vector2Int(0, 0),
                        new Vector2Int(0, 1)
                  }, "2v"
                );
                break;
            case 4:
                colors.Add(BColor.Blue);
                break;
            case 5:
                colors.Add(BColor.Green);
                AddPieceType(
                 new Vector2Int[] {
                        new Vector2Int(0, 0),
                        new Vector2Int(0, 1)
                 }, "2v2"
               );
                break;
            case 6:
                AddPieceType(
                 new Vector2Int[] {
                        new Vector2Int(0, 0),
                        new Vector2Int(0, 1),
                        new Vector2Int(1, 0)
                 }, "3L1"
               ); 
                break;
            case 7:
                colors.Add(BColor.Red);
                break;
            case 8:
                AddPieceType(
                   new Vector2Int[] {
                            new Vector2Int(0, 0),
                            new Vector2Int(0, 1),
                            new Vector2Int(-1, 0)
                   }, "3L2"
                 );

                break;
            case 9:
                nums.Add(1);
                nums.Add(1);
                nums.Add(2);
                break;
            case 10:
                
                AddPieceType(
                new Vector2Int[] {
                         new Vector2Int(0, 0),
                            new Vector2Int(0, 1),
                            new Vector2Int(-1, 0)
                }, "3L3"
                );
                break;
            case 11:
                nums.Add(2);
                break;
            case 12: 
                AddPieceType(
                   new Vector2Int[] {
                            new Vector2Int(0, 0),
                            new Vector2Int(0, 1),
                            new Vector2Int(-1, 0),
                            new Vector2Int(-1, 1)
                   }, "4"
                 );
                break;
            case 13:
                nums.Add(2);
                break;
            case 14:
                AddPieceType(
                new Vector2Int[] {
                         new Vector2Int(0, 0),
                            new Vector2Int(0, 1),
                            new Vector2Int(-1, 0)
                }, "3L4"
                );
                break;
            case 15:
                colors.Add(BColor.Blue);
                colors.Add(BColor.Red);
                colors.Add(BColor.Green);
                break;
        }
    }

    static void AddPieceType(Vector2Int[] poses, string name) {
        Piece threeL1 = new Piece();
        foreach (Vector2Int v in poses) {
            threeL1.AddB(v);
        }
        dic.Add(name, threeL1);
        keys.Add(name);
    }

    public static Piece GenNewPiece() {
        string key = keys[Random.Range(0, keys.Count)];
        Piece cur_piece = dic[key];

        List<Vector2Int> list = new List<Vector2Int>();
        list.AddRange(cur_piece.blocks.Keys);

        foreach (Vector2Int i in list) {
            Block b = new Block();
            b.bColor = (colors[Random.Range(0, colors.Count)]);
            b.times = (nums[Random.Range(0, nums.Count)]);
            cur_piece.blocks[i] = b;
        }

        return cur_piece;
    }
}
