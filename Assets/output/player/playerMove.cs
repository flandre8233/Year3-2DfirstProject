using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {
    npcClass npcclass;
    npcScript npcScript;
    Transform GroundCheckWall1;
    Transform GroundCheckWall2;

    float localScaleX;

    [Range(0, 100)]  [SerializeField] private float movespeed;
    // [SerializeField] private float maxMove;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer;

    bool playerFace = false;  // false to left   true to right
    bool needChangeFace = false;

    Rigidbody2D rb2d;   

    // Use this for initialization
    void Awake() {
        if(transform.localScale.x < 0) {
            localScaleX = -1*transform.localScale.x;
        }
        else {
            localScaleX = transform.localScale.x;
        }
        
        npcclass = GetComponent<npcClass>();
        npcScript = GetComponent<npcScript>();
        rb2d = GetComponent<Rigidbody2D>();
        GroundCheckWall1 = npcScript.GroundCheckWall1;
        GroundCheckWall2 = npcScript.GroundCheckWall2;
    }

    void saveCode() {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKey(vKey)) { //找出自己按什麼鍵
                //your code here
            }
        }
    }

    public void delegateUpdate () {
        /*
        if (Input.GetButton("Abutton")) {
            Debug.Log("A");
        }
        if (Input.GetButton("Bbutton")) {
            Debug.Log("B");
        }
        if (Input.GetButtonUp("Xbutton")) {
            Debug.Log("X");
        }
        */

        //Debug.Log("gkgkgkdad");
        if (npcclass.TypeP == npcClass.Type.contorl) {
            jump();
            faceDetect();
            movement();
        }
    }

    void movement() {
        //Debug.Log(npcclass.movementStateP);
        if (npcclass.movementStateP == npcClass.movementState.falling) {
        }
        //&& (Physics2D.OverlapCircle(GroundCheckLeft.position, 0.35f, groundLayer))

        float movementX = Input.GetAxisRaw("Horizontal");

        if (((Physics2D.OverlapCircle(GroundCheckWall1.position, 0.15f, groundLayer)) || (Physics2D.OverlapCircle(GroundCheckWall2.position, 0.15f, groundLayer)))) {
            //movementX = 0;
        }
        else if (npcclass.CastAniP == npcClass.CastAni.onMovement) {
            rb2d.velocity = new Vector2(movementX * (movespeed ) , rb2d.velocity.y); //角色移動
        }
        //Debug.Log(rb2d.velocity);

    }

    void jump() {
        if (Input.GetButtonDown("JumpYbutton")) {
            if (npcclass.movementStateP != npcClass.movementState.jumpingBothCanMove && npcclass.movementStateP != npcClass.movementState.falling) {
                //transform.Translate(Vector3.up * 260 * Time.deltaTime, Space.World);
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(transform.up * jumpHeight / 54, ForceMode2D.Impulse);  //jump
                npcclass.movementStateP = npcClass.movementState.jumpingBothCanMove;
            }
        }
    }

    void faceDetect() {
        if (npcclass.CastAniP == npcClass.CastAni.onMovement) {
            if (Input.GetAxisRaw("Horizontal") >= 0.01) {
                //playerFace = true;
                transform.localScale = new Vector3(-localScaleX, transform.localScale.y, transform.localScale.z);
                //flip();
            }
            else if (Input.GetAxisRaw("Horizontal") <= -0.01) {
                //playerFace = false;
                transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
                //flip();
            }
        }
    

    }

    void flip() {
        //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
    }
}

