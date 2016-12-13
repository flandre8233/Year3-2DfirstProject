using UnityEngine;
using System.Collections;

public class npcMove : GameFunction
{
    npcClass npcclass;
    npcScript npcscript;
    Rigidbody2D rb2d;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private Transform jumpRequestPointCheck1;
    [SerializeField]
    private Transform patrolLeftPoint;
    [SerializeField]
    private Transform patrolRightPoint;
    [Range(0,10)][SerializeField]
    private int patrolWaitTime;
    [SerializeField]
    private GameObject attackTargetGameObject;

    public float checker;

    Vector3 attackTargetPoint;

    Vector3 patrolLeftPointSave;
    Vector3 patrolRightPointSave;
    float localScaleX;

    bool isPatrolToLeft = true;
    bool patrolWaitLock = false;

    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        npcscript = GetComponent<npcScript>();
        npcclass = GetComponent<npcClass>();
        patrolLeftPointSave = patrolLeftPoint.position;
        patrolRightPointSave = patrolRightPoint.position;
        if (transform.localScale.x < 0) {
            localScaleX = -1 * transform.localScale.x;
        }
        else {
            localScaleX = transform.localScale.x;
        }


    }

    // Update is called once per frame
    void Update() {

        if (npcclass.TypeP == npcClass.Type.contorl) {  // playerControl 
        }
        else {  // npcAIMove npc自行控制
            simpleAutoJump();

            switch (npcclass.attackStateP) {
                case npcClass.attackState.alert:

                    break;
                case npcClass.attackState.attack:
                    if(npcclass.CastAniP == npcClass.CastAni.onMovement) { //只能在能播放移動動畫的時間移動
                        switch (npcclass.WeaponP) {
                            case npcClass.Weapon.sword:
                                simpleChasePlayer();
                                break;
                            case npcClass.Weapon.axe:
                                simpleChasePlayer();
                                break;
                            case npcClass.Weapon.rifle:
                                simpleFacePlayer();
                                break;
                        }
                    }

                    break;
                case npcClass.attackState.guard:
                    simplePatrol();

                    break;
                case npcClass.attackState.stand:

                    break;
            }

        }



    }

#region npc跳躍
    void simpleAutoJump() {
        if ((Physics2D.OverlapCircle(jumpRequestPointCheck1.position, 0.15f, npcscript.groundLayer))   ) {
            jump();
        }
    }

    void jump() {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.AddForce(transform.up * jumpHeight / 54, ForceMode2D.Impulse);  //jump
        npcclass.movementStateP = npcClass.movementState.jumpingBothCanMove;
    }
    #endregion


    void simplePatrol() {
        checker = patrolRightPointSave.x;
        if (!patrolWaitLock) {
            if (isPatrolToLeft) {
                if (transform.position.x >= patrolLeftPointSave.x) {
                    //rb2d.AddForce(new Vector2(-(Time.deltaTime * speed * rb2d.transform.localScale.x), 0 ), ForceMode2D.Force);
                        rb2d.velocity = new Vector2( -( speed * rb2d.transform.localScale.x), rb2d.velocity.y);
                        //rb2d.velocity = new Vector2(-4, rb2d.velocity.y);
                }
                else {
                    StartCoroutine(Timer(patrolWaitTime));
                }
            }
            else {
                if (transform.position.x <= patrolRightPointSave.x) {
                    //rb2d.AddForce(new Vector2(-(Time.deltaTime * speed * rb2d.transform.localScale.x), 0 ), ForceMode2D.Force);
                    rb2d.velocity = new Vector2( -( speed * rb2d.transform.localScale.x), rb2d.velocity.y);
                    //rb2d.velocity = new Vector2(4, rb2d.velocity.y);
                }
                else {
                    StartCoroutine(Timer(patrolWaitTime));
                }
            }
        }
    }

    IEnumerator Timer(int waitSeconds) {  //patroltimer
        patrolWaitLock = true;
        yield return new WaitForSeconds(waitSeconds);
        
        patrolWaitLock = false;
        if (isPatrolToLeft) {
            transform.localScale = new Vector3(-localScaleX, transform.localScale.y, transform.localScale.z);
            isPatrolToLeft = false;
        }
        else {
            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
            isPatrolToLeft = true;
        }

        }

    void simpleChasePlayer() {
        if (attackTargetGameObject.GetComponent<playerSensorCode>().npc != null) {
            attackTargetPoint = attackTargetGameObject.GetComponent<playerSensorCode>().npc.transform.position;
            //Gfunction.ImageLookAt2D(transform.position, attackTargetPoint);

            if (transform.position.x >= attackTargetPoint.x) {  //簡單角色移動

                rb2d.velocity = new Vector2(-(speed * rb2d.transform.localScale.x) * 1.2f , rb2d.velocity.y);
                transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

            }
            else {
                rb2d.velocity = new Vector2(-(speed * rb2d.transform.localScale.x) *1.2f, rb2d.velocity.y);
                transform.localScale = new Vector3(-localScaleX, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    void simpleFacePlayer() {
        if (attackTargetGameObject.GetComponent<playerSensorCode>().npc != null) {
            attackTargetPoint = attackTargetGameObject.GetComponent<playerSensorCode>().npc.transform.position;
            //Gfunction.ImageLookAt2D(transform.position, attackTargetPoint);

            if (transform.position.x >= attackTargetPoint.x) {  //簡單角色移動
                transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

            }
            else {
                transform.localScale = new Vector3(-localScaleX, transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
