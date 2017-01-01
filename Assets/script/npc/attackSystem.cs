using UnityEngine;
using System.Collections;
using Spine.Unity;

public class attackSystem : MonoBehaviour {
    selectEnemySystemScript selectEnemySystem;
    npcClass npcclass;
    npcScript npcscript;
    [SerializeField]
    SkeletonAnimation npcSkeletonAnimation;
    [SerializeField]
    playerSensor2 playerSensor; //確定player在攻擊範圍
    [SerializeField]
    public float attackVelocity = 300;


    public float CD = 0.5F;
    public float npcReaction = 5.0F;
    bool npcReactionLock = true;
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
        npcscript = GetComponentInParent<npcScript>();
        if (GameObject.FindGameObjectsWithTag("megumin_player").Length != 0) {
            selectEnemySystem = GameObject.FindGameObjectsWithTag("megumin_player")[0].GetComponent<selectEnemySystemScript>(); //
        }
        npcSkeletonAnimation.state.Complete += State_End;
    }

    private void State_End(Spine.TrackEntry trackEntry) { //動作結果那時
                                                               //throw new System.NotImplementedException();

        if (trackEntry.animation.name == "up_attack" && trackEntry.trackIndex == 0) {
            if (ComboCounter >=2) {
                StartCoroutine("sideAttackStoprheVelocity");
                npcSkeletonAnimation.state.SetAnimation(0, "side_attack", false);
                npcSkeletonAnimation.timeScale = 2.5f;
                npcSkeletonAnimation.Update(0);
                attackGO.SetActive(false);
                attackGO.SetActive(true);
                
            }
            else {
                
                npcSkeletonAnimation.state.SetAnimation(0, "up_front", false);
                //npcSkeletonAnimation.state.SetAnimation(1, npcscript.idle1, false);
            }

        }

        if (trackEntry.animation.name == "side_attack" && trackEntry.trackIndex == 0) {
            attackGO.SetActive(false);
            swordBlur.GetComponentInChildren<blurHold>().follow = false;
            //swordBlur.SetActive(false);
            attackCDLock = true; //進入CD
            StartCoroutine("attackColdDown");
            rigid2d.velocity = new Vector2(0, rigid2d.velocity.y);
            npcclass.CastAniP = npcClass.CastAni.onMovement;
        }

        if (trackEntry.animation.name == "up_front" && trackEntry.trackIndex == 0) {
            swordBlur.GetComponentInChildren<blurHold>().follow = false;
            //swordBlur.SetActive(false);
            npcSkeletonAnimation.state.SetAnimation(0, npcscript.idle1, false);
            attackGO.SetActive(false);
            attackCDLock = true; //進入CD
            StartCoroutine("attackColdDown");
            rigid2d.velocity = new Vector2(0, rigid2d.velocity.y);
            npcclass.CastAniP = npcClass.CastAni.onMovement;
        }

    }

    // Update is called once per frame
    void Update() {
        Debug.Log(ComboCounter);
        if (npcclass.TypeP == npcClass.Type.contorl) { //player attack
            if (Input.GetMouseButtonUp(1) && !attackCDLock && !selectEnemySystem.openTargetLockDown) {  //玩家按下攻擊
                attackFunction();
            }
        }
        else { //npc的攻擊
            if (playerSensor.isFindPlayer && npcReactionLock &&!attackCDLock ) { //npc反應
                npcReactionLock = false;
                Debug.Log(npcclass.TypeP);
                StartCoroutine("npcReactionColdDown");
            }

        }
        


    }

    void attackFunction() {
        if (ComboCounter >= 2) {
            attackCDLock = true; //鎖上不讓再打
            
        }
        else if (ComboCounter == 1) {
            isCastCombo = true; //單純指出現在需要COMBO 未有任何程式碼需要用到
            
        }
            if (attackGO != null) {
                //StartCoroutine("spawnAttackSensorFunction");
                ComboCounter++; //計算當前要不要COMBO COMBO到多少?
            spawnAttackAni();//播動畫
            
        }
            else {
            Debug.Log("ddd");
                gunspawn.Shot();
            StartCoroutine("attackColdDown"); //正常要播完動畫才cd
        }
        if (swordBlur != null) {
            swordBlur.GetComponentInChildren<Animator>().SetInteger("ComboCounter", ComboCounter);
            Debug.Log(swordBlur.GetComponentInChildren<Animator>().name);
            swordBlur.GetComponentInChildren<Animator>().speed = 2.5f;
        }
    }

    IEnumerator attackColdDown() { //當正式打完攻擊招式時就進行COLDDOWN
        yield return new WaitForSeconds(CD);
        ComboCounter = 0;
        attackCDLock = false;
    }
    IEnumerator npcReactionColdDown() { //當正式打完攻擊招式時就進行COLDDOWN
        yield return new WaitForSeconds(npcReaction);
        if (!attackCDLock && !npcReactionLock) {
            attackCDLock = true; //只能打一次
            attackFunction();
        }

        npcReactionLock = true;

    }
    public GameObject swordBlur;
    void spawnAttackAni() {
        npcclass.CastAniP = npcClass.CastAni.onAttack;
        swordBlur.transform.localScale = new Vector3(-npcclass.gameObject.transform.localScale.x, swordBlur.transform.localScale.y, swordBlur.transform.localScale.z);
        swordBlur.GetComponentInChildren<blurHold>().follow = true;
        //npcSkeletonAnimation.AnimationName = "side_attack_sword";
        if (ComboCounter == 1) { //反之 COMBOECOUNTER = 2就不需要用這個
            npcSkeletonAnimation.state.SetAnimation(0, "up_attack", false);
            npcSkeletonAnimation.timeScale = 2.5f;
            
            swordBlur.SetActive(false);
            swordBlur.GetComponentInChildren<blurHold>().Update();
            swordBlur.SetActive(true);
            attackGO.SetActive(true);
            attackVelocitySetting(attackVelocity);
        }
        //Spine.TrackEntry trackEntry1 = npcSkeletonAnimation.state.GetCurrent(1); //*******


    }

    void attackVelocitySetting(float velocity) {        //velocityPart
        if (npcclass.TypeP == npcClass.Type.contorl) { //給予攻擊動量
            rigid2d.velocity = new Vector2(0, rigid2d.velocity.y);
            rigid2d.AddForce(transform.right * velocity * -rigid2d.transform.localScale.x, ForceMode2D.Force);
            StartCoroutine( "stoprheVelocity");
        }
        else {
            rigid2d.velocity = new Vector2(0, rigid2d.velocity.y);
            //rigid2d.AddForce(transform.right * 300 * -rigid2d.transform.localScale.x, ForceMode2D.Force);
        }
    }


    IEnumerator sideAttackStoprheVelocity() {
        yield return new WaitForSeconds(0.15f);
        attackVelocitySetting(attackVelocity);
    }
    IEnumerator stoprheVelocity() { 
        yield return new WaitForSeconds(0.15f);
        rigid2d.velocity = new Vector2(0, rigid2d.velocity.y);
    }
}
