using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myFunction;
public class OnAwakeSpawnOnce : MonoBehaviour {

    [SerializeField]
    private GameObject npcPrefabs;
    [SerializeField]
    int spawnTimes;
    [SerializeField]
    int dispersion;
    // Use this for initialization
    void Awake() {
        while (spawnTimes-- > 0) { //it's work
            setLocalScale(npcPrefabs);
            Instantiate(npcPrefabs, setPosition(dispersion)  , Quaternion.identity)  ;
            //Debug.Log(Function.RandomNumber(6));
        }
        Destroy(gameObject);
    }
    void setLocalScale(GameObject npcPrefabClone) {
        int faceLocalScale = 0;
        if (Function.RandomNumber(10) > 5) {
            faceLocalScale = 1;
        }
        else {
            faceLocalScale = -1;
        }
        npcPrefabClone.transform.localScale = new Vector3(npcPrefabClone.transform.localScale.x * faceLocalScale, npcPrefabClone.transform.localScale.y, npcPrefabClone.transform.localScale.z);
    }
    Vector3 setPosition(int dispersion) {
        dispersion *= 10;
        //return new Vector3((transform.position.x + Function.RandomNumber(dispersion) - (dispersion / 2)) / 10f, (transform.position.y + Function.RandomNumber(dispersion) - (dispersion / 2)) / 10f, transform.position.z);
        return new Vector3((transform.position.x + Function.RandomNumber(dispersion) - (dispersion / 2)) / 10f, transform.position.y,transform.position.z);
    }
}
