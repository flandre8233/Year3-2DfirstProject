using UnityEngine;
using System.Collections.Generic;

public class gunShot : MonoBehaviour
{

    public enum damageType { npcOnly, playerOnly };
    public damageType damagetype;

    public float speed;
    [Range(0, 100)]
    [SerializeField]
    public short damage;

    [SerializeField]
    public Object bullethitParticlePrefab;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    /*
    private float DegreeToRadian(float angle) {
        return Mathf.PI * angle / 180.0f;
    }

    private Vector3 forward2d() {
        float zRotation = transform.eulerAngles.z;
        float sinF = Mathf.Sin(DegreeToRadian(zRotation));
        float cosF = Mathf.Cos(DegreeToRadian(zRotation));
        return new Vector3(cosF, sinF, 0);
    }
    */

    void OnTriggerEnter2D(Collider2D other) {



        if (other.gameObject.tag == "enemy" || other.gameObject.tag == "enemy-cantbePossessed") {
            switch (damagetype) {
                case damageType.npcOnly:
                    if (other.gameObject.GetComponent<npcClass>().TypeP == npcClass.Type.normal) {
                        other.gameObject.GetComponent<npcScript>().npcHPCheck(damage, "player");
                        Instantiate(bullethitParticlePrefab, transform.position, Quaternion.identity);
                        Destroy(gameObject); //destroy self
                    }
                    break;
                case damageType.playerOnly:
                    if (other.gameObject.GetComponent<npcClass>().TypeP == npcClass.Type.contorl) {
                        other.gameObject.GetComponent<npcScript>().npcHPCheck(damage, "enemy");
                        Instantiate(bullethitParticlePrefab, transform.position, Quaternion.identity);
                        Destroy(gameObject); //destroy self
                    }
                    break;
            }
        }

        if (other.gameObject.tag == "ground") {
            Destroy(gameObject); //destroy self
        }

    }

    void OnCollisionEnter2D(Collision2D other) {

    }
}
