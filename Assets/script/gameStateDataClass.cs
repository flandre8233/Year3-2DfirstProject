using UnityEngine;
using System.Collections;

public class gameStateDataClass : MonoBehaviour {
    
    public enum gameState { menu,game,pause,gameover };
    public gameState gamestate;

    public enum gameWinCondition { reciprocal , goal , killAll , normal };
    public gameWinCondition gameWin = gameWinCondition.goal;

    public float reciprocalTime;
    public int starNumber;
    // Use this for initialization
    void Start () {
        
    }
    void Awake() {
        //DontDestroyOnLoad(transform.gameObject);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
