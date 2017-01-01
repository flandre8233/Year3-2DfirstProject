using UnityEngine;
using System.Collections;

public class hpParticleScript : MonoBehaviour {
    [SerializeField]
    public playerDataClass playerData;
    [SerializeField]
    public playerContorl playercontorl;
    [SerializeField]
    public float hp;
    float timeCount=0.0f;
    // Use this for initialization
    GameObject incontorlObj;
    void Start () {
        playercontorl = (playerContorl)FindObjectOfType(typeof(playerContorl)); //找出場景內第一個playerContorl
        GameObject[] gameObj = GameObject.FindGameObjectsWithTag("HPpartacle");  //無視其他enemy碰撞
        foreach (GameObject each in gameObj)
        {
            Physics2D.IgnoreCollision(each.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        GameObject[] enemygameObj = GameObject.FindGameObjectsWithTag("enemy");  //無視其他enemy碰撞
        foreach (GameObject each in enemygameObj)
        {
            Physics2D.IgnoreCollision(each.GetComponent<BoxCollider2D>(), GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(each.GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
        }

    }

    // Update is called once per frame
    bool isIgnoreCollisionGround = false;

    void Update () {
        if (timeCount <= 0.8f) {
            timeCount +=Time.deltaTime;
        }
        else {
            //GetComponent<Rigidbody2D>().velocity+=new Vector2(0,20);
            if (playercontorl != null) {
                if (!isIgnoreCollisionGround) {
                    GameObject[] groundgameObj = GameObject.FindGameObjectsWithTag("ground");  //無視其他enemy碰撞
                    foreach (GameObject each in groundgameObj) {
                        Physics2D.IgnoreCollision(each.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    }
                    isIgnoreCollisionGround = true;
                }
                incontorlObj = playercontorl.incontorlObj;
                transform.position = Vector2.MoveTowards(transform.position, incontorlObj.transform.position, 30 * Time.deltaTime);
            }
        }

	}

    void OnTriggerEnter2D(Collider2D other)
    {

        /*
        if (other.tag == "playerCollider" && playerData.HP+hp <= playerData.MAXHP)
        {
            playerData.HP += hp;
            Destroy(gameObject);
        }
        */

        if (other.tag == "playerCollider" && timeCount > 0.6f ) {
            if (playerData.HP + hp <= playerData.MAXHP) {
                playerData.HP += hp;
            }
            Destroy(gameObject);
        }

    }
}
