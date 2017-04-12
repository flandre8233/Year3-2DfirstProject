using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class canvaslevelselect : MonoBehaviour {
    public GameObject canvas1;
    public GameObject canvas2;

    public GameObject backgroundScrollSystem1;
    public GameObject backgroundScrollSystem2;

    // Use this for initialization
    void Start () {
        if (globalDataBase.staticData.curSelectPage == 1)
        {
            canvas1.SetActive(true);
            canvas2.SetActive(false);

            backgroundScrollSystem2.SetActive(false);
            backgroundScrollSystem1.SetActive(true);

        }
        else
        {
            canvas1.SetActive(false);
            canvas2.SetActive(true);

            backgroundScrollSystem1.SetActive(false);
            backgroundScrollSystem2.SetActive(true);

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
