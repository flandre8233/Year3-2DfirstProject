using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackFadeScipt : MonoBehaviour {
    [SerializeField]
    bool isStartAni;
    [SerializeField]
    bool isNeedFadeIn = false;
    [SerializeField]
    Animator ani;
    [SerializeField]
    RectTransform Rts;

    bool DoOnce;

	// Use this for initialization
	void Start () {
        ani.Stop(); //here
        Rts.anchorMax = new Vector2(1,1);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (isStartAni&& !DoOnce) {
            DoOnce = true;
            if (isNeedFadeIn) {
                ani.Play("BlackFadeInnimation");
            }
            else {
                ani.Play("blackFadeOut");
            }
        }
	}
}
