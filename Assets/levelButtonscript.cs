using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class levelButtonscript : MonoBehaviour
{
    [SerializeField]
    int level;
    [SerializeField]
    public List<Sprite> LockedLevelButtonSpriteList;
    [SerializeField]
    public List<Sprite> UnlockLevelButtonSpriteList;
    [SerializeField]
    public List<Sprite> starSpriteList;
    [SerializeField]
    public List<Image> starImageList;
    [SerializeField]
    public int starNumber;
    [SerializeField]
    public bool islocked;

    globalDataBase globalDataBase;

    // Use this for initialization
    void Start() {

        buttonSetUp();
    }

    // Update is called once per frame
    void Update() {

    }

    public void buttonSetUp() {
        GameObject findObject = GameObject.Find("globalDataBase");
        globalDataBase = findObject.GetComponent<globalDataBase>();
        islocked = globalDataBase.allLevelList[level].isLocked;
        starNumber = globalDataBase.allLevelList[level].starNumber;

        if (islocked) {
            GetComponentInChildren<Image>().sprite = LockedLevelButtonSpriteList[level - 2];
            GetComponent<Button>().enabled = false;

        }
        else {
            GetComponentInChildren<Image>().sprite = UnlockLevelButtonSpriteList[level - 2];
            GetComponent<Button>().enabled = true;

            while (starNumber-- > 0) {
                starImageList[starNumber].sprite = starSpriteList[1];
            }
        }

    }

}
