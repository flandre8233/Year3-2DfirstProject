using UnityEngine;
using System.Collections;

public class playerGhostMove : MonoBehaviour {

    enum charState { landed, walking, flying };
    charState playerState;

    public float movespeed;

    bool playerFace = false;  // false to left   true to right
    bool needChangeFace = false;

    Rigidbody2D rb2d;
    Transform ts;
    SpriteRenderer SR;

    

    // Use this for initialization
    void Start() {
        
        ts = GetComponent<Transform>();
        SR = GetComponent<SpriteRenderer>();

    }
    void OnEnable() {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
    }

    // Update is called once per frame
    void Update() {

        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKey(vKey)) {
                //your code here
                //Debug.Log(vKey.ToString());
            }
        }


        // Debug.Log(Input.GetAxis("JOY1"));
        

        if (Input.GetButtonUp("Xbutton")) {
            Debug.Log("X");
        }
        if (Input.GetButtonUp("Ybutton")) {
            
        }


        if (Input.GetButton("Abutton")) {
            Debug.Log("A");
        }
        if (Input.GetButton("Bbutton")) {
            Debug.Log("B");
        }

        if (Input.GetAxisRaw("Horizontal") >= 0.01 && playerFace != true) {
            playerFace = true;
            needChangeFace = true;
        }
        else if (Input.GetAxisRaw("Horizontal") <= -0.01 && playerFace != false) {
            playerFace = false;
            needChangeFace = true;
        }


        if (needChangeFace) {
            needChangeFace = false;
            ts.localScale = new Vector3(ts.localScale.x * -1, ts.localScale.y, 1);

        }

        // playerStateSet
        if ((rb2d.velocity.x > 0.1 && rb2d.velocity.y <= 0.01) || (rb2d.velocity.x < -0.1 && rb2d.velocity.y >= -0.01)) {
            playerState = charState.walking;
        }
        else {
            playerState = charState.landed;
        }

        //Debug.Log(rb2d.velocity);
        //Debug.Log(playerState.ToString());

        Vector3 v3 = new Vector3(movementX, movementY,0 );
        rb2d.AddForce(v3 * movespeed * Time.deltaTime, ForceMode2D.Impulse);
        //ts.position += v3 * speed * Time.deltaTime;
    }
}
