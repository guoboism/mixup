using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScriptBase : MonoBehaviour {

    //public static GameScriptBase self;
    
    public LevelSystem level_system;

    public virtual void Awake() {
        Init();
    }

    public virtual void Init() {
    }

    public virtual void SwitchLevel(int ind) {

    }


    public virtual void LevelStart() {
        
    }

    public virtual void LevelFailed() { 
    
    }

    public virtual void LevelCompleted() {

    }

}
