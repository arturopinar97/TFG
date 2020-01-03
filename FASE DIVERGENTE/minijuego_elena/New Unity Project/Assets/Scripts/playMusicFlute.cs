using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusicFlute : MonoBehaviour
{
   bool playingM = false;
    public AudioClip flute;
	


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
        if (Input.GetKeyDown(KeyCode.F) && !playingM){
			PlayM();
			fuenteAudio.clip= flute;
			fuenteAudio.Play ();
		}
		else if(Input.GetKeyDown(KeyCode.F) && playingM){
			PlayM();
			fuenteAudio.Pause ();
		}
		
		
    }
}

