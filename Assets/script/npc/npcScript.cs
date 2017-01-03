using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;
using myFunction;

public class npcScript : GameFunction
{
    npcClass npcclass;
    playerDataClass playerData;

    bool IsAlreadyStartDeadFunction = false;
    bool inDeadHitFly = false;
    
    [SerializeField]
    private Transform GroundCheck1;
    [SerializeField]
    public  Transform GroundCheckWall1;
    [SerializeField]
    public  Transform GroundCheckWall2;
    [SerializeField]
    public LayerMask groundLayer;

    [SerializeField]
    delegate void npcUpdataDelegate();
    npcUpdataDelegate npcDelegate;

     playerMove playermove;
     npcMove npcmove;

    Rigidbody2D rigidbody2d;

    [SerializeField]
    public SkeletonAnimation thisAnimation;
    [SerializeField]
    public Animator TestAnimator;

    [SerializeField]
    public Object hpParticlePrefab;
    [SerializeField]
    public Object SoulsParticlePrefab;

    
    [SerializeField]
    public string idle1;
    [SerializeField]
    public string run;
    [SerializeField]
    public string jump;
    [SerializeField]
    public string hit;
    [SerializeField]
    public string dead;
    [SerializeField]
    public string attack1;
    [SerializeField]
    public string attack2;
    

    // Use this for initialization
    void Start () {
        TestAnimator = GetComponentInChildren<Animator>();
        playerData = GameObject.FindGameObjectsWithTag("backgroundScipt")[0].GetComponent<playerDataClass>(); //
        rigidbody2d = GetComponent<Rigidbody2D>();
        npcclass = GetComponent<npcClass>();
        playermove = GetComponent<playerMove>();
        npcmove = GetComponent<npcMove>();
        npcclass.npcClassSetUp();
        ignoreCollisionSetUp();//無視其他enemy碰撞
        if (thisAnimation!=null) {
            thisAnimation.state.Complete += MyCompleteListener;
        }
        DelegateSetUp();
    }
	
    void DelegateSetUp() {
        npcDelegate += attackStateChangeCodeFunction;
        npcDelegate += npcColorSetting;
        npcDelegate += movementStateCheck;
        npcDelegate += movementAnimationSetting;
        npcDelegate += NpcDead;
        npcDelegate += npcmove.delegateUpdate;
        npcDelegate += playermove.delegateUpdate;
    }

