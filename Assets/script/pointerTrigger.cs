using UnityEngine;
using System.Collections.Generic;

public class pointerTrigger : MonoBehaviour {
    public List<GameObject> TriggerList = new List<GameObject>();

    void OnEnable() {
        TriggerList.Clear();
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (!TriggerList.Contains(other.gameObject) && other.gameObject.tag == "enemy" ) {
            if (other.gameObject.GetComponent<npcClass>().TypeP != npcClass.Type.contorl && other.gameObject.GetComponent<npcClass>().liveStateP != npcClass.liveState.dead) {
                TriggerList.Add(other.gameObject);
            }
            
        }

    }

    void OnTriggerExit2D(Collider2D other) {
        if (TriggerList.Contains(other.gameObject) && other.gameObject.tag == "enemy") {
            //remove it from the list
            if (other.gameObject.GetComponent<npcClass>().TypeP != npcClass.Type.contorl && other.gameObject.GetComponent<npcClass>().liveStateP != npcClass.liveState.dead) {
                TriggerList.Remove(other.gameObject);
            }
            
        }

    }

}
