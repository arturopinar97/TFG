using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using HI5.VRCalibration;



public class StateController : MonoBehaviour {

//public GameObject box1; 
public AudioClip colliderFeedback; 

public static int state; 
private int maxStates; 
AudioSource fuenteAudio; 

    public StateController(/* int maxStates */){
        maxStates = 3; // 2 por 4
    }
    public void Start() {
        fuenteAudio = GetComponent<AudioSource> ();
        state = 1; 
    }


    public void Update() {
        //Debug.Log("---- GUANTE: ------  X" + transform.position.x + "Y: " + transform.position.y +  "Z: " + transform.position.z); 
        //Debug.Log("---- CUBO: ------ X: " + box1.transform.position.x + "Y: " + transform.position.y + "Z: " + transform.position.z); 
        

    
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("SIGUIENTE ESTADO: " + state); 
        if(this.name == "Cube1" && 
            state == 1){
                //Debug.Log("collider: " + other.gameObject.tag);
                //Debug.Log("CHOCO CON " + other.gameObject.name); 
                //fuenteAudio.clip = colliderFeedback; 
                //fuenteAudio.Play(); 
                Debug.Log("Has tocado correctamente el primer cubo "); 
                state = 2; 
            }


        if(this.name == "Cube2" && 
            state == 2) {
                //fuenteAudio.clip = colliderFeedback; 
                //fuenteAudio.Play(); 
                Debug.Log("Has tocado correctamente el segundo cubo"); 
                state = 3; 
            }
        if(this.name == "Cube3" && 
            state == 3) {
                //fuenteAudio.clip  = colliderFeedback; 
                //fuenteAudio.Play(); 
                Debug.Log("Has tocado correctamente el tercer cubo "); 
            }

        else {
            Debug.Log("No estas haciendo correctamente el gesto. "); 
            Debug.Log("Has cometido un error porque estas en el estado: " + state + "y tocando el cubo: " + this.name); 
        }

      
        //Debug.Log("Ya has activado el cubo 1"); 
        
    }







}