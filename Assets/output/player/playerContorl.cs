using UnityEngine;
using System.Collections;
using Spine.Unity;
public class playerContorl : GameFunction {
    private selectEnemySystemScript lockDownSystem;

    SpriteRenderer SP;

    public bool inContorl;
    private float journeyLength;

    bool alphaLock = true;

    [SerializeField][HideInInspector]
    Transform pointerTransform;

    [SerializeField]
    float ghostMoveMentSmoothTime;
    [SerializeField]
    float GhostMovementSpeed;
    [SerializeField]
    public GameObject incontorlObj;
    [SerializeField]
    GameObject SoulPacticle;

    // Use this for initialization

    float timer = 0.0f;
    public float timeLimit ;

    float CDSave = 0.0f;

    int counter = 0;

    void controlEnemyFunction(GameObject GameEnemy) {  //控制這裡
        counter++;
        if (counter != 1) {
            soundEffectManager.staticSoundEffect.play_possessedOnFinish();
        }
        //GameEnemy.GetComponent<playerMove>().enabled = true;

        //GameEnemy.GetComponent<npcMove>().enabled = false;
        //GameEnemy.GetComponent<SpriteRenderer>().color = Color.blue;
        //Debug.Log(GameEnemy.GetComponent<SpriteRenderer>().color);
        GameEnemy.GetComponents<npcClass>()[0].TypeP = npcClass.Type.contorl;

        //incontorlObj.tag = "contorl";
        
        if (GameEnemy.GetComponentInChildren<attackSystem>() != null) {
            CDSave = GameEnemy.GetComponentInChildren<attackSystem>().CD ;
            GameEnemy.GetComponentInChildren<attackSystem>().enabled = true;
            GameEnemy.GetComponentInChildren<attackSystem>().CD = CDSave * 0.5f;
        }
            
        lockDownSystem.cancelTargetLockDown();
        inContorl = true;
    }

    void Start() {
        GetComponent<Collider2D>().enabled = false;
        SoulPacticle.SetActive(false);
        lockDownSystem = GetComponent<selectEnemySystemScript>();
        //pm = GetComponents<playerMove>()[0];
        //GM = GetComponents<playerGhostMove>()[0];
        // SP  = GetComponent<SpriteRenderer>();


        //GM.enabled = true;
        //GetComponent<Rigidbody2D>().gravityScale = 0;

        incontorlObj = lockDownSystem.targetGameObj;
        //transform.position = incontorlObj.transform.position;
        controlEnemyFunction(incontorlObj);
    }

