using UnityEngine;
using System.Collections;

public class goalPointCheck : GameFunction {
    public gameManager gameManager;

    public GameObject pacticle;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindGameObjectsWithTag("backgroundScipt")[0].GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "playerCollider" && gameStateDataClass.staticGameStateDataClass.gamestate != gameStateDataClass.gameState.gameover) {
            if (pacticle !=null)
            {
                pacticle.active = true;
            }
            gameManager.OnPlayerWin();
            gameStateDataClass.staticGameStateDataClass.gamestate = gameStateDataClass.gameState.gameover;
        }

    }

}
