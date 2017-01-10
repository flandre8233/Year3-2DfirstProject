using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMusicScript : MonoBehaviour {
    public AudioClip lv1;
    public AudioClip lv2;
    public AudioClip dead;
    public AudioClip titleScreen;
    public AudioClip clear;
    public AudioClip selectLevel;


    AudioSource audio;


    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(lv1, 0.7F);
    }
	
	// Update is called once per frame
	void Update () {
    }
}
