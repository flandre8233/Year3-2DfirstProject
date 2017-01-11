using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectManager : MonoBehaviour {
    public static soundEffectManager staticSoundEffect;

    public AudioClip swordloseTarget1;
    public AudioClip swordloseTarget2;
    public AudioClip restoreHP;
    public AudioClip rifleloseTarget;
    public AudioClip axeloseTarget;
    public AudioClip botShot;
    public AudioClip templeFire;
    public AudioClip run;

    public AudioClip clickButton;
    public AudioClip MoveOnButton;
    public AudioClip startSceneClickButton;

    public AudioClip swordHitTarget;
    public AudioClip axeHitTarget;
    public AudioClip rifleHitTarget;
    public AudioClip hitOrShotBotOrSpyder;

    public AudioClip PriestAttack1;
    public AudioClip PriestAttack2;

    public AudioClip possessedOnOpen;
    public AudioClip possessedOnFinish;

    AudioSource audio;
    AudioSource audio2;

    void Awake () {
        if (staticSoundEffect != null) {

        }
        else {
            staticSoundEffect = this;
        }
        audio = GetComponent<AudioSource>();
	}

    public void play_swordloseTarget1() {
        
        audio.PlayOneShot(swordloseTarget1,1.0f);
    }

    public void play_swordloseTarget2() {
        
        audio.PlayOneShot(swordloseTarget2, 1.0f);

    }

    public void play_restoreHP() {
        
        audio.PlayOneShot(restoreHP, 1.0f);

    }

    public void play_rifleloseTarget() {
        
        audio.PlayOneShot(rifleloseTarget, 1.0f);

    }

    public void play_axeloseTarget() {
        
        audio.PlayOneShot(axeloseTarget, 1.0f);

    }

    public void play_botShot() {
        
        audio.PlayOneShot(botShot, 1.0f);

    }

    public void play_templeFire() {
        
        audio.PlayOneShot(templeFire, 1.0f);

    }

    public void play_run() {
        
        audio.PlayOneShot(run, 1.0f);

    }

    public void play_clickButton() {
        
        audio.PlayOneShot(clickButton, 1.0f);

    }

    public void play_MoveOnButton() {
        
        audio.PlayOneShot(MoveOnButton, 1.0f);

    }

    public void play_startSceneClickButton() {
        
        audio.PlayOneShot(startSceneClickButton, 1.0f);

    }

    public void play_swordHitTarget() {
        
        audio.PlayOneShot(swordHitTarget, 1.0f);

    }

    public void play_axeHitTarget() {
        
        audio.PlayOneShot(axeHitTarget, 1.0f);

    }

    public void play_rifleHitTarget() {
        
        audio.PlayOneShot(rifleHitTarget, 1.0f);

    }

    public void play_hitOrShotBotOrSpyder() {
        
        audio.PlayOneShot(hitOrShotBotOrSpyder, 1.0f);

    }

    public void play_PriestAttack1() {
        
        audio.PlayOneShot(PriestAttack1, 1.0f);

    }

    public void play_PriestAttack2() {
        
        audio.PlayOneShot(PriestAttack2, 1.0f);

    }

    public void play_possessedOnOpen() {
        
        audio.PlayOneShot(possessedOnOpen, 1.0f);

    }

    public void play_possessedOnFinish() {
        
        audio.PlayOneShot(possessedOnFinish, 1.0f);

    }

}
