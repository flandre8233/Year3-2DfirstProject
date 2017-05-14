using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAA : MonoBehaviour {

    Rigidbody rd;


    public int myint = 1;

    int kk = 4;


    // Use this for initialization
    void Start () {
        rd = GetComponent<Rigidbody>();
        myint = 2;


	}
	
	// Update is called once per frame
	void Update () {
        
       // Debug.Log(rd.velocity);
    }
}
