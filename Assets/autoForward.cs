using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoForward : MonoBehaviour {
    public float speed = 10.0f;
         
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(speed * Time.deltaTime, 0, 0);
        transform.position += movement;
	}
}
