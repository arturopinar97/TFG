using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusicViolin : MonoBehaviour
{
   bool playingM = false;
    public AudioClip violin;
	


    AudioSource fuenteAudio;
    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
    }
	void PlayM() {
			playingM =! playingM;
	}
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && !playingM){
			PlayM();
			fuenteAudio.clip= violin;
			fuenteAudio.Play ();
		}
		else if(Input.GetKeyDown(KeyCode.V) && playingM){
			PlayM();
			fuenteAudio.Pause ();
		}
		
		
    }
}
