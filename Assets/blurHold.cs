using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blurHold : MonoBehaviour {
    public bool follow = false;
    bool savePosition;

    bool setLocalPosition = false;
    Vector3 positionHold;
    [SerializeField]
    Transform npcObj;
	// Use this for initialization
	void Start () {
        //transform.localPosition = Vector3.zero;
	}

    void OnEnable() {

    }


    void attacksound() {
        if (myFunction.Function.RandomNumber(2) == 1) {
            soundEffectManager.staticSoundEffect.play_swordloseTarget1();
        }
        else {
            soundEffectManager.staticSoundEffect.play_swordloseTarget2();
        }
    }

    // Update is called once per frame
    public void Update () {
        if (follow) {
            transform.position = npcObj.position;
            }
        /*
        if (follow) {
            savePosition = false;
            if (!setLocalPosition) {
                Debug.Log(setLocalPosition);
                transform.position = Vector3.zero;
                setLocalPosition = true;
            }
        }
        else {
            if (!savePosition) {
                positionHold = transform.position;
                savePosition = true;
                setLocalPosition = false;
            }
            else {
                transform.position = positionHold;
            }
        }
        */
    }

}
