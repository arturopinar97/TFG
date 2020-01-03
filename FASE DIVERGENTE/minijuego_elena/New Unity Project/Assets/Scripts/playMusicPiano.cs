using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusicPiano : MonoBehaviour
{
	 bool playingM = false;
    public AudioClip piano;
	


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
        if (Input.GetKeyDown(KeyCode.P) && !playingM){
			PlayM();
			fuenteAudio.clip= piano;
			fuenteAudio.Play ();
		}
		else if(Input.GetKeyDown(KeyCode.P) && playingM){
			PlayM();
			fuenteAudio.Pause ();
		}
		
		
    }
}
