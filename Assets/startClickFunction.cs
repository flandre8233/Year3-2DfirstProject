﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startClickFunction : MonoBehaviour {

    public Animator buttonAni;

    bool once = false;


    public void set() {
        if (!once) {
            once = true;
            soundEffectManager.staticSoundEffect.play_startSceneClickButton();
            buttonAni.SetTrigger("click");
        }

    }

}
