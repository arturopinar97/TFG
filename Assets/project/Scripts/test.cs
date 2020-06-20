using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
using Valve.VR;
using Microsoft.VisualBasic; 
using HI5.VRCalibration;



public class test : MonoBehaviour {

public AudioClip clip; 

AudioSource audio; 


private void Start() {
    audio = GameObject.Find("state1 (1)").GetComponent<AudioSource>();

}

private void OnTriggerEnter(Collider other) {
        if(other.tag == "RightHand") {
        audio.clip = clip; 
        audio.Play(); 
        Debug.Log(other.name); 
        }
    }



}