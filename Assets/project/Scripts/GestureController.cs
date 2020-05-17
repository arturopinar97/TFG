using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using HI5.VRCalibration;



public class GestureController : MonoBehaviour {

//public GameObject box1; 
public AudioClip colliderFeedback; 
AudioSource fuenteAudio; 


    public void Start() {
        fuenteAudio = GetComponent<AudioSource> ();
    }


    public void Update() {
        //Debug.Log("---- GUANTE: ------  X" + transform.position.x + "Y: " + transform.position.y +  "Z: " + transform.position.z); 
        //Debug.Log("---- CUBO: ------ X: " + box1.transform.position.x + "Y: " + transform.position.y + "Z: " + transform.position.z); 
        

    
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("collider: " + other.gameObject.tag);
        Debug.Log("CHOCO CON " + other.gameObject.name); 
        fuenteAudio.clip = colliderFeedback; 
        fuenteAudio.Play(); 
        
    }







}