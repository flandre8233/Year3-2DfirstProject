using UnityEngine;
using System.Collections;

public class backgroundPic : MonoBehaviour {
    public GameObject camera;

    public float scrollSpeed;
    float tileSizeZ;

    private Vector3 startPosition;

    void Start() {
        tileSizeZ = transform.localScale.y * 10;
        startPosition = transform.localPosition;
    }

    void Update() {
        float distance =  Vector3.Distance(camera.transform.position,Vector3.zero);
        float newPosition = Mathf.Repeat(-distance * (scrollSpeed/100), tileSizeZ);
        transform.localPosition = startPosition + (Vector3.right * Time.deltaTime) * newPosition ;
    }
}
