using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackFadeScipt : MonoBehaviour {
    [SerializeField]
    public bool isStartAni;
    [SerializeField]
    public bool isNeedFadeIn = false;
    [SerializeField]
    Animator ani;
    [SerializeField]
    RectTransform Rts;

    bool DoOnce;

	// Use this for initialization
	void Start () {
        check();
        Rts.anchorMax = new Vector2(1,1);
	}
	
    void check() {
        if (isStartAni && !DoOnce) {
            DoOnce = true;
            ani.SetTrigger("start");
            if (isNeedFadeIn) {
                ani.SetBool("fadeIn", true);
            }
            else {
                ani.SetBool("fadeIn", false);
            }
        }
    }

	// Update is called once per frame
	void Update () {
        check();
    }
}
