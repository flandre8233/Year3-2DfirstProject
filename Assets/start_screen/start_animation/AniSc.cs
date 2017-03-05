using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniSc : MonoBehaviour {

    public void destroyAni() {
        if (Application.loadedLevel == 1) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
