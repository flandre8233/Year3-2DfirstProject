using UnityEngine;
using System.Collections;

public class showGameFps : MonoBehaviour
{/*
    public playerDataClass pdc;
    string soulsText;
    [SerializeField]
    int frameRate = -1;

    float deltaTime = 0.0f;

    void Awake() {
        Application.targetFrameRate = frameRate;
    }


    void Update() {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        soulsText = pdc.playerSouls.ToString();
    }

    void OnGUI() {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 40);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 40;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);

        Rect soulsRect = new Rect(100, 100, 100, 100);


        GUI.Label(soulsRect, soulsText, style);
    }
    */
}
