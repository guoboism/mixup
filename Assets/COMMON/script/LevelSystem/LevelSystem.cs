using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour {
    //a single scene level system


    LevelBase[] levels;

	// Use this for initialization
	public void init () {

        GameObject levelContainer = GameObject.FindGameObjectWithTag("LEVEL_CONTAINER");
        if (levelContainer == null) return;

        levels = levelContainer.GetComponentsInChildren<LevelBase>(true);

        //hide all
        foreach (LevelBase l in levels) l.gameObject.SetActive(false);
	}

    public bool hasLevel(int indF0) {

        if (indF0 < 0 || indF0 >= levels.Length) {
            return false;
        }

        return true;
    }

    public LevelBase getLevel(int indF0) {
        if (hasLevel(indF0)) {
            return levels[indF0];
        }

        return null;
    }
   
}
