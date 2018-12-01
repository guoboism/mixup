using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour {

    //state
    public int x;
    public int y;
    public bool selected = false;

    public virtual void SelectedChanged() { }

}
