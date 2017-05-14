using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectChangeBackGround : MonoBehaviour {
    public GameObject backgroundScrollSystem1;
    public GameObject backgroundScrollSystem2;

    public Animator ani;

    void changeBackground() {
        if (GameObject.Find("globalDataBase") != null) {
            GameObject findObject = GameObject.Find("globalDataBase");
            globalDataBase globalDataBase = findObject.GetComponent<globalDataBase>();
            if (globalDataBase.curSelectPage == 1) {
                backgroundScrollSystem2.SetActive(false);
                backgroundScrollSystem1.SetActive(true);

            }
            else {
                backgroundScrollSystem1.SetActive(false);
                backgroundScrollSystem2.SetActive(true);
            }
        }
    }

    // Use this for initialization
    void Start () {
        ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
