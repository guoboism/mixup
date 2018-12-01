using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Menu : MonoBehaviour {

    public CanvasGroup cg1;
    public CanvasGroup cg2;
    public CanvasGroup cg3;

    bool ready = false;
    bool show_control = true;
    bool go_next = false;
    // Use this for initialization
    void Start () {
         
        StartCoroutine(proc());
         
	}

    IEnumerator proc() {

        cg1.DOFade(0, 0);
        cg2.DOFade(0, 0);
        cg3.DOFade(0, 0);

        yield return new WaitForSeconds(0.5f);

        cg1.DOFade(1, 1);

        yield return new WaitForSeconds(2.5f);

        cg1.DOFade(0, 1);
        cg2.DOFade(1, 1);

        yield return new WaitForSeconds(0.5f);

        ready = true;
    }

    IEnumerator proc2() {

        show_control = false;
        cg2.DOFade(0, 1);
        cg3.DOFade(1, 1);

        yield return new WaitForSeconds(0.5f);

        go_next = true;
    }

        // Update is called once per frame
        void Update () { 
        if (ready && Input.anyKeyDown ) {

            if (show_control) { 
                StartCoroutine(proc2()); 
            } else if(go_next) {
                SceneManager.LoadScene("Main");
            }
        } 
	}
}
