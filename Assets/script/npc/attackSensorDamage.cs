using UnityEngine;
using System.Collections.Generic;
using myFunction;
public class attackSensorDamage : GameFunction
{
    npcClass npcclass;
    [SerializeField]
    playerSensorCode playerSensorCode;
    public playerDataClass pDc;
    public short damage;

    float lifetime = 0.4f;
    List<GameObject> alreadyDamageArray = new List<GameObject>();

    void Awake() {
    }

    void OnEnable() {
        pDc = GameObject.FindGameObjectsWithTag("backgroundScipt")[0].GetComponent<playerDataClass>();
        alreadyDamageArray.Clear();
    }

    private void Update()
    {
        /*
        if (lifetime > 0.0f)
        {
            lifetime -= Time.deltaTime;
        }else
        {
            Destroy(gameObject);
        }
        */
    }

    //prefab那邊有問題 小心  http://stackoverflow.com/questions/36095870/how-to-keep-references-to-ui-elements-in-a-prefab-instantiated-at-runtime
    void OnTriggerEnter2D(Collider2D other) {
        npcclass = GetComponentInParent<npcClass>();

        triggerBullet(other);
        triggerNpc(other); //problem maybe?

    }

    void triggerBullet(Collider2D other) {
        #region 反彈子彈
        if (other.tag == "bullet" && npcclass.WeaponP == npcClass.Weapon.sword && npcclass.TypeP == npcClass.Type.contorl) {
            GameObject bulletObject = other.gameObject;
            //bulletObject.transform.eulerAngles = new Vector3(0,0,444);
            bulletObject.transform.eulerAngles = new Vector3(0, 0, Function.RandomNumber(40) + 160 + bulletObject.transform.eulerAngles.z);
            bulletObject.GetComponent<gunShot>().speed *= 3;
            //bulletObject
            //transform.rotation = Quaternion.Euler(0, 0, i);
        }
        #endregion
    }
    void triggerNpc(Collider2D other) {
        bool alreadyDeal = false;
        if ( (other.tag == "enemy" || other.gameObject.tag == "enemy-cantbePossessed" )&& other.gameObject != gameObject) {   //多重攻擊目標


            if (alreadyDamageArray.Count > 0) {
                foreach (GameObject each in alreadyDamageArray) {
                    if (each == other.gameObject) {
                        alreadyDeal = true;
                        break;
                    }
                }
            }


            if (!alreadyDeal) {
                switch (npcclass.TypeP) {
                    case npcClass.Type.contorl:
                        if (other.gameObject.GetComponent<npcClass>().TypeP != npcClass.Type.contorl) {
                            if (other.gameObject.GetComponent<npcClass>().Species == npcClass.SpeciesType.spyder || other.gameObject.GetComponent<npcClass>().Species == npcClass.SpeciesType.robot) {
                                soundEffectManager.staticSoundEffect.play_hitOrShotBotOrSpyder();
                            }else
                            {
                                switch (npcclass.WeaponP)
                                {
                                    case npcClass.Weapon.none:
                                        break;
                                    case npcClass.Weapon.sword:
                                        soundEffectManager.staticSoundEffect.play_swordHitTarget();
                                        break;
                                    case npcClass.Weapon.rifle:
                                        break;
                                    case npcClass.Weapon.axe:
                                        soundEffectManager.staticSoundEffect.play_axeHitTarget();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            other.gameObject.GetComponent<npcScript>().npcHPCheck(damage, "player");
                            alreadyDamageArray.Add(other.gameObject);
                            //gameObject.SetActive(false);
                        }

                        break;
                    case npcClass.Type.normal:
                        if (other.gameObject == playerSensorCode.npc && playerSensorCode.npc != null) {
                            switch (npcclass.WeaponP)
                            {
                                case npcClass.Weapon.none:
                                    break;
                                case npcClass.Weapon.sword:
                                    soundEffectManager.staticSoundEffect.play_swordHitTarget();
                                    break;
                                case npcClass.Weapon.rifle:
                                    break;
                                case npcClass.Weapon.axe:
                                    soundEffectManager.staticSoundEffect.play_axeHitTarget();
                                    break;
                                default:
                                    break;
                            }
                            other.gameObject.GetComponent<npcScript>().npcHPCheck(damage, "enemy");
                            alreadyDamageArray.Add(other.gameObject);
                            //gameObject.SetActive(false);
                        }
                        break;
                }
            }


        }
    }
}
