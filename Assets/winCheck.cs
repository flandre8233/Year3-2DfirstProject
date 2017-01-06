using UnityEngine;
using System.Collections;

public class winCheck : GameFunction
{
    public gameManager gameManager;
    gameStateDataClass gameStateDataClass;
    [SerializeField]
    private GameObject GoalPoint ;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectsWithTag("backgroundScipt")[0].GetComponent<gameManager>();
        gameStateDataClass = GetComponent<gameStateDataClass>();
        if (gameStateDataClass.gameWin == gameStateDataClass.gameWinCondition.reciprocal) {
            StartCoroutine(reciprocal());
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator reciprocal(  ) {

        yield return new WaitForSeconds(20);

        if (gameStateDataClass.gamestate != gameStateDataClass.gameState.gameover) {
            gameManager.OnPlayerWin();
            gameStateDataClass.gamestate = gameStateDataClass.gameState.gameover;
        }

    }

}
