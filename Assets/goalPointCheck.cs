using UnityEngine;
using System.Collections;

public class goalPointCheck : GameFunction {
    public gameStateDataClass gameStateDataClass;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "playerCollider" && gameStateDataClass.gamestate!= gameStateDataClass.gameState.gameover) {
            OnPlayerWin();
            gameStateDataClass.gamestate = gameStateDataClass.gameState.gameover;
        }

    }

}
