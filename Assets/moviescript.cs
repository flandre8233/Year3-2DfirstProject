using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moviescript : MonoBehaviour {
    Renderer renderer;
    MovieTexture movie;

    public GameObject eventsystem;

    public GameObject whitefadein;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        movie = (MovieTexture)renderer.material.mainTexture;
    }


    // Use this for initialization
    void Start () {
       // ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(movie.isPlaying);
        if (movie.isPlaying)
        {

        }else
        {
            //Destroy(gameObject);
            //whitefadein.SetActive(true);
            //eventsystem.SetActive(true);
        }
	}
}
