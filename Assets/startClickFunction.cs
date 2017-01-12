using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startClickFunction : MonoBehaviour {

    public Animator buttonAni;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public void set() {
        soundEffectManager.staticSoundEffect.play_startSceneClickButton();
        buttonAni.SetTrigger("click");
    }

}
