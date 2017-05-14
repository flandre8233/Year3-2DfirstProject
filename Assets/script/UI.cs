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


    public int saveLoadScene;

    GUIStyle myStyle;

    //public Text scriptText2; //soulsText
    public Slider volSlider;
    public GameObject pauseMenuCanvas;
    public GameObject blackFade;

    // Use this for initialization
    void Start() {
        //volSlider = GameObject.FindGameObjectWithTag("soundBar").GetComponent<Slider>();

        playerDataClass = GetComponent<playerDataClass>();
        if ( GetComponent<gameStateDataClass>() != null) {
            gameStateDataClass = GetComponent<gameStateDataClass>();
            gameStateDataClass.gamestate = gameStateDataClass.gameState.game;
        }
        
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
        soundEffectManager.staticSoundEffect.play_clickButton();
        Time.timeScale = 1.0f;
        if (blackFade!=null) {
            Debug.Log(Application.loadedLevel);
            if (Application.loadedLevel > 1) {
                blackFade.GetComponent<blackFadeScipt>().DoOnce = false;
                blackFade.GetComponent<blackFadeScipt>().isNeedFadeIn = true;
            }
            blackFade.GetComponent<UI>().saveLoadScene = loadScene;
            blackFade.GetComponent<blackFadeScipt>().isStartAni = true;
        }
    }


    public void buttonEnterLevel2() {
        if (GameObject.Find("globalDataBase") != null) {
            GameObject findObject = GameObject.Find("globalDataBase");
            globalDataBase globalDataBase = findObject.GetComponent<globalDataBase>();
            globalDataBase.curLevel = saveLoadScene;
        }
        SceneManager.LoadScene(saveLoadScene); // enter select

    }

    public void startMenuEnterSelect() {
        if (GameObject.Find("globalDataBase") != null) {
            GameObject findObject = GameObject.Find("globalDataBase");
            globalDataBase globalDataBase = findObject.GetComponent<globalDataBase>();
            globalDataBase.curLevel = 1;
        }
        SceneManager.LoadScene(1); // enter select

    }

    public void buttonOnclick() {

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
        soundEffectManager.staticSoundEffect.play_clickButton();
        pauseMenuCanvas.SetActive(isPauseButtonDown);
    }

    public void restartCurGame() {
        soundEffectManager.staticSoundEffect.play_clickButton();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void buttonQuit() {
        soundEffectManager.staticSoundEffect.play_clickButton();
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

    public void DisableLevelCanvas(GameObject go) {
        go.SetActive(false);
    }

    public void selectLevelCanvas(GameObject go) {
        //Debug.Log(this.gameObject.name);
        soundEffectManager.staticSoundEffect.play_MoveOnButton();
        //GetComponentInParent<RectTransform>().gameObject.SetActive(false);
        go.SetActive(true);
    }

    void OnGUI() {
    }
}
