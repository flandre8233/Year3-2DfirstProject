using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
     playerDataClass playerDataClass;
    gameStateDataClass gameStateDataClass;
    int soulsNumber;

    bool isPauseButtonDown = false;
    float originalTimeScale = 0.0f;
    float volumeConNumber = 0.1f;

    GUIStyle myStyle;

    //public Text scriptText2; //soulsText
    public Slider volSlider;
    public GameObject pauseMenuCanvas;

    // Use this for initialization
    void Start() {

        playerDataClass = GetComponent<playerDataClass>();
        gameStateDataClass = GetComponent<gameStateDataClass>();
        gameStateDataClass.gamestate = gameStateDataClass.gameState.game;
        //scriptText = GetComponent<Text>();
        if (pauseMenuCanvas != null) {
            pauseMenuCanvas.SetActive(false);
        }
        if (volSlider != null) {
            volSlider.value = 1;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        //scriptText = GameObject.Find("/Canvas/CountText").GetComponent("Text (Script)");
        //SoulsText();
        SilderVol();

    }

    public void buttonEnterLevel(int loadScene) {
        Debug.Log("nnnn");
        SceneManager.LoadScene(loadScene); // enter select
    }

    public void buttonOnclick() {
        Debug.Log("nnnn");
        
    }

    public void buttonPause() {
        if (isPauseButtonDown) {
            isPauseButtonDown = false;
            gameStateDataClass.gamestate = gameStateDataClass.gameState.game;
            Time.timeScale = originalTimeScale;
        }
        else {
            isPauseButtonDown = true;
            originalTimeScale = Time.timeScale;
            gameStateDataClass.gamestate = gameStateDataClass.gameState.pause;
            Time.timeScale = 0f;
        }
        pauseMenuCanvas.SetActive(isPauseButtonDown);
    }

    public void restartCurGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void buttonQuit() {
        Application.Quit();
        gameStateDataClass.gamestate = gameStateDataClass.gameState.menu; //?
    }
    
    void SilderVol() {
        if (volSlider!=null) {
            AudioListener.volume = volSlider.value;
        }

    }

    /*
    void SoulsText() {
        if(scriptText2 != null) {
            string text = "";
            text = "靈魂數：" + playerDataClass.playerSouls.ToString();
            scriptText2.text = text;
        }
    }
    */

    void OnGUI() {
    }
}
