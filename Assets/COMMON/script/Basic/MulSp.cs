using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MulSp : MonoBehaviour
{

    public List<Sprite> sprites;
    SpriteRenderer sr;
    Image img;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //from 0
    public void setSprite(int ind)
    {

        if (ind > sprites.Count - 1)
        {
            return;
        }
        gameObject.SetActive(true);
        sr = GetComponent<SpriteRenderer>();
        if (sr)
            sr.sprite = sprites[ind];
        img = GetComponent<Image>();
        if (img)
            img.sprite = sprites[ind];
    }
}