    void ignoreCollisionSetUp() {//無視其他enemy碰撞
        GameObject[] gameObj = GameObject.FindGameObjectsWithTag("enemy");  //無視其他enemy碰撞
        foreach (GameObject each in gameObj) {
            Physics2D.IgnoreCollision(each.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(each.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
            Physics2D.IgnoreCollision(each.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(each.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }

        if (gameObject.tag != "enemy") {  //無視自己同tag的npc碰撞
            gameObj = GameObject.FindGameObjectsWithTag(gameObject.tag);  
            foreach (GameObject each in gameObj) {
                Physics2D.IgnoreCollision(each.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
                Physics2D.IgnoreCollision(each.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
                Physics2D.IgnoreCollision(each.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
                Physics2D.IgnoreCollision(each.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
            }
        }

        GameObject[] HPpartaclegameObj = GameObject.FindGameObjectsWithTag("HPpartacle");  //無視其他enemy碰撞
        foreach (GameObject each in HPpartaclegameObj) {
            Physics2D.IgnoreCollision(each.GetComponent<Collider2D>(), GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(each.GetComponent<Collider2D>(), GetComponent<CircleCollider2D>());
        }
    }

	// Update is called once per frame
	void Update () {
        if (npcDelegate != null) {
            npcDelegate.Invoke();
        }
    }

    //很像end效果都一樣
    //complete 當完成這次動畫後



    private void MyCompleteListener( Spine.TrackEntry trackEntry) {
        if (npcclass.TypeP == npcClass.Type.contorl) {

        }
        loopEndHoldTheAnimation("jump", trackEntry);
        loopEndHoldTheAnimation("hit_sword", trackEntry);
    }

    void loopEndHoldTheAnimation(string name, Spine.TrackEntry trackEntry) {
        if (thisAnimation.AnimationName == name) { //使跳躍動畫不會回到<none>
            thisAnimation.state.SetAnimation(0, name, false);
            trackEntry.trackTime = trackEntry.TrackEnd;
            thisAnimation.timeScale = 0.00f;
        }
    }

    void attackStateChangeCodeFunction() {
        #region  進入不同攻擊狀態時的程式啟用
        if (npcclass.TypeP == npcClass.Type.normal && IsAlreadyStartDeadFunction == false) {  //npc是自已控制,非由玩家控制
            //GetComponent<playerMove>().enabled = false;
            switch (npcclass.attackStateP) {
                case npcClass.attackState.alert:

                    break;
                case npcClass.attackState.attack:


                    if (GetComponentInChildren<attackSystem>() != null && !GetComponentInChildren<attackSystem>().enabled) {
                        GetComponentInChildren<attackSystem>().enabled = true;
                    }

                    break;
                case npcClass.attackState.guard:
                    if (!GetComponent<npcMove>().enabled) {
                        GetComponent<npcMove>().enabled = true;
                    }
                    if (GetComponentInChildren<attackSystem>() != null && GetComponentInChildren<attackSystem>().enabled) {
                        GetComponentInChildren<attackSystem>().enabled = false;
                    }
                    break;
                case npcClass.attackState.stand:
                    if (GetComponentInChildren<attackSystem>() != null && GetComponentInChildren<attackSystem>().enabled) {
                        GetComponentInChildren<attackSystem>().enabled = false;
                    }
                    break;
            }


        }
        else {
            //GetComponent<playerMove>().enabled = true;
            //GetComponent<npcMove>().enabled = false;
            //GetComponent<attackSystem>().enabled = true;
        }
        #endregion
    }

    void movementAnimationSetting() {
        if (Time.timeScale != 0 && npcclass.CastAniP == npcClass.CastAni.onMovement) {
            switch (npcclass.movementStateP) {
                case npcClass.movementState.walking:
                    if (TestAnimator != null) {
                        TestAnimator.SetBool("onWalking", true);
                        TestAnimator.SetBool("onFalling", false);
                        TestAnimator.SetBool("onJumping", false);
                        TestAnimator.SetBool("onLanded", false);
                    }
                    break;
                case npcClass.movementState.jumpingBothCanMove:
                    if (TestAnimator != null) {
                        TestAnimator.SetBool("onWalking", false);
                        TestAnimator.SetBool("onFalling", false);
                        TestAnimator.SetBool("onJumping", true);
                        TestAnimator.SetBool("onLanded", false);
                    }
                    break;
                case npcClass.movementState.falling:
                    break;
                case npcClass.movementState.landed:
                    if (TestAnimator != null) {
                        TestAnimator.SetBool("onWalking", false);
                        TestAnimator.SetBool("onFalling", false);
                        TestAnimator.SetBool("onJumping", false);
                        TestAnimator.SetBool("onLanded", true);
                    }
                    GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                    break;
            }
        }
    }

    void movementStateCheck() {
        #region npc的move狀態定位
        if (Time.timeScale != 0 ) {
            if ((Physics2D.OverlapCircle(GroundCheck1.position, 0.35f, groundLayer)) && ((rigidbody2d.velocity.x > 0.1 * Time.deltaTime) || (rigidbody2d.velocity.x < -0.1 * Time.deltaTime))) {
                npcclass.movementStateP = npcClass.movementState.walking;
            }
            else if (rigidbody2d.velocity.y > 0.1 * Time.deltaTime && !(Physics2D.OverlapCircle(GroundCheck1.position, 0.35f, groundLayer))) {
                npcclass.movementStateP = npcClass.movementState.jumpingBothCanMove;
                GetComponent<Rigidbody2D>().gravityScale = 3.0f;

            }
            else if (rigidbody2d.velocity.y < -0.05 * Time.deltaTime && !(Physics2D.OverlapCircle(GroundCheck1.position, 0.35f, groundLayer))) {
                npcclass.movementStateP = npcClass.movementState.falling;
            }
            else if (Physics2D.OverlapCircle(GroundCheck1.position, 0.15f, groundLayer)) {
                npcclass.movementStateP = npcClass.movementState.landed;
            }
        }
        
        #endregion
    }

    void npcColorSetting() {
        #region 顏色設定，但放在這裡不會是一個好主意
        if (npcclass.TypeP == npcClass.Type.contorl) {
            GetComponentInChildren<SkeletonAnimator>().skeleton.SetColor(Color.red);
        }
        else {
            GetComponentInChildren<SkeletonAnimator>().skeleton.SetColor(Color.white);
        }
        #endregion
    }

    public void npcHPCheck(short DamageDeal ,string dealerStringType) {
        #region HP


        if (dealerStringType == "player") { //是誰打出這個傷害
            npcclass.HP = 0;
        }
        else {

        }

        npcclass.HP -= DamageDeal;
        if (npcclass.HP <= 0 ) {
            npcclass.liveStateP = npcClass.liveState.dead;
            if (dealerStringType == "player") { //是誰打出這個傷害
                //playerData.HP = playerData.MAXHP;
            }

            //Destroy(gameObject);
            
            if (npcclass.TypeP == npcClass.Type.contorl) {
               if (playerData.HP - DamageDeal <= 0) {//玩家控制並玩家已經沒有hp
                    Time.timeScale = 1f;
                    playerData.HP = 0.0f;
                    npcclass.TypeP = npcClass.Type.normal;
                    OnPlayerGameOver(); //player gameover  這堆code應該放在player
                    gameStateDataClass gameState = GameObject.FindGameObjectsWithTag("backgroundScipt")[0].GetComponent<gameStateDataClass>();
                    gameState.gamestate = gameStateDataClass.gameState.gameover;
                }
                else {//玩家控制並玩家還有hp
                    playerData.HP -= DamageDeal;
                }
            }

        }
        #endregion
    }

    void NpcDead() { //do once
        #region npc死亡時做的程式
        if (npcclass.liveStateP == npcClass.liveState.dead && npcclass.TypeP != npcClass.Type.contorl) { //當死亡時
             if (!IsAlreadyStartDeadFunction) { //當死亡時只做一次
                IsAlreadyStartDeadFunction = true;

                switch (npcclass.TypeP) {
                    case npcClass.Type.normal:
                        spawnHPParticle(10);
                        break;
                    case npcClass.Type.spyder:
                        spawnHPParticle(1);
                        break;
                }

                //GetComponent<npcMove>().enabled = false;
                npcclass.CastAniP = npcClass.CastAni.onDestory;
                npcDelegate -= npcmove.delegateUpdate;
                npcDelegate -= playermove.delegateUpdate;

                if (npcclass.TypeP == npcClass.Type.normal && GetComponentInChildren<attackSystem>() ) {
                    GetComponentInChildren<attackSystem>().enabled = false;
                }

                //npcclass.movementStateP = npcClass.movementState.jumpingBothCanMove;//強制鎖定為跳躍  處理死亡時往後擊飛
                npcclass.CastAniP = npcClass.CastAni.onDestory;
                if (thisAnimation!=null) {
                    thisAnimation.state.SetAnimation(0, "hit_sword", false);
                    thisAnimation.loop = false;
                }
                inDeadHitFly = true;
                npcDelegate += spyDerSpecDeadAni;
                //rigidbody2d.velocity = new Vector2(5f * (Function.RandomNumber(10) / 10) * transform.localScale.x, 20 * (Function.RandomNumber(10) / 10)); //大過0面向左邊,反則右邊
                if (transform.localScale.x > 0f) { //大過0面向左邊,反則右邊
                    //rigidbody2d.velocity = new Vector2(2.5f, 10);
                    
                    rigidbody2d.velocity = new Vector2( Function.RandomNumber(10) + 5, Function.RandomNumber(15) + 10  ); //大過0面向左邊,反則右邊
                }
                else {
                    //rigidbody2d.velocity = new Vector2(-2.5f, 10);
                    rigidbody2d.velocity = new Vector2(-Function.RandomNumber(10) + 5,  Function.RandomNumber(15)+10  ); //大過0面向左邊,反則右邊
                }
                

            }
            else {  //當死亡時就一直做
                if (npcclass.movementStateP == npcClass.movementState.landed && inDeadHitFly && npcclass.TypeP != npcClass.Type.spyder) { //landed時就毀掉自己
                    DeadFadeOut();
                    //playerData.playerSouls += npcclass.souls; //玩家靈魂增加
                }
                if (npcclass.liveStateP == npcClass.liveState.dead && npcclass.TypeP == npcClass.Type.spyder ) {
                    DeadFadeOut();
                }
            }

        }
        #endregion
    }

    float z = 0;
    bool spyTranformsSettingLock = false;
    void spyDerSpecDeadAni() {
        if (!spyTranformsSettingLock) {
            spyTranformsSettingLock = true;
            //GetComponentInChildren<SkeletonAnimator>().gameObject.transform.localPosition = Vector3.zero;
        }
        if (npcclass.TypeP == npcClass.Type.spyder && npcclass.liveStateP == npcClass.liveState.dead) {
            GetComponentInChildren<SkeletonAnimator>().gameObject.transform.RotateAround(transform.position, transform.forward, Time.deltaTime * Function.RandomNumber(45) + 10);
        }
    }

    float onDeadAlpha =  1.0f;

    void DeadFadeOut() {
        onDeadAlpha = Mathf.Lerp(onDeadAlpha,0,Time.deltaTime*3f);
        GetComponentInChildren<SkeletonAnimator>().skeleton.SetColor(new Color(GetComponentInChildren<SkeletonAnimator>().skeleton.r, GetComponentInChildren<SkeletonAnimator>().skeleton.g, GetComponentInChildren<SkeletonAnimator>().skeleton.b, onDeadAlpha) );
        if (GetComponentInChildren<SkeletonAnimator>().skeleton.A <= 0.1f) {
            DestroyNpc();
            if (npcclass.TypeP != npcClass.Type.spyder) {
                Instantiate(SoulsParticlePrefab, transform.position, Quaternion.identity);
            }

        }
        
    }

    void DestroyNpc() {
        if (npcclass.TypeP != npcClass.Type.spyder) {
            Destroy(GetComponentInParent<Transform>().gameObject);
        }
        else {
            Destroy(gameObject);
        }
        
    }

    void spawnHPParticle(int Number)
    {
        #region hp粒子
        while (Number > 0)
        {
            GameObject thisHPparticle = Instantiate(hpParticlePrefab,transform.position,Quaternion.identity) as GameObject;
            Rigidbody2D rigid = thisHPparticle.GetComponent<Rigidbody2D>();
            SpriteRenderer spriteRend = thisHPparticle.GetComponent<SpriteRenderer>();
            hpParticleScript ParticleScript = thisHPparticle.GetComponent<hpParticleScript>();
            rigid.velocity = new Vector2(Function.RandomNumber(12)-4, Function.RandomNumber(10));
            spriteRend.color = new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, (Function.RandomNumber(4)+3)/10f);
            ParticleScript.playerData = playerData;
            Number--;
        }
        #endregion
    }

}
