using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {
    public static gameManager staticgameManager;
    gameStateDataClass gameData;
    public float timeCounter;

    void Awake() {
        staticgameManager = this;
    }


    public float distance = 0.0f;
    float findDistance() {
        Transform playerV3 = transform;
        foreach (var item in GameObject.FindGameObjectsWithTag("enemy")) {
            if (item.GetComponent<npcClass>().TypeP == npcClass.Type.contorl) {
                playerV3 = item.transform;
                break;
            }
        }

        Transform goal = GameObject.FindGameObjectWithTag("goal").transform;
        return Mathf.Abs(goal.position.x - playerV3.position.x);
    }


    // Use this for initialization
    void Start () {
        distance = findDistance();


        gameData = GetComponent<gameStateDataClass>();
        startTotalTarget = countTotalTarget();


    }

    // Update is called once per frame
    void Update() {
        timeCounter += Time.deltaTime;
        if (Input.GetMouseButtonUp(1)) {
            //Debug.Log(countTotalTarget());
        }

        if (gameData.gamestate == gameStateDataClass.gameState.game) {
            if (playerDataClass.staticData.HP + 0.05f * Time.deltaTime <= playerDataClass.staticData.MAXHP) {
                playerDataClass.staticData.HP += 0.05f * Time.deltaTime;
            }
        }


    }

    public float Percentage;
    public int startTotalTarget = 0;
    public int curTotalTarget = 0;
    public int countTotalTarget() {
        int totalTarget = 0;
        GameObject[] gameObj = GameObject.FindGameObjectsWithTag("enemy");  //無視其他enemy碰撞
        foreach (GameObject each in gameObj) {
            if (each.GetComponent<npcClass>().TypeP == npcClass.Type.normal && each.GetComponent<npcClass>().liveStateP != npcClass.liveState.dead ) {
                totalTarget++;
            }
        }
        return totalTarget;
    }

    void killPercentageCheck() {
        curTotalTarget = countTotalTarget();

        Percentage = (((float)startTotalTarget - 1.0f) - (float)curTotalTarget) / ((float)startTotalTarget - 1.0f) * 100.0f;
        Debug.Log(Percentage + "%");
        if (Percentage >= 90.0f) {
            gameData.starNumber = 3;
        }
        else if (Percentage >= 70.0f) {
            gameData.starNumber = 2;
        }
        else if (Percentage >= 50.0f) {
            gameData.starNumber = 1;
        }
        else {
            gameData.starNumber = 0;
        }

    }

    void starNumberCheck() {
        if (score >= 90.0f) {
            gameData.starNumber = 3;
        }
        else if (score >= 75.0f) {
            gameData.starNumber = 2;
        }
        else if (score >= 60.0f) {
            gameData.starNumber = 1;
        }
        else {
            gameData.starNumber = 0;
        }

    }

    float score = 0;
    public void OnPlayerWin() {
        if (backgroundMusicScript.staticBackground != null) {
            backgroundMusicScript.staticBackground.playClearLevel();
        }

        curTotalTarget = countTotalTarget();
        score = scoreCounter(startTotalTarget-curTotalTarget, startTotalTarget, distance, timeCounter);
        //killPercentageCheck();
        starNumberCheck();
        Debug.Log(score);
        GameObject gameCanvas = GameObject.FindGameObjectsWithTag("Menu/game-Canvas")[0];
        gameCanvas.SetActive(false);

        GameObject winLoseMenu = GameObject.FindGameObjectsWithTag("Menu/win-LoseMenu")[0];
        GameObject winMenu = winLoseMenu.transform.GetChild(1).gameObject;
        winMenu.SetActive(true);



        if (globalDataBase.staticData != null) { //更新關卡數值
            if (globalDataBase.staticData.allLevelList[globalDataBase.staticData.curLevel].starNumber < gameData.starNumber) {
                globalDataBase.staticData.allLevelList[globalDataBase.staticData.curLevel].starNumber = gameData.starNumber; //要放在on完成通關那邊
            }
            if (globalDataBase.staticData.allLevelList[globalDataBase.staticData.curLevel].starNumber != 0)
            {
                globalDataBase.staticData.allLevelList[globalDataBase.staticData.curLevel + 1].isLocked = false; //要放在on完成通關那邊
            }
            if (globalDataBase.staticData.allLevelList[globalDataBase.staticData.curLevel + 1].souls < playerDataClass.staticData.playerSouls) {
                globalDataBase.staticData.allLevelList[globalDataBase.staticData.curLevel + 1].souls = playerDataClass.staticData.playerSouls;
            }
            if (  globalDataBase.staticData.allLevelList[globalDataBase.staticData.curLevel + 1].time < timeCounter) {
                globalDataBase.staticData.allLevelList[globalDataBase.staticData.curLevel + 1].time = timeCounter;
            }

            if (globalDataBase.staticData.allLevelList[globalDataBase.staticData.curLevel].score < score) {
                globalDataBase.staticData.allLevelList[globalDataBase.staticData.curLevel].score = score ; //要放在on完成通關那邊
            }

        }

    }

    public float scoreCounter(int soul,int enemyTotalNumber,float distance,float time) {
        //Debug.Log(soul + "soul");
        //Debug.Log(enemyTotalNumber + "enemyTotalNumber");
        //Debug.Log(distance + "distance");
        //Debug.Log(time + "time");
        float score = ((soul / (enemyTotalNumber * 1.0f ) ) * 35.0f) + ((distance / time) / 13.0f * 65.0f);
        return score;


    }


    void OnDestroy() {


    }

}
