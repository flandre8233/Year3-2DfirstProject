using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class gameCanvasScript : MonoBehaviour {
    [SerializeField]
    playerDataClass playerDataclass;
    [SerializeField]
    Text soulsDisplayText;
    [SerializeField]
    Image npcTypePart;
    [SerializeField]
    List<Sprite> npcTypePartArray;
    [SerializeField]
    Image shieldPart;
    [SerializeField]
    Image HPPart;
    [SerializeField]
    playerContorl playerContorl;

    npcClass inContorlObjectnpcClass;

    float HPFillAmountInBar;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (playerContorl != null) {
            inContorlObjectnpcClass = playerContorl.incontorlObj.GetComponent<npcClass>();
        }
        soulsPart();
        HPBARPart();
        ShieldPart();
        controlNpcTypeShow();
    }

    void controlNpcTypeShow() {
        switch (inContorlObjectnpcClass.WeaponP) {
            case npcClass.Weapon.sword:
                npcTypePart.sprite = npcTypePartArray[0];
                break;
            case npcClass.Weapon.axe:
                npcTypePart.sprite = npcTypePartArray[1];
                break;
            case npcClass.Weapon.rifle:
                npcTypePart.sprite = npcTypePartArray[2];
                break;
        }
    }


    void soulsPart() {
        soulsDisplayText.text = playerDataclass.playerSouls.ToString();
    }

    float ShowUphpbarAmount = 1.0f;

    void HPBARPart() {
        HPFillAmountInBar = (1.0f / playerDataclass.MAXHP) * playerDataclass.HP;
        //HPPart.fillAmount = HPFillAmountInBar;
        HPPart.fillAmount = Mathf.Lerp(HPPart.fillAmount, HPFillAmountInBar,0.05f);


        //



    }
    void ShieldPart() {

        if (inContorlObjectnpcClass != null) { 
            if (inContorlObjectnpcClass.HP > 0.0f) {
                shieldPart.enabled = true;
            }
            else {
                shieldPart.enabled = false;
            }
        }
    }
}
