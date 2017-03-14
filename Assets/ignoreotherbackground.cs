using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreotherbackground : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GameObject[] gameObj = GameObject.FindGameObjectsWithTag("ground");  //無視其他enemy碰撞
        foreach (GameObject each in gameObj)
        {
            Physics2D.IgnoreCollision(each.GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
        }
    }
	
}
