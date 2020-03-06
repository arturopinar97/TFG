using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class setVolumen : MonoBehaviour
{
    public AudioMixer mixer;
	AudioSource aSource;
	
	public void setLevel(float valor){
		mixer.SetFloat("volumeField",Mathf.Log10(valor)*20);
	}
	public void setPitch(float valor){
		mixer.SetFloat("pitchField",valor);
	}
	public void setTempo(float valor){
		aSource = GetComponent<AudioSource>();
		aSource.pitch = valor;
	}
}
