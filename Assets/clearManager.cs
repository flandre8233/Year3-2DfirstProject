using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clearManager : MonoBehaviour
{
    public Text soulsText;
    public Text timeText;
    bool write = false;

    public void OnEnable() {
        soulsText.text = playerDataClass.staticData.playerSouls.ToString();
        if (!write) {
            write = true;
            timeText.text = (gameManager.staticgameManager.timeCounter) + "";
        }
        
    }



    // Use this for initialization
    void Start() {


    }

    // Update is called once per frame
    void Update() {
        
    }
}
