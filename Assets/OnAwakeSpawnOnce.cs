using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAwakeSpawnOnce : MonoBehaviour {

    [SerializeField]
    private GameObject npcPrefabs;
    [SerializeField]
    int spawnTimes;
    // Use this for initialization
    void Start () {

        while (--spawnTimes < 0) {

        } 

        Destroy(gameObject);	
	}
    
}
