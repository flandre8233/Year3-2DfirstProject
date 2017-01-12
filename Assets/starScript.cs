using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starScript : MonoBehaviour
{
    public gameStateDataClass gameStateDataClass;
    public int thisObjectStarLevel;
    public GameObject starObj;

    bool once = false;


    // Update is called once per frame
    void Update() {
        if (!once) {
            gameStateDataClass = GameObject.FindGameObjectsWithTag("backgroundScipt")[0].GetComponent<gameStateDataClass>();
            if (gameStateDataClass.starNumber >= thisObjectStarLevel) {
                starObj.SetActive(true);
            }

            once = true;
        }


    }
}
