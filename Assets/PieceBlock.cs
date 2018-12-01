using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PieceBlock : MonoBehaviour
{

    public MulColor mulColor;
    public MulSp icon;
    Color c;

    public void Start() {

        icon.transform.DOScale(0.8f,0.5f).SetLoops(-1, LoopType.Yoyo);

    }

    public void Render(Block b)
    {

        mulColor.setColor((int)b.bColor - 2);
        c = mulColor.colors[(int)b.bColor - 2];

        icon.gameObject.SetActive(false);
        if (b.bColor == BColor.Black)
        {
            icon.setSprite(3);
        }
        else
        {
            if (b.times == 2) icon.setSprite(0);
            else if (b.times == 3) icon.setSprite(1);
            else if (b.times == 4) icon.setSprite(2);
        }
    }

    public void Ani_Match() {
        transform.DOScale(1.2f, 0.3f);
        transform.DOScale(0f, 0.1f).SetDelay(0.3f);

    

        Overall.self.pss.Play(transform.position, c);
    }

    public void Ani_BeforeBoom() {
        transform.DOShakeScale(0.5f, 0.3f,50);
    }

    public void Ani_CannotPut() {
        transform.DOShakeScale(0.3f, 0.1f, 10);
    }

    public void Ani_GameOver() {
        mulColor.setColor((int)BColor.Black - 2);
        transform.DOShakeScale(0.5f, 0.3f, 50);
        icon.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
    }
}
