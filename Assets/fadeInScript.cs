using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeInScript : MonoBehaviour {

    [SerializeField]
    float fadeInTime = 0.5f;
    float fadeInCurrentNumber = 0;
    float CurrentTime = 0;

    void OnEnable() {
        fadeInCurrentNumber = 0.0f;
        GetComponent<CanvasGroup>().alpha = 0.0f;

    }

    // Update is called once per frame
    void FixedUpdate() {
        fadeInFunction();
    }

    void fadeInFunction() {
        if (GetComponent<CanvasGroup>().alpha < 1.0f) {
            CurrentTime += Time.deltaTime / fadeInTime;
            //fadeInCurrentNumber += Time.deltaTime / fadeInTime;
            fadeInCurrentNumber = Mathf.Lerp(0, 1, CurrentTime);

            GetComponent<CanvasGroup>().alpha = fadeInCurrentNumber;
        }
    }

}
