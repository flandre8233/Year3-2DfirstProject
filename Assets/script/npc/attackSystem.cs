using UnityEngine;
using System.Collections;
using Spine.Unity;

public class attackSystem : MonoBehaviour {
    selectEnemySystemScript selectEnemySystem;
    npcClass npcclass;
    [SerializeField]
    SkeletonAnimation npcSkeletonAnimation;
    [SerializeField]
    playerSensor2 playerSensor; //確定player在攻擊範圍


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
                npcSkeletonAnimation.state.SetAnimation(1, "sword_idel_single_hand_back_to_front2", false);
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

    }

    // Update is called once per frame
    void Update() {

        if (npcclass.TypeP == npcClass.Type.contorl) { //player attack
            if (Input.GetMouseButtonUp(1) && !attackCDLock && !selectEnemySystem.openTargetLockDown) {  //玩家按下攻擊
                attackFunction();
            }
        }
        else { //npc的攻擊
            if (playerSensor.isFindPlayer && npcReactionLock &&!attackCDLock ) {
                npcReactionLock = false;
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
                gunspawn.Shot();
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

    void spawnAttackAni() {
        npcclass.CastAniP = npcClass.CastAni.onAttack;
        //npcSkeletonAnimation.AnimationName = "side_attack_sword";
        if (ComboCounter == 1) { //反之 COMBOECOUNTER = 2就不需要用這個
            npcSkeletonAnimation.state.SetAnimation(1, "up_attack_sword", false);
            attackGO.SetActive(true);
        }
        Spine.TrackEntry trackEntry1 = npcSkeletonAnimation.state.GetCurrent(1); //*******

        attackVelocity();

    }

    void attackVelocity() {        //velocityPart
        if (npcclass.TypeP == npcClass.Type.contorl) { //給予攻擊動量
            rigid2d.velocity = new Vector2(0, rigid2d.velocity.y);
            rigid2d.AddForce(transform.right * 300 * -rigid2d.transform.localScale.x, ForceMode2D.Force);
        }
        else {
            rigid2d.velocity = new Vector2(0, rigid2d.velocity.y);
            //rigid2d.AddForce(transform.right * 300 * -rigid2d.transform.localScale.x, ForceMode2D.Force);
        }
    }
}
