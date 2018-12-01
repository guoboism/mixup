using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSScript : MonoBehaviour {

    public ParticleSystem ps;

    public void Play(Vector3 wp, Color c) {

        var m =  ps.main;
        m.startColor = c;
        ps.transform.position = wp;
        ps.Play();
    }

}
