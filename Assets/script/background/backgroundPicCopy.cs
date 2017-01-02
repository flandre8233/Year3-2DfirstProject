using UnityEngine;
using System.Collections;

public class backgroundPicCopy : MonoBehaviour
{
    [SerializeField]
    public string sortingLayerName;
    private MeshRenderer rend;
    public Camera camera;
    public float scrollSpeed;
    private Vector2 savedOffset;
    private Renderer renderer;
    private Vector3 startPosition;

    public float distance;

    public void Awake() {
        if (sortingLayerName != "") {
            rend = GetComponent<MeshRenderer>();
            rend.sortingLayerName = sortingLayerName;
        }
    }

    void Start() {
        renderer = GetComponent<Renderer>();
        savedOffset = renderer.sharedMaterial.GetTextureOffset("_MainTex");
        //startPosition = Vector3.Distance(camera.transform.position, Vector3.zero); 
    }

    void Update() {
        distance = Vector3.Distance(new Vector3(camera.transform.position.x, 0, 0), new Vector3(-100, 0, 0));
        //float x = Mathf.Repeat( (distance * (scrollSpeed/1000.0f) ), 1f);
        // Vector2 offset = new Vector2(x, savedOffset.y);

        Vector2 offset = new Vector2((distance * (scrollSpeed / 1000.0f)), savedOffset.y);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable() {
        renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
