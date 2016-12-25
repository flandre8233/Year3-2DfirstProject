using UnityEngine;
using System.Collections;

public class playerSensorCode : MonoBehaviour {
    public GameObject npc;
    npcClass.attackState OriginalAttackState;

    public bool isLostPlayer = false;
    float timer = 2.0f;

    // Use this for initialization
    void Start() {
    }
	
	// Update is called once per frame
	void Update () {
        if (isLostPlayer) {
            timer -= Time.deltaTime;

            if (timer <= 0.0f) {
                npc = null;

                GetComponentInParent<npcClass>().attackStateP = OriginalAttackState;
            }
        }
        else {
            timer = 2.0f;
        }

	}

    void OnTriggerEnter2D(Collider2D other) {

        
        if (other.gameObject.tag == "enemy" && GetComponentInParent<npcClass>().WeaponP !=  npcClass.Weapon.none ) { //進入攻擊狀態

            if (other.GetComponent<npcClass>().TypeP == npcClass.Type.contorl) {
                string st = this.gameObject.name + "    " + "find";
                npc = other.gameObject;  //player as 

                //StateSave = GetComponentInParent<npcClass>().attackStateP;
                isLostPlayer = false;
                if (GetComponentInParent<npcClass>().attackStateP != npcClass.attackState.attack) {
                    OriginalAttackState = GetComponentInParent<npcClass>().attackStateP;
                }
                GetComponentInParent<npcClass>().attackStateP = npcClass.attackState.attack; 
                //GetComponent<npcClass>().attackStateP = npcClass.attackState.attack;
            }

        }

    }

    void OnTriggerExit2D(Collider2D other) {
       

        if (other.gameObject == npc) {
            isLostPlayer = true;

        }


    }



}
