/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
using Valve.VR;
using Microsoft.VisualBasic; 
using HI5.VRCalibration;
using UnityEngine.Audio;

namespace ChoVR_Core{
public class MixerController : MonoBehaviour {


    public AudioMixer sopranoMixer; 

    public enum ETone {
        INCREASE, 
        DECREASE, 
        CONSTANT
    }

    private static ETone sopranoTone; 

    private static MixerController instance; 

    public static MixerController getInstance() {
        if(instance == null) {
            instance = new MixerController(); 
        }
        return instance; 
    }


    public void Update() {

    }

    private void checkToneState() {
        
    }

    private void evaluateToneSoprano()


    public static void setToneSoprano(ETone type) {
        sopranoTone = type;
    }



    // 
    public void increaseTone() {
        float value; 
        bool ok = sopranoMixer.GetFloat("pitchField", out value); 
        if(ok) {
            value += 1; 
            sopranoMixer.SetFloat("pitchField", value); 
            Debug.Log("increaseToneSoprano"); 
        }
        else{
            Debug.Log("ERROR INCREASING TONE SOPRANO"); 
        }
    }




/*using System.Collections;
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
		float valuue;
         bool result =  mixer.GetFloat("pitchField", out valuue);
		 Debug.Log(valuue);
	}
	public void setTempo(float valor){
		mixer.SetFloat("pitchGeneral",valor);
		float aux = 1/valor;
		    float valuue;
         bool result =  mixer.GetFloat("pitchField", out valuue);
         
		 Debug.Log(valuue);
		mixer.SetFloat("pitchField",aux );
       
	}
}
*/
/*





















}

}*/