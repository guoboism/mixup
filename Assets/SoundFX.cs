using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour {


    public AudioSource ass;

    public AudioClip ac_placeDown;
    public AudioClip ac_negative;

    public AudioClip ac_match;
    public AudioClip ac_match_all;
    public AudioClip ac_grow;

    public AudioClip ac_gameover;
    public AudioClip ac_gameover_all;

    public AudioClip ac_gamestart;

    public AudioClip ac_bomb;
    public AudioClip ac_thunder;
    public AudioClip ac_countdown;


    AudioClip last;
    int cout = 0;
    public void Play(AudioClip ac) {
        if (ac == last) {
            cout++;
            if (cout > 6) cout = 6;
            ass.pitch = 1 + 0.5f * cout; 
        } else {
            cout = 0;
            ass.pitch = 1;
        }

        ass.PlayOneShot(ac);
        last = ac;
    }
}
