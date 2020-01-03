using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusicGuitar : MonoBehaviour
{
    bool playingM = false;
    public AudioClip guitar;
	


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
        if (Input.GetKeyDown(KeyCode.G) && !playingM){
			PlayM();
			fuenteAudio.clip= guitar;
			fuenteAudio.Play ();
		}
		else if(Input.GetKeyDown(KeyCode.G) && playingM){
			PlayM();
			fuenteAudio.Pause ();
		}
		
		
    }
}
