using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour {

    bool open = false;
    int ind = 0;

    public MulSp msp;

    public void OnClk_Open_Close() {
        gameObject.SetActive(!open);
        open = !open; 
    }

    public void OnClk_Switch() {
        ind++;
        if (ind >= 6) ind = 0;
        msp.setSprite(ind);

    }
}
