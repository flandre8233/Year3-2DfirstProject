using UnityEngine;
using System.Collections;

public class backgroundPicCopy : MonoBehaviour {
    public float scrollSpeed;
    private Vector2 savedOffset;
    private Renderer renderer;

    void Start() {
        renderer = GetComponent<Renderer>();
        savedOffset = renderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update() {
        float x = Mathf.Repeat( (Time.time * scrollSpeed), 1f);
        Vector2 offset = new Vector2(x-0.5f, savedOffset.y);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable() {
        renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
