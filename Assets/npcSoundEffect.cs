using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcSoundEffect : MonoBehaviour {
    public AudioClip run;

    AudioSource audio;

    void Awake () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void play_run() {
        //audio.Stop();
        if (!audio.isPlaying) {
            audio.PlayOneShot(run, 1.0f);
            //AudioSource.PlayClipAtPoint(run,transform.position);
            
        }

        audio.loop = true;
    }

    public void stop() {
        //audio.Stop();
        if (audio.isPlaying) {
            audio.loop = false;
        }
       
    }

}
