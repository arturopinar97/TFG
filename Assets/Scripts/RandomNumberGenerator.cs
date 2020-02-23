using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class RandomNumberGenerator : MonoBehaviour
{
    // Start is called before the first frame update
	
	AudioSource Musiquita;
	public AudioMixer mixer;
    public void Button_ChangeVolume()
    {
       float number = Random.Range(0.0f,1.0f);
	   Debug.Log(number);
	   
        //Fetch the AudioSource from the GameObject
        Musiquita = GetComponent<AudioSource>();
		 Musiquita.volume = number;
        //Play the AudioClip attached to the AudioSource on startup
        Musiquita.Play();
    }

	public void justPlay(){
		Musiquita = GetComponent<AudioSource>();
        //Play the AudioClip attached to the AudioSource on startup
        Musiquita.Play();
	}
	
	public void Button_ChangePitch(){
		float number = Random.Range(0.0f,2.0f);
		mixer.SetFloat("pitchField",number);
		justPlay();
		Debug.Log(number);
	}
    
}
