using UnityEngine;
using System.Collections;

public class droneScript : MonoBehaviour {
    [SerializeField]GameObject shotPoint;
    gunSpawn shotBulletScript;

    public float CD = 0.5F;
    bool attackCDLock = false;



    // Use this for initialization
    void Start() {
        shotBulletScript = shotPoint.GetComponent<gunSpawn>();

    }

    // Update is called once per frame
    void Update() {

            if (!attackCDLock && shotPoint.active) {
                StartCoroutine("attackFunction");
            }


    }

    IEnumerator attackFunction() {
        if (!attackCDLock) {
            attackCDLock = true;
            shotBulletScript.DroneShot();
        }

        


        yield return new WaitForSeconds(CD);
        attackCDLock = false;
    }



}

