using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballDetection : MonoBehaviour {

    [SerializeField]
     GameObject shotPoint;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "enemy") {
            if (other.gameObject.GetComponent<npcClass>().TypeP == npcClass.Type.contorl) {
                shotPoint.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "enemy") {
            if (other.gameObject.GetComponent<npcClass>().TypeP == npcClass.Type.contorl) {
                shotPoint.SetActive(false);
            }
        }
    }

    }
