using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySound : MonoBehaviour
{
   public AudioClip music1; // AudioClip: Sonido que vamos a reproducir. 
   public AudioClip music2;

   AudioSource fuenteAudio;
    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            fuenteAudio.clip = music1;
            fuenteAudio.Play ();
            GetComponent<Renderer>().material.color = new Color(0.2355375f, 0.745283f, 0.4376931f); 


        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            fuenteAudio.clip = music2;
            fuenteAudio.Play();
            GetComponent<Renderer>().material.color = new Color(0.1045746f, 0.2972887f, 0.8867924f); 
        }
    }
}
