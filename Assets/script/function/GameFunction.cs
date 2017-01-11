using UnityEngine;
using System.Collections.Generic;

public class GameFunction : MonoBehaviour {

    public GameObject[] getallenemyWithoutContorlOnce() { //取所有敵人 除了被控制的
        GameObject[] GB = GameObject.FindGameObjectsWithTag("enemy");
        List<GameObject> GBWithoutContorl = new List<GameObject>();

        foreach (GameObject each in GB) {
            if (each.GetComponent<npcClass>().TypeP != npcClass.Type.contorl) {
                GBWithoutContorl.Add(each);
            }
        }

        return GBWithoutContorl.ToArray();

    }

    public Vector2 getCameraToV2(Camera camera) {
        Vector3 mousePosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));

        return new Vector2(mousePosition.x, mousePosition.y);
    }
    public Vector3 getCameraToV3(Camera camera) {
        Vector3 mousePosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
        Vector3 mousePosition2d = new Vector3(mousePosition.x, mousePosition.y, 0);

        return mousePosition2d;
    }

    public Quaternion ImageLookAt2D(Vector3 from,Vector3 to) {
        Vector3 difference = to - from;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = (Quaternion.Euler(0.0f, 0.0f, rotationZ));
        return rotation;
    }

    public void OnPlayerGameOver() {
        
        Debug.Log("ggggg");
        GameObject[] playerGhost  = GameObject.FindGameObjectsWithTag("Player"); //
        backgroundMusicScript.staticBackground.playOnDead();
        foreach (GameObject each in playerGhost) {
            Destroy(each);
        }
        GameObject gameCanvas = GameObject.FindGameObjectsWithTag("Menu/game-Canvas")[0];
        gameCanvas.SetActive(false);


        GameObject winLoseMenu = GameObject.FindGameObjectsWithTag("Menu/win-LoseMenu")[0];
        GameObject LoseMenu  = winLoseMenu.transform.GetChild(0).gameObject; //顯示輸掉畫面
        LoseMenu.SetActive(true);


    }
    /*
    IEnumerator void fadeIntest() {
        yield break 0;
    }
    */



}
