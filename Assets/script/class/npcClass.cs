using UnityEngine;
using System.Collections;
using myFunction;

public class npcClass : MonoBehaviour {
    Function myfunction = new Function();
    

    public enum Type{ contorl,normal,spyder};
    public Type TypeP;

    public enum liveState { live, dead };
    public liveState liveStateP;

    public enum attackState { stand,guard,alert,attack };
    public attackState attackStateP;

    public enum movementState { landed, walking, jumpingBothCanMove, jumpingOnlyLeftMove, jumpingOnlyRightMove, falling };
    public movementState movementStateP;

    public enum CastAni { onMovement, onAttack,onDestory };
    public CastAni CastAniP;

    //public enum attackState { landed, walking, jumpingBothCanMove, jumpingOnlyLeftMove, jumpingOnlyRightMove, falling };
    //public State movementStateP;

    public enum Weapon { sword,rifle,axe};
    public Weapon WeaponP;

    [SerializeField]
    public short rifleBullet;
    [SerializeField]
    public short souls;
    [SerializeField]
    public short HP;

    public void npcClassSetUp() {
        /*
        short weaponNum =  (short)myfunction.RandomNumber(3);
        switch (weaponNum) {
            case 1:
                
                WeaponP = Weapon.sword;
                break;
            case 2:
                WeaponP = Weapon.rifle;
                break;
            case 3:
                WeaponP = Weapon.axe;
                break;

        }
        */

        //rifleBullet = 10;
        //souls = 1;
        //HP = 1;


    }
    


}
