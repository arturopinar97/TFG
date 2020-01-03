using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusicDrums : MonoBehaviour
{
   bool playingM = false;
    public AudioClip drums;
	


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
        if (Input.GetKeyDown(KeyCode.D) && !playingM){
			PlayM();
			fuenteAudio.clip= drums;
			fuenteAudio.Play ();
		}
		else if(Input.GetKeyDown(KeyCode.D) && playingM){
			PlayM();
			fuenteAudio.Pause ();
		}
		
		
    }
}
