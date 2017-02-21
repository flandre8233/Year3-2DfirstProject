using UnityEngine;
using System.Collections;

public class selectPointerMovement : GameFunction
{
    public Camera camera;

    Vector3 mouseV3;

    public GameObject playerSelf;

    // Update is called once per frame
    void Update() {

        mouseV3 = getCameraToV3(camera);
        //Debug.Log(lockDownTarget.transform);

        transform.rotation = (ImageLookAt2D(transform.position,mouseV3) );

    }


}
