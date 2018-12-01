using UnityEngine;
using System.Collections;

public struct Inctor2 {

    public static Inctor2 Zero = new Inctor2(0, 0);

    //
    public static Inctor2 UP = new Inctor2(0, -1);
    public static Inctor2 DOWN = new Inctor2(0, 1);
    public static Inctor2 LEFT = new Inctor2(-1, 0);
    public static Inctor2 RIGHT = new Inctor2(1, 0);
    public static Inctor2[] DIRS = new Inctor2[4] { UP, DOWN, LEFT, RIGHT };


    public int x;
    public int y;

    public Inctor2(int x_, int y_) {
        x = x_;
        y = y_;
    }

    public Vector2 toVector() {
        return new Vector2(x, y);
    }

    public Vector3 toVector3() {
        return new Vector3(x, y);
    }

    public static Vector2 operator +(Inctor2 lhs, Vector2 rhs) {
        Vector2 result = new Vector2();
        result.x = lhs.x + rhs.x;
        result.y = lhs.y + rhs.y;
        return result;
    }

    public static Vector2 operator -(Inctor2 lhs, Vector2 rhs) {
        Vector2 result = new Vector2();
        result.x = lhs.x - rhs.x;
        result.y = lhs.y - rhs.y;
        return result;
    }

    public static Inctor2 operator +(Inctor2 lhs, Inctor2 rhs) {
        Inctor2 result = new Inctor2();
        result.x = lhs.x + rhs.x;
        result.y = lhs.y + rhs.y;
        return result;
    }

    public static Inctor2 operator -(Inctor2 lhs, Inctor2 rhs) {
        Inctor2 result = new Inctor2();
        result.x = lhs.x - rhs.x;
        result.y = lhs.y - rhs.y;
        return result;
    }

    public static Inctor2 operator *(Inctor2 lhs, int rhs) {
        Inctor2 result = new Inctor2();
        result.x = lhs.x * rhs;
        result.y = lhs.y * rhs;
        return result;
    }

    public static Vector2 operator *(Inctor2 lhs, float rhs) {
        Vector2 result = new Vector2();
        result.x = lhs.x * rhs;
        result.y = lhs.y * rhs;
        return result;
    }

    public bool isZero() {
        return x == 0 && y == 0;
    }

    public static bool operator ==(Inctor2 lhs, Inctor2 rhs) {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(Inctor2 lhs, Inctor2 rhs) {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public override bool Equals(object obj) {
        return base.Equals(obj);
    }

    public override string ToString() {
        return "Inc ( " + x + ", " + y + ")";
    }

    public void Immornalize() {
        if (x != 0) x = x / Mathf.Abs(x);
        if (y != 0) y = y / Mathf.Abs(y);
    }

    public int GetZLen() {
        return Mathf.Abs(x) + Mathf.Abs(y);
    }

    public override int GetHashCode() {
        string str = x + "=" + y;
        return str.GetHashCode();
    }

    public bool IsPure() {
        return (x == 0 && y != 0) || (x != 0 && y == 0);
    }
}