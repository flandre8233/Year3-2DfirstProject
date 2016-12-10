using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {

    private Vector2 velocity;

    public float smoothTimeX;
    public float smoothTimeY;

    public Transform playerTransform;

	// Update is called once per frame
	void  FixedUpdate () {
        if (GameObject.FindGameObjectsWithTag("Player").Length != 0) {
            float posX = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x, ref velocity.x, smoothTimeX,100,Time.deltaTime);
            float posY = Mathf.SmoothDamp(transform.position.y, playerTransform.position.y, ref velocity.y, smoothTimeY , 100, Time.deltaTime);




            transform.position = new Vector3(posX, posY, transform.position.z);
        }
        
    }


}
