using UnityEngine;
using System.Collections.Generic;
using myFunction;

public class selectEnemySystemScript : GameFunction
{
    playerContorl playercontorl;

    [HideInInspector]
    public GameObject[] GB; //･ﾘｼﾐｩﾒｦbｪｺarray
    public GameObject playerSelectPointerSystem; //ｨ﨑oselectPointerMovementｵ{ｦ｡ ､ｺｮe
    //public GameObject selectPointerUIpart;


    [SerializeField][HideInInspector]
    Transform pointer;  //･Hｨｺｭﾓｹﾏｧ@?Ew ｶ}ｩlｧ莎ﾌｪｺ?EE
    [SerializeField][HideInInspector]
    pointerTrigger OnTriggerEnter2DCircle;
    [SerializeField]
    public float slowMotionTimeScale;
    [SerializeField]
    public float timeToComplete;
    [SerializeField]
    public int controlHpCost;
    [SerializeField]
    Object possessedPacticlePrefab;


    GameObject[] TriggerArray;

    public GameObject targetGameObj;  //､｣ｬO､箍ﾊｩi･h
    //sadadssadsadsad
    public GameObject ggggggggggg;

    int selectTaget = 1;

    float[] eachEnemylerpFloat;

    int target = -1;
    float timerCount = 0.0f;

    public bool openTargetLockDown = false;

    // Use this for initialization
    void Start() {
        playercontorl = GetComponent<playerContorl>();
        TriggerArray = OnTriggerEnter2DCircle.TriggerList.ToArray();

        playerSelectPointerSystem.transform.position = transform.position;
        playerSelectPointerSystem.SetActive(false);
        //selectPointerUIpart.SetActive(false);
        GB = getallenemyWithoutContorlOnce();
    }
    
