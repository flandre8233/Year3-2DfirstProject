using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSensor2 : MonoBehaviour {
    public GameObject npc;
    public bool isFindPlayer = false;
    float timer = 2.0f;

    // Use this for initialization
    void Start() {
    }

    void OnTriggerEnter2D(Collider2D other) {


        if (other.gameObject.tag == "enemy") {

            if (other.GetComponent<npcClass>().TypeP == npcClass.Type.contorl) {

                npc = other.gameObject;  //player as 
                isFindPlayer = true;

            }

        }

    }

    void OnTriggerExit2D(Collider2D other) {


        if (other.gameObject == npc) {
            isFindPlayer = false;

        }


    }



}
