﻿using UnityEngine;
using System.Collections.Generic;
using myFunction;
public class attackSensorDamage : GameFunction
{
    npcClass npcclass;
    Function function = new Function(); 
    [SerializeField]
    playerSensorCode playerSensorCode;
    public playerDataClass pDc;
    public short damage;

    List<GameObject> alreadyDamageArray = new List<GameObject>();

    void Start() {

    }

    void OnEnable() {
        alreadyDamageArray.Clear();
    }



    //prefab那邊有問題 小心  http://stackoverflow.com/questions/36095870/how-to-keep-references-to-ui-elements-in-a-prefab-instantiated-at-runtime
    void OnTriggerEnter2D(Collider2D other) {
        npcclass = GetComponentInParent<npcClass>();

        triggerBullet(other);
        triggerNpc(other); //problem maybe?

    }

    void triggerBullet(Collider2D other) {
        if (other.tag == "bullet" && npcclass.WeaponP == npcClass.Weapon.sword && npcclass.TypeP == npcClass.Type.contorl) {
            GameObject bulletObject = other.gameObject;
            //bulletObject.transform.eulerAngles = new Vector3(0,0,444);
            bulletObject.transform.eulerAngles = new Vector3(0, 0, function.RandomNumber(40) + 160 + bulletObject.transform.eulerAngles.z);
            bulletObject.GetComponent<gunShot>().speed *= 3;
            //bulletObject
            //transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
    void triggerNpc(Collider2D other) {
        bool alreadyDeal = false;
        if (other.tag == "enemy" && other.gameObject != gameObject) {   //多重攻擊目標
            if (alreadyDamageArray.Count > 0) {
                foreach (GameObject each in alreadyDamageArray) {
                    if (each == other.gameObject) {
                        alreadyDeal = true;
                        break;
                    }
                }
            }


            if (!alreadyDeal) {
                switch (npcclass.TypeP) {
                    case npcClass.Type.contorl:
                        if (other.gameObject.GetComponent<npcClass>().TypeP == npcClass.Type.normal) {
                            other.gameObject.GetComponent<npcScript>().npcHPCheck(damage, "player");
                            alreadyDamageArray.Add(other.gameObject);
                            //gameObject.SetActive(false);
                        }
                        break;
                    case npcClass.Type.normal:
                        if (other.gameObject == playerSensorCode.npc && playerSensorCode.npc != null) {
                            other.gameObject.GetComponent<npcScript>().npcHPCheck(damage, "enemy");
                            alreadyDamageArray.Add(other.gameObject);
                            //gameObject.SetActive(false);
                        }
                        break;
                }
            }


        }
    }
}
