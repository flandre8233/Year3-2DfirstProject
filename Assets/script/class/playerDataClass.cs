using UnityEngine;
using System.Collections;

public class playerDataClass : MonoBehaviour {
    public static playerDataClass staticData;

    public int playerSouls { get; set; }
    [SerializeField]
    public float HP = 0.0f;
    [SerializeField]
    public int MAXHP = 0;

    void Awake() {

        staticData = this;

    }

    public playerDataClass() {
        playerSouls = 0;
    }



}
