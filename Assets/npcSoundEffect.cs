using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcSoundEffect : MonoBehaviour {
    public AudioClip run;

    AudioSource audio;
    npcClass npcclass;

    void Awake () {
        npcclass = GetComponentInParent<npcClass>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void play_run() {
        //audio.Stop();
        if (!audio.isPlaying) {
            if (npcclass.TypeP == npcClass.Type.contorl) {
                audio.PlayOneShot(run, 0.7f);
            }
            else {
               // audio.PlayOneShot(run,0.7f);
            }
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
