﻿using UnityEngine;
using System.Collections;
using Spine.Unity;

public class attackSystem : MonoBehaviour {
    selectEnemySystemScript selectEnemySystem;
    npcClass npcclass;
    [SerializeField]
    SkeletonAnimation npcSkeletonAnimation;

    public float CD = 0.5F;
    bool attackCDLock = false;
    public GameObject attackGO;
    public gunSpawn gunspawn;
    Rigidbody2D rigid2d;
    bool attacksensor = false;
    public float attackSensorCD = 0.2F;

    bool isCastCombo = false;
    int ComboCounter = 0;

    // Use this for initialization
    void Start() {
        rigid2d = GetComponentInParent<Rigidbody2D>();
        npcclass = GetComponentInParent<npcClass>();
        if (GameObject.FindGameObjectsWithTag("megumin_player").Length != 0) {
            selectEnemySystem = GameObject.FindGameObjectsWithTag("megumin_player")[0].GetComponent<selectEnemySystemScript>(); //
        }
        npcSkeletonAnimation.state.Complete += State_Complete;


    }

    private void State_Complete(Spine.TrackEntry trackEntry) { //動作結果那時
                                                               //throw new System.NotImplementedException();

        if (trackEntry.animation.name == "up_attack_sword" && trackEntry.trackIndex == 1) {
            if (ComboCounter >= 2) {
                attackGO.SetActive(false);
                npcSkeletonAnimation.state.SetAnimation(1, "side_attack_sword", false);
                attackGO.SetActive(true);
            }
            else {
                attackGO.SetActive(false);
                attackCDLock = true;
                StartCoroutine("attackColdDown");
                rigid2d.velocity = new Vector2(0, rigid2d.velocity.y); //還原
                npcclass.CastAniP = npcClass.CastAni.onMovement;
            }

        }

        if (trackEntry.animation.name == "side_attack_sword" && trackEntry.trackIndex == 1) {
            attackGO.SetActive(false);
            attackCDLock = true; //進入CD
            StartCoroutine("attackColdDown");
            rigid2d.velocity = new Vector2(0, rigid2d.velocity.y);
            npcclass.CastAniP = npcClass.CastAni.onMovement;
        }


        //Debug.Log(trackIndex + " " + state.GetCurrent(trackIndex) + ": end");
    }

    // Update is called once per frame
    void Update() {

        if (npcclass.TypeP == npcClass.Type.contorl) { //player attack

            if (Input.GetMouseButtonUp(1) && !attackCDLock && !selectEnemySystem.openTargetLockDown) {  //玩家按下攻擊
                attackFunction();

            }

        }
        else {
            if (!attackCDLock) {
                attackFunction();
            }
        }



    }

    void attackFunction() {
        if (ComboCounter >= 2) {
            attackCDLock = true; //鎖上不讓再打
        }
        else if (ComboCounter == 1) {
            isCastCombo = true;
        }

        if (!attackCDLock) {

            if (attackGO != null) {
                //StartCoroutine("spawnAttackSensorFunction");
                Debug.Log("diudiudiud");
                ComboCounter++;
                spawnAttackAni();
            }
            else {
                gunspawn.Shot();
            }
        }
    }

    IEnumerator attackColdDown() {
        yield return new WaitForSeconds(CD);
        ComboCounter = 0;
        attackCDLock = false;
    }


    void spawnAttackAni() {
        npcclass.CastAniP = npcClass.CastAni.onAttack;
        //npcSkeletonAnimation.AnimationName = "side_attack_sword";
        if (ComboCounter == 1) {
            npcSkeletonAnimation.state.SetAnimation(1, "up_attack_sword", false);
            attackGO.SetActive(true);
        }

        Spine.TrackEntry trackEntry1 = npcSkeletonAnimation.state.GetCurrent(1); //*******
        //velocityPart

        rigid2d.velocity = new Vector2(0, rigid2d.velocity.y);
        rigid2d.AddForce(transform.right * 300 * -rigid2d.transform.localScale.x, ForceMode2D.Force);
        //rigid2d.velocity -= new Vector2(3.5f * rigid2d.transform.localScale.x, 0);
        //rigid2d.transform.position += transform.right * Time.deltaTime * 100;
        Debug.Log("diudiudiud");
    }

}
