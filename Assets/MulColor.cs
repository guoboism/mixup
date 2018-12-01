using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MulColor : MonoBehaviour
{

    public List<Color> colors;
    SpriteRenderer sr;
    Image img;



    //from 0
    public void setColor(int ind)
    {

        if (ind > colors.Count - 1)
        {
            return;
        }
        sr = GetComponent<SpriteRenderer>();
        if (sr)
            sr.color = colors[ind];
        img = GetComponent<Image>();
        if (img)
            img.color = colors[ind];
    }
}
