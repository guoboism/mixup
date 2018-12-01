using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectLevelSelect : MonoBehaviour {

    //USE ON THE SAME GAMEOBJECT WITH A GAME_SCRIPT_BASE


    //keycode 1=97 9=105

    GameScriptBase gameScript;

    public void Start() {
        gameScript = GetComponent<GameScriptBase>();
    }

    public void Update() {

        for (int i = 1; i <= 9; i++) {
            if (Input.GetKeyDown( i+"")) {
                print("sss"+(i - 1));
                gameScript.SwitchLevel(i-1);
            }
        }

    }


}