    void startTargetLockDown(Vector3 centerObjectPosition, int targetNum) {
        #region 進: ?心點,離?個?心點第幾個目標 出:targetGameObj
        GB = TriggerArray; //ｨﾏ･ﾎｨｺｭﾓenemyArray?

        if (GB.Length > 0) { //TriggerArray､ｺｦｳｪF?E
            eachEnemylerpFloat = new float[GB.Length];  //､ﾆｦｨｨCｭﾓｼﾄ､H?EEwｬﾛｮtｦh､ﾖ?
            short i = 0;
            foreach (GameObject each in GB) {
                // each.GetComponent<SpriteRenderer>().color = Color.white;
                eachEnemylerpFloat[i] = Vector3.Distance(centerObjectPosition, each.GetComponent<Transform>().position);

                //eachEnemylerpFloat[i] = Vector3.Distance(centerObjectPosition, each.GetComponent<Transform>().position);
                i++;
            }

            target = Function.findSmallestOfBigestNumberInArray(eachEnemylerpFloat, false, targetNum);  //?出eachEnemylerpFloat array?第幾個最小數?

            targetGameObj = GB[target]; //ｧ筵ﾘｼﾐｧ茹Xｨﾓ
            //targetGameObj.GetComponent<SpriteRenderer>().color = Color.red;

            foreach (GameObject each in getallenemyWithoutContorlOnce()) {  //ﾁﾙ?EﾒｦｳenemyｪｺﾃC?Ewithout targetgame  ･iｦ讌i･ﾎ
                if (each.GetInstanceID() != targetGameObj.GetInstanceID() ) {
                    //each.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }

        }
        else {
            foreach (GameObject each in getallenemyWithoutContorlOnce()) {  //ﾁﾙ?EﾒｦｳenemyｪｺﾃC?E
                //each.GetComponent<SpriteRenderer>().color = Color.white;

            }
        }


        //openTargetLockDown = true;
        #endregion
    }


    public void setupTargetLockDown() {  //
        transform.rotation = Quaternion.Euler(0,0,0);
        Time.timeScale = slowMotionTimeScale;  //ｴ鋓Cｾ罸ﾓｹCﾀｸｳtｫﾗ
        //playercontorl.incontorlObj.GetComponent<playerMove>().enabled = false;
        targetGameObj = null;
   
        playerSelectPointerSystem.SetActive(true);  //ｶ}ｱﾒ?Eﾜｶ・
        //selectPointerUIpart.SetActive(true);
        GB = TriggerArray;
        openTargetLockDown = true;
    }

    public void cancelTargetLockDown() {  //ｨ峵unction
        ggggggggggg.SetActive(false);
        //GB = TriggerArray;
        Time.timeScale = 1f;
        //playercontorl.incontorlObj.GetComponent<playerMove>().enabled = true;
        playerSelectPointerSystem.SetActive(false);
        //selectPointerUIpart.SetActive(false);
        openTargetLockDown = false;

        if (targetGameObj!=null) {
            //targetGameObj.GetComponent<SpriteRenderer>().color = Color.white;
        }

        target = -1;


    }

    // Update is called once per frame
    void Update() {
        
        if (gameStateDataClass.staticGameStateDataClass.gamestate != gameStateDataClass.gameState.pause && playercontorl.incontorlObj ) {
            if (Input.GetButtonDown("OpenCloseControlPreview")) {

                if (openTargetLockDown) {  //ｵｲ?EEﾜｱｱｨ・
                    cancelTargetLockDown();

                }
                else {  //ｶ}ｩl?Eﾜｱｱｨ・
                    playerDataClass playerData = GameObject.FindGameObjectsWithTag("backgroundScipt")[0].GetComponent<playerDataClass>();
                    if (playerData.HP - controlHpCost > 0) { //要消耗hp才能發動
                        soundEffectManager.staticSoundEffect.play_possessedOnOpen();
                        Instantiate(possessedPacticlePrefab, transform.position, Quaternion.identity);

                        playerData.HP -= controlHpCost;
                        setupTargetLockDown();
                        timerCount = 0;
                    }

                }
            }

        }

        

        if (openTargetLockDown) {

            TriggerArray = OnTriggerEnter2DCircle.TriggerList.ToArray();
            //Vector3 pointerV3 = pointer.position;
            Camera Ccamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            Vector3 pointerV3 = Ccamera.ScreenToWorldPoint(Input.mousePosition);
            //startTargetLockDown(pointerV3, 0);
            startTargetLockDown(pointerV3, 0);
            if (GB.Length > 0 )
            {
                ggggggggggg.SetActive(true);
                ggggggggggg.transform.position = targetGameObj.transform.position;
            }else
            {
                ggggggggggg.SetActive(false);
                //ggggggggggg.transform.position = targetGameObj.transform.position;
            }

            if (timerCount <= timeToComplete * slowMotionTimeScale ) { //?間計算系? 已算進slowMotion的?間差問題
                timerCount += Time.deltaTime;
            }
            else { //過了?間就自動取消
                cancelTargetLockDown();
            }

        }
        
        #region 廢棄code
        /*
        if (Input.GetKeyDown(KeyCode.LeftArrow) && openTargetLockDown) {
            if (selectTaget == 0) {
                selectTaget = eachEnemylerpFloat.Length - 1;
            }
            else {
                selectTaget--;
            }
            startTargetLockDown(getLeftestPointer.transform.position,selectTaget);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)&& openTargetLockDown) {



            if (eachEnemylerpFloat.Length-1 == selectTaget) {
                selectTaget = 0;
            }
            else {
                selectTaget++;
            }
            startTargetLockDown(getLeftestPointer.transform.position, selectTaget);
        }
        */

        //Debug.Log(selectTaget);

        /*
        if (Input.GetKeyUp(KeyCode.U)) {
            if (eachEnemylerpFloat.Length == selectTaget) {
                selectTaget = 0;
            }
            else {
                selectTaget++;
            }

        }
        */


        //Debug.Log(eachEnemylerpFloat[0]);




        //Debug.Log(myfunction.findSmallestOfBigestNumberInArray(testfloat, false,3));

        //ｶ}ｵo､@ｭﾓ･ｪ･kｶsｨﾓ?Eﾜﾂ{ｦb･ﾘｼﾐｳﾌｪｺ･t､@ｰｦｩﾇｪｫ

        #endregion
    }
}
