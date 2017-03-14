using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class canvaslevelselect : MonoBehaviour {
    public GameObject canvas1;
    public GameObject canvas2;

	// Use this for initialization
	void Start () {
        if (globalDataBase.staticData.curSelectPage == 1)
        {
            canvas1.SetActive(true);
            canvas2.SetActive(false);
        }else
        {
            canvas1.SetActive(false);
            canvas2.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
