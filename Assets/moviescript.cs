using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moviescript : MonoBehaviour {
    MovieTexture movie;

    public GameObject eventsystem;

    public GameObject whitefadein;

    private void Awake()
    {
        movie = (MovieTexture)GetComponent<RawImage>().texture;
    }


    // Use this for initialization
    void Start () {
        movie.Play();
    }
	
	// Update is called once per frame
	void Update () {

        if (movie.isPlaying)
        {

        }else
        {
            Destroy(gameObject);
            whitefadein.SetActive(true);
            eventsystem.SetActive(true);
        }
	}
}
