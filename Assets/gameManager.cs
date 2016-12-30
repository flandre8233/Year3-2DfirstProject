using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPlayerGameOver() {

        Debug.Log("ggggg");
        GameObject[] playerGhost = GameObject.FindGameObjectsWithTag("Player"); //

        foreach (GameObject each in playerGhost) {
            Destroy(each);
        }

        //playerDataClass playerData = GameObject.FindGameObjectsWithTag("backgroundScipt")[0].GetComponent<playerDataClass>(); //
        //playerData.playerSouls = 0;

        GameObject winLoseMenu = GameObject.FindGameObjectsWithTag("Menu/win-LoseMenu")[0];
        GameObject LoseMenu = winLoseMenu.transform.GetChild(0).gameObject; //顯示輸掉畫面
        LoseMenu.SetActive(true);
        /*
        do {
            LoseMenu.GetComponent<Renderer>().material.color = new Color(1,1,1,0);
        }
        while(){
            LoseMenu.GetComponent<Renderer>().material.color = new Color(1,1,1,Time.deltaTime/fadeInTime );
        }
        */


    }



}
