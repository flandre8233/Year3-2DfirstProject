﻿using UnityEngine;
using System.Collections;

public class gunSpawn : GameFunction{
    npcClass npcclass;
    

    public GameObject bulletPrefabs;
    public GameObject Player;
    public float perSecond;
    bool spawnLock = false;

    [Range(1, 180)]
    [SerializeField]
    int endZRotation;
    [Range(1, 180)]
    [SerializeField]
    int everyAngleShotOnce;
    [SerializeField]
    bool shootByNpc;
    [SerializeField]
    Camera mouseInCameraPosition;


	
	public void Shot() {

        npcclass = GetComponentInParent<Transform>().GetComponentInParent<npcClass>();
        Debug.Log(npcclass.HP);
        if (shootByNpc && npcclass != null) {
            if (npcclass.TypeP == npcClass.Type.contorl) {
                float Angle = ImageLookAt2D(transform.position, getCameraToV3(mouseInCameraPosition)).eulerAngles.z;
                spawnBulletSpecial1((int)Angle, endZRotation, everyAngleShotOnce);
            }
            else {
                float Angle = ImageLookAt2D(transform.position, Player.transform.position).eulerAngles.z;
                spawnBulletSpecial1((int)Angle, endZRotation, everyAngleShotOnce);
            }
        }
        else {
            float Angle = ImageLookAt2D(transform.position, Player.transform.position).eulerAngles.z;
            spawnBulletSpecial1((int)Angle, endZRotation, everyAngleShotOnce);
        }

        
	}

    public void DroneShot() {
        if (Player != null) {  //單純防止出bugs
            float Angle = ImageLookAt2D(transform.position, Player.transform.position).eulerAngles.z;
            spawnBulletSpecial1((int)Angle, endZRotation, everyAngleShotOnce);
        }


    }

    void spawnBulletSpecial1(int faceMidZRotation,int endZRotation,int everyAngleShotOnce) {
        

        
        for (int i = faceMidZRotation - (endZRotation/2); i <= (endZRotation/2)+faceMidZRotation; i+= everyAngleShotOnce) {
            if (shootByNpc && npcclass!= null) {
                
                if (npcclass.TypeP == npcClass.Type.contorl) {
                    StartCoroutine(spawnBullet(bulletPrefabs, Quaternion.Euler(0, 0, i)  ,gunShot.damageType.npcOnly));
                    //Instantiate(bulletPrefabs, transform.position, Quaternion.Euler(0, 0, 5));

                }
                else {
                   // Instantiate(bulletPrefabs, transform.position, Quaternion.Euler(0, 0, 5));

                    StartCoroutine(spawnBullet(bulletPrefabs, Quaternion.Euler(0, 0, i), gunShot.damageType.playerOnly));
                }
            }
            else {
                //Instantiate(bulletPrefabs, transform.position, Quaternion.Euler(0, 0, 5));

                StartCoroutine(spawnBullet(bulletPrefabs, Quaternion.Euler(0, 0, i), gunShot.damageType.playerOnly));
            }
        }
        
    }


    /*
    IEnumerator spawnTimer() {
        spawnLock = true;
        StartCoroutine(spawnBullet(bulletPrefabs, gunShot.damageType.npcOnly));
        yield return new WaitForSeconds(perSecond);
        spawnLock = false;
    }
    */

    IEnumerator spawnBullet(GameObject prefabs,Quaternion rotation, gunShot.damageType damageType) {
        GameObject PF = Instantiate(prefabs, transform.position, rotation) as GameObject;
        PF.GetComponent<gunShot>().damagetype = damageType;
        yield return new WaitForSeconds(5);
        Destroy(PF);
        StopCoroutine("spawnBullet");
    }

}
