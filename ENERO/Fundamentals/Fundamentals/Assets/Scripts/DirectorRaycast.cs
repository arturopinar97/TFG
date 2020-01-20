using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorRaycast : MonoBehaviour
{
    RaycastHit hit; // objeto con el que colisiona el rayo.
    int i = 0;
    int j = 0; 
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;
    public AudioClip music5; 

    AudioSource fuenteAudio; 

    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource> (); // componente en el objeto Directing al que esta asociado este script.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    //Debug.Log("CHOCA" + i + "veces");

                    Debug.Log("Collider" + i + " : " + hit.collider.name); 
                    if(hit.collider.name == "Music1")
                    {
                        fuenteAudio.clip = music1;
                        fuenteAudio.Play(); 
                    }
                    if (hit.collider.name == "Music2")
                    {
                        fuenteAudio.clip = music2;
                        fuenteAudio.Play();
                    }
                    if (hit.collider.name == "Music3")
                    {
                        fuenteAudio.clip = music3;
                        fuenteAudio.Play();
                    }
                    if (hit.collider.name == "Music4")
                    {
                        fuenteAudio.clip = music4;
                        fuenteAudio.Play();
                    }
                    if (hit.collider.name == "Music5")
                    {
                        fuenteAudio.clip = music5;
                        fuenteAudio.Play();
                    }
                    i = i + 1; 
                }
            }
            else
            {
                Debug.Log("NO CHOCA" + j + "veces");
                j = j + 1; 
            }


        }
    }
}
