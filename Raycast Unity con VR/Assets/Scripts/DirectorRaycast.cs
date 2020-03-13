using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorRaycast : MonoBehaviour
{
    RaycastHit hit; // objeto con el que colisiona el rayo.
    int i = 0;
    int j = 0; 
    public AudioClip music1; // pista de audio del musico 1
    public AudioClip music2; // pista de audio del musico 2
    public AudioClip music3; // pista de audio del musico 3
    public AudioClip music4;
    public AudioClip music5; 

    AudioSource fuenteAudio; 

    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource> (); // componente en el objeto Directing al que esta asociado este script.
    }

    // Update is called once per frame
    int left = 0;
    int right = 0;
    int llamadas = 0; 
    void Update()
    {
      
      
        if (Input.GetButtonDown("right_vr_controller"))
        {
            

            throwRaycast(GameObject.Find("Controller (right)"));
          

        }
        else if (Input.GetButtonDown("left_vr_controller"))
        {
            
          
            throwRaycast(GameObject.Find("Controller (left)"));
        }
    }
    void throwRaycast(GameObject gameObject)
    {
        RaycastHit hit;
        
       
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // dibuja un rayo invisible hacia donde apunta el cursor del raton
        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
        llamadas += 1;
       
        
        if (Physics.Raycast(ray, out hit)) // ¿Choca ese rayo con algun objeto? 
        {
            if (hit.collider != null) // Cogemos el collider (propiedades del cuerpo) del objeto que choca
            {
           
                // Finalmente comprobamos si el cuerpo con el que ha chocado es de alguno de los musicos 
                // y si es asi cargamos su pista de audio correspondiente. 
                if (hit.collider.name == "Music1")
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
            else
            {
                j = j + 1;
            }
        }
    }
}
