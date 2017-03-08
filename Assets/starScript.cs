using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starScript : MonoBehaviour
{
    public int thisObjectStarLevel;
    public GameObject starObj;

    bool once = false;


    // Update is called once per frame
    void Update() {
        if (!once) {
            if (gameStateDataClass.staticGameStateDataClass != null) {
                if (gameStateDataClass.staticGameStateDataClass.starNumber >= thisObjectStarLevel) {
                    starObj.SetActive(true);
                    once = true;
                }
            }

        }


    }
}