    // Update is called once per frame
    void Update() {
        SoulPacticle.SetActive(!inContorl);
        #region movement
        /*
        if (Input.GetKeyUp(KeyCode.X)) {
            if (test) {
                GM.enabled = true;
                pm.enabled = false;

                test = false;
            }
            else {
                pm.enabled = true;
                GM.enabled = false;

                test = true;
            }
        }
        */
        #endregion movement

        timer += Time.deltaTime;
        //Debug.Log(timer);

        /*
        if(timer >= timeLimit) {  //GAMEOVER HERE   
            Destroy(this.gameObject);
            Debug.Log("gg");
        }
        */

        //ControlEnemyShotSouls
        if (lockDownSystem.openTargetLockDown && lockDownSystem.GB.Length > 0) {
            if (Input.GetButtonDown("ControlEnemyShotSouls")) {  //click do once
                

                
                //transform.rotation = (Gfunction.ImageLookAt2D(transform.position, pointerVector3));
                lockDownSystem.cancelTargetLockDown();
                Time.timeScale = 1f;
                inContorl = false;

                
                
                //incontorlObj.GetComponent<npcMove>().enabled = true;
                //GB[i].tag = "enemy";
                incontorlObj.GetComponents<npcClass>()[0].TypeP = npcClass.Type.normal;
                //incontorlObj.GetComponent<SpriteRenderer>().color = Color.white;
                //incontorlObj.GetComponent<playerMove>().enabled = false;
                incontorlObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                if (incontorlObj.GetComponentInChildren<attackSystem>() != null) {
                    incontorlObj.GetComponentInChildren<attackSystem>().enabled = false;
                    incontorlObj.GetComponentInChildren<attackSystem>().CD = CDSave;
                }
            }
        }

        if (inContorl) {
            transform.position = incontorlObj.transform.position;
            if (GetComponent<Collider2D>().enabled) {
                GetComponent<Collider2D>().enabled = false;
            }

        }
        else {
            if (!GetComponent<Collider2D>().enabled) {
                GetComponent<Collider2D>().enabled = true;
            }

            //journeyLength = Vector3.Distance(transform.position, incontorlObj.GetComponent<Transform>().position);
            //float fracJourney = Time.deltaTime * 1 / journeyLength;
            //transform.position = Vector3.Lerp(transform.position, keyUpPointerPosition, fracJourney);

            //

            //float posX = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x, ref velocity.x, smoothTimeX, 100, Time.deltaTime);
            //float posY = Mathf.SmoothDamp(transform.position.y, playerTransform.position.y, ref velocity.y, smoothTimeY, 100, Time.deltaTime);

            //先前的移動系統
            transform.rotation = ImageLookAt2D(transform.position, lockDownSystem.targetGameObj.transform.position);
            transform.position += transform.right * Time.deltaTime * GhostMovementSpeed;

            //transform.position = new Vector3(transform.position.x,transform.position.y,0);
        }




        //float fracJourney = Time.deltaTime * 45 / journeyLength;
        //selfTransform.position = Vector3.Lerp(selfTransform.position, incontorlObj.GetComponent<Transform>().position, fracJourney);  //靈體移動至被控制單位

        //SP.color = Color.Lerp(SP.color, new Color(1, 1, 1, 0), Time.deltaTime * 50 / (journeyLength / 2));

        /*
        if (lockDownSystem.openTargetLockDown && lockDownSystem.GB.Length > 0) {
            if (Input.GetKeyUp(KeyCode.Y)) {  //click do once
                SP.color = new Color(1, 1, 1, 1);
                GameObject[] GB = GameObject.FindGameObjectsWithTag("enemy");
                alphaLock = false;  //進入alpha
                timer = 0;

                for (int i = 0; i <= GB.Length - 1; i++) {
                    if (lockDownSystem.targetGameObj.GetInstanceID() != GB[i].GetInstanceID()) {
                        GB[i].GetComponent<playerMove>().enabled = false;
                        GB[i].GetComponent<attackSystem>().enabled = false;
                        //GB[i].tag = "enemy";
                        GB[i].GetComponents<npcClass>()[0].TypeP = npcClass.Type.normal;
                        GB[i].GetComponent<SpriteRenderer>().color = Color.white;

                    }
                }


                incontorlObj = lockDownSystem.targetGameObj;
                Debug.Log(incontorlObj.GetComponent<npcClass>().WeaponP.ToString());
                incontorlObj.GetComponent<playerMove>().enabled = true;
                incontorlObj.GetComponent<attackSystem>().enabled = true;
                incontorlObj.GetComponent<SpriteRenderer>().color = Color.blue;
                incontorlObj.GetComponents<npcClass>()[0].TypeP = npcClass.Type.contorl;
                journeyLength = Vector3.Distance(selfTransform.position, incontorlObj.GetComponent<Transform>().position);
                //incontorlObj.tag = "contorl";
                lockDownSystem.cancelTargetLockDown();
                GM.enabled = false;
                inContorl = true;
            }
        }



        /*
        if (!alphaLock) {
            Alphafunction(!inContorl);
            Debug.Log("sadsadsa");
        }

    */




    }

    /*
    void Alphafunction(bool addOrDecrease) {
        SpriteRenderer SP = GetComponent<SpriteRenderer>();

        if (addOrDecrease) {  //alpha要增加
            if (SP.color.a < 1) {
                SP.color = new Color(SP.color.r, SP.color.g, SP.color.b, SP.color.a + 0.1f);
            }
            else {
                SP.color = new Color(SP.color.r, SP.color.g, SP.color.b, 1);
                alphaLock = true;
            }
        }
        else {  //alpha 要減少
            if (SP.color.a > 0) {
                SP.color = new Color(SP.color.r, SP.color.g, SP.color.b, SP.color.a - 0.1f);
            }
            else {
                SP.color = new Color(SP.color.r, SP.color.g, SP.color.b, 0);
                alphaLock = true;
            }

        }
    }
     */

    [SerializeField]
    Object blooduseParticle;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == lockDownSystem.targetGameObj && other.gameObject.tag == "enemy" && !inContorl && other.gameObject != incontorlObj) {
            //Debug.Log("ssssasdasd");

            if (other.gameObject.GetComponent<npcClass>().TypeP != npcClass.Type.contorl && other.gameObject.GetComponent<npcClass>().liveStateP != npcClass.liveState.dead) {   //還原
                
                incontorlObj = other.gameObject;
                Instantiate(blooduseParticle,transform.position,Quaternion.identity);
                inContorl = true;

                lockDownSystem.cancelTargetLockDown();
                controlEnemyFunction(incontorlObj);

            }
            
        }
        
    }
}
