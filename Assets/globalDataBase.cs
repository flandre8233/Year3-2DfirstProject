using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalDataBase : MonoBehaviour {

    public int curLevel = 0;
    int maxLevel = 25;

    public class levelDetails
    {
        public string levelName = "";
        public bool isLocked = true;
        public int starNumber = 0;
    }

    public List<levelDetails> allLevelList ;


    void Awake() {
        if (GameObject.FindGameObjectsWithTag("globalDataBase").Length == 1) {
            Debug.Log("reset");
            allLevelList = new List<levelDetails>();
            while (maxLevel-- >= 0) {
                allLevelList.Add(new levelDetails());
            }
            allLevelList[0 + 2].isLocked = false;
            DontDestroyOnLoad(transform.gameObject);
            
        }
        else {
            Destroy(gameObject);
        }
    }


    // Use this for initialization
    void Start () {
        
        
    }
	
	// Update is called once per frame
	void Update () {

    }
}
