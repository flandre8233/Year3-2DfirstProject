using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {

    private Vector2 velocity;

    public float smoothTimeX;
    public float smoothTimeY;

    public Transform playerTransform;
    public Transform globalVoidKillZone;

    // Update is called once per frame
    void  FixedUpdate () {
        if (GameObject.FindGameObjectsWithTag("Player").Length != 0 && playerTransform.position.y >= globalVoidKillZone.position.y+18)  {
            float posX = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x+10,ref velocity.x, smoothTimeX,100,Time.deltaTime);
            float posY = Mathf.SmoothDamp(transform.position.y, playerTransform.position.y +5 ,ref velocity.y, smoothTimeY , 100, Time.deltaTime);




            transform.position = new Vector3(posX, posY, transform.position.z);
        }
        
    }


}
