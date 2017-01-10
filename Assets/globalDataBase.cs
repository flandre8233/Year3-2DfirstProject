using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalDataBase : MonoBehaviour {
    public static globalDataBase staticData;


    public int curLevel = 0;
    int maxLevel = 25;

    public class levelDetails
    {
        public string levelName = "";
        public bool isLocked = true;
        public int starNumber = 0;
        public int souls = 0;
        public float time = 0;
    }

    public List<levelDetails> allLevelList ;


    void Awake() {
        if (staticData != null) {
            DontDestroyOnLoad(transform.gameObject);
        }
        else {
            staticData = this;
        }

        if (GameObject.FindGameObjectsWithTag("globalDataBase").Length == 1) {
            Debug.Log("reset");
            allLevelList = new List<levelDetails>();
            while (maxLevel-- >= 0) {
                allLevelList.Add(new levelDetails());
            }
            allLevelList[0 + 2].isLocked = false;
        }
    }


    // Use this for initialization
    void Start () {
        
        
    }
	
	// Update is called once per frame
	void Update () {

    }
}
