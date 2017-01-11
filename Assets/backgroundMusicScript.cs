using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMusicScript : MonoBehaviour {
    public static backgroundMusicScript staticBackground;

    public AudioClip lv1;
    public AudioClip lv2;
    public AudioClip dead;
    public AudioClip titleScreen;
    public AudioClip clear;
    public AudioClip selectLevel;


    AudioSource audio;


    // Use this for initialization
    void Awake () {
        if (staticBackground != null) {
            Destroy(gameObject);
        }
        else {
            staticBackground = this;
            DontDestroyOnLoad(gameObject);
        }



        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(titleScreen, 0.2f);






    }

    void OnLevelWasLoaded(int level) {
        audio.Stop();
        if (level == 0) {
            audio.PlayOneShot(titleScreen, 0.2f);
        }
        else if (level == 1) {
            audio.PlayOneShot(selectLevel, 0.2f);
        }
        else if (level >= 2 && level < 7) {
            audio.PlayOneShot(lv1, 0.2f);
        }
        else {
            audio.PlayOneShot(lv2, 0.2f);
        }

    }

    public void playClearLevel() {
        audio.Stop();
        audio.PlayOneShot(clear, 0.2f);
    }

    public void playOnDead() {
        audio.Stop();
        audio.PlayOneShot(dead, 0.2f);
    }

}
