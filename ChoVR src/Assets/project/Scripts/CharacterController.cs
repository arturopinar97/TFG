using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using Microsoft.VisualBasic; 
using HI5.VRCalibration;
using UnityEngine.Audio;

namespace ChoVR_Core{
public class CharacterController : MonoBehaviour {


private const string CHORIST_SOPRANO = "soprano"; 
private const string CHORIST_CONTRALTO = "contralto"; 
private const string CHORIST_TENOR = "tenor"; 
private const string CHORIST_BAJO = "bajo"; 


private const float MAX_TONE = 1.25f; 
private const float MIN_TONE = 0.80f; 

private const float MAX_VOLUME = 15f; 
private const float MIN_VOLUME = -15f; 
private static GameObject choristSoprano; 
private static GameObject choristContralto; 
private static GameObject choristTenor; 
private static GameObject choristBajo; 

private static AudioSource audioSourceSoprano; 
private static AudioSource audioSourceContralto; 
private static AudioSource audioSourceTenor; 
private static AudioSource audioSourceBajo; 
public AudioClip sopranoVoice; 
public AudioClip contraltoVoice; 
public AudioClip tenorVoice; 
public AudioClip bajoVoice; 


private static bool startSong; 
private static bool startedSong; 





public enum EState {
    INCREASE_VOLUME, 
    LOWER_VOLUME, 
    INCREASE_TONE, 
    LOWER_TONE, 
    SING, 
    MUTE, 
    NO_CHANGE
}

private static EState sopranoState; 
private static EState contraltoState; 
private static EState tenorState; 
private static EState bajoState; 

public AudioMixer mixer; 



public void Start() {
    choristSoprano = GameObject.Find(CHORIST_SOPRANO); 
    choristContralto = GameObject.Find(CHORIST_CONTRALTO); 
    choristTenor = GameObject.Find(CHORIST_TENOR); 
    choristBajo = GameObject.Find(CHORIST_BAJO); 
    
    audioSourceSoprano = choristSoprano.GetComponent<AudioSource>(); 
    audioSourceContralto = choristContralto.GetComponent<AudioSource>(); 
    audioSourceTenor = choristTenor.GetComponent<AudioSource>();
    audioSourceBajo = choristBajo.GetComponent<AudioSource>();  
    
    audioSourceSoprano.clip = sopranoVoice; 
    audioSourceContralto.clip = contraltoVoice; 
    audioSourceTenor.clip = tenorVoice; 
    audioSourceBajo.clip = bajoVoice; 

    startSong = false; 

    audioSourceSoprano.volume = 0.5f; 
    audioSourceContralto.volume = 0.5f;
    audioSourceTenor.volume = 0.5f;
    audioSourceBajo.volume = 0.5f;


    
}

private static void playSong() {
        startedSong = true; 
        audioSourceSoprano.Play();
        audioSourceContralto.Play();
        audioSourceTenor.Play();
        audioSourceBajo.Play();

        choristSoprano.GetComponent<Animator>().Play("Cantar");
        choristContralto.GetComponent<Animator>().Play("Cantar");
        choristTenor.GetComponent<Animator>().Play("Cantar");
        choristBajo.GetComponent<Animator>().Play("Cantar");
    
}

public static bool isSongStarted() {
    //Debug.Log("isSongStarted: " + startSong); 
    return startSong; 
}

public void Update() {
    repeatSong(); 
    
    evaluateStates(); 


    
}

private void repeatSong() {
    if((!audioSourceSoprano.isPlaying || 
        !audioSourceContralto.isPlaying || 
        !audioSourceTenor.isPlaying || 
        !audioSourceBajo.isPlaying) && 
        (startSong)) {
            audioSourceSoprano.Play(); 
            audioSourceTenor.Play(); 
            audioSourceContralto.Play(); 
            audioSourceBajo.Play(); 
        }
}


public static void notSignAll() {
    muteSoprano();  
    muteContralto(); 
    muteTenor(); 
    muteBajo(); 

}

public static void StartSong() {
    startSong = true; 
    //Debug.Log("start song"); 
    playSong();
}

public static void setStateSoprano(EState type) {
    sopranoState = type; 
}

public static void setStateContralto(EState type) {
    contraltoState = type; 
}

public static void setStateTenor(EState type) {
    tenorState = type; 
}

public static void setStateBajo(EState type) {
    bajoState = type; 
}

private void evaluateStates() {
    evaluateSopranoState(); 
    evaluateContraltoState(); 
    evaluateTenorState(); 
    evaluateBajoState(); 
}

private void evaluateSopranoState() {
    //Debug.Log("sopranoState vale: " + sopranoState);
    switch(sopranoState) {
        case EState.INCREASE_TONE: 
            //Debug.Log("evaluateIncreaseToneSoprano"); 
            increaseToneSoprano(); 
            break; 
        case EState.LOWER_TONE: 
            lowerToneSoprano(); 
            break; 
        case EState.INCREASE_VOLUME: 
            increaseVolumeSoprano(); 
            break; 
        case EState.LOWER_VOLUME: 
            lowerVolumeSoprano(); 
            break; 
        case EState.MUTE: 
            muteSoprano(); 
            break; 
        case EState.SING: 
            signSoprano(); 
            break; 
    }
}

private void evaluateContraltoState() {
    //Debug.Log("sopranoState vale: " + sopranoState);
    switch(contraltoState) {
        case EState.INCREASE_TONE: 
            //Debug.Log("evaluateIncreaseToneSoprano"); 
            increaseToneContralto(); 
            break; 
        case EState.LOWER_TONE: 
            lowerToneContralto(); 
            break; 
        case EState.INCREASE_VOLUME: 
            increaseVolumeContralto(); 
            break; 
        case EState.LOWER_VOLUME: 
            lowerVolumeContralto(); 
            break; 
        case EState.MUTE: 
            muteContralto(); 
            break; 
        case EState.SING: 
            signContralto(); 
            break; 
    }
}

private void evaluateTenorState() {
    //Debug.Log("sopranoState vale: " + sopranoState);
    switch(tenorState) {
        case EState.INCREASE_TONE: 
            //Debug.Log("evaluateIncreaseToneSoprano"); 
            increaseToneTenor(); 
            break; 
        case EState.LOWER_TONE: 
            lowerToneTenor(); 
            break; 
        case EState.INCREASE_VOLUME: 
            increaseVolumeTenor(); 
            break; 
        case EState.LOWER_VOLUME: 
            lowerVolumeTenor(); 
            break; 
        case EState.MUTE: 
            muteTenor(); 
            break; 
        case EState.SING: 
            signTenor(); 
            break; 
    }
}

private void evaluateBajoState() {
    //Debug.Log("sopranoState vale: " + sopranoState);
    switch(bajoState) {
        case EState.INCREASE_TONE: 
            //Debug.Log("evaluateIncreaseToneSoprano"); 
            increaseToneBajo(); 
            break; 
        case EState.LOWER_TONE: 
            lowerToneBajo(); 
            break; 
        case EState.INCREASE_VOLUME: 
            increaseVolumeBajo(); 
            break; 
        case EState.LOWER_VOLUME: 
            lowerVolumeBajo(); 
            break; 
        case EState.MUTE: 
            muteBajo(); 
            break; 
        case EState.SING: 
            signBajo(); 
            break; 
    }
}



public static void signSoprano() {
    choristSoprano.GetComponent<Animator>().Play("Cantar");
    if(audioSourceSoprano.isPlaying) {
        audioSourceSoprano.mute = false; 
    }
    
}

public static void signContralto() {
    choristContralto.GetComponent<Animator>().Play("Cantar");
    if(audioSourceContralto.isPlaying) {
        audioSourceContralto.mute = false; 
    }
    
}

public static void signTenor() {
    choristTenor.GetComponent<Animator>().Play("Cantar");
    if(audioSourceTenor.isPlaying) {
        audioSourceTenor.mute = false; 
    }
    
}

public static void signBajo() {
    choristBajo.GetComponent<Animator>().Play("Cantar");
    if(audioSourceBajo.isPlaying) {
        audioSourceBajo.mute = false; 
    }
   
}

public static void muteSoprano() {
    choristSoprano.GetComponent<Animator>().Play("Silencio"); 
    audioSourceSoprano.mute = true; 
}

public static void muteContralto() {
    choristContralto.GetComponent<Animator>().Play("Silencio"); 
    audioSourceContralto.mute = true; 
}

public static void muteTenor() {
    choristTenor.GetComponent<Animator>().Play("Silencio"); 
    audioSourceTenor.mute = true;
}

public static void muteBajo() {
    choristBajo.GetComponent<Animator>().Play("Silencio"); 
    audioSourceBajo.mute = true;
}


private void increaseToneSoprano() {
        changeTone("pitchFieldSoprano", EState.INCREASE_TONE); 
        setSopranoNoChangeState(); 
}

private void increaseToneContralto() {
        changeTone("pitchFieldContralto", EState.INCREASE_TONE); 
        setContraltoNoChangeState(); 
}

private void increaseToneTenor() {
        changeTone("pitchFieldTenor", EState.INCREASE_TONE); 
        setTenorNoChangeState(); 
}

private void increaseToneBajo() {
        changeTone("pitchFieldBajo", EState.INCREASE_TONE); 
        setBajoNoChangeState(); 
}


private void lowerToneSoprano() {
        changeTone("pitchFieldSoprano", EState.LOWER_TONE); 
        setSopranoNoChangeState(); 
}

private void lowerToneContralto() {
        changeTone("pitchFieldContralto", EState.LOWER_TONE); 
        setContraltoNoChangeState(); 
}

private void lowerToneTenor() {
        changeTone("pitchFieldTenor", EState.LOWER_TONE); 
        setTenorNoChangeState(); 
}

private void lowerToneBajo() {
        changeTone("pitchFieldBajo", EState.LOWER_TONE); 
        setBajoNoChangeState(); 
}



private void changeVolume(string channel, EState state) {
    float value; 
    bool ok = mixer.GetFloat(channel, out value); 
    if(ok) {
    if(state == EState.INCREASE_VOLUME) {
        value += 0.1f; 
    }
    else if(state == EState.LOWER_VOLUME) {
        value -= 0.1f; 
    }
    else{
        Debug.Log("Error en el cambio de tono"); 
    }
    if(value > MIN_VOLUME && value < MAX_VOLUME) {
        mixer.SetFloat(channel, value); 
    }
    }
    else{
        mixer.SetFloat(channel, generatorVolume()); 
    }
}

private void increaseVolumeSoprano() {
        changeVolume("volumeFieldSoprano", EState.INCREASE_VOLUME); 
        setSopranoNoChangeState(); 
}

private void increaseVolumeContralto() {
        changeVolume("volumeFieldContralto", EState.INCREASE_VOLUME); 
        setContraltoNoChangeState(); 
}

private void increaseVolumeTenor() {
        changeVolume("volumeFieldTenor", EState.INCREASE_VOLUME); 
        setTenorNoChangeState(); 
}

private void increaseVolumeBajo() {
        changeVolume("volumeFieldBajo", EState.INCREASE_VOLUME); 
        setBajoNoChangeState(); 
}


private void lowerVolumeSoprano() {
        changeVolume("volumeFieldSoprano", EState.LOWER_VOLUME); 
        setSopranoNoChangeState(); 
}

private void lowerVolumeContralto() {
        changeVolume("volumeFieldContralto", EState.LOWER_VOLUME); 
        setContraltoNoChangeState(); 
}

private void lowerVolumeTenor() {
        changeVolume("volumeFieldTenor", EState.LOWER_VOLUME); 
        setTenorNoChangeState(); 
}

private void lowerVolumeBajo() {
        changeVolume("volumeFieldBajo", EState.LOWER_VOLUME); 
        setBajoNoChangeState(); 
}



private void changeTone(string channel, EState state) {
    float value; 
    bool ok = mixer.GetFloat(channel, out value); 
    if(ok) {
    if(state == EState.INCREASE_TONE) {
        value += 0.001f; 
    }
    else if(state == EState.LOWER_TONE) {
        value -= 0.001f; 
    }
    else{
        Debug.Log("Error en el cambio de tono"); 
    }
    if(value > MIN_TONE && value < MAX_TONE) {
        mixer.SetFloat(channel, value); 
    }
    }
    else{
        mixer.SetFloat(channel, generatorTone()); 
    }
}


private float generatorVolume() {
    return 0.0f; 
}
private void setSopranoNoChangeState() {
    sopranoState = EState.NO_CHANGE;
}

private void setContraltoNoChangeState() {
    contraltoState = EState.NO_CHANGE; 
}

private void setTenorNoChangeState() {
    tenorState = EState.NO_CHANGE; 
}

private void setBajoNoChangeState() {
    bajoState = EState.NO_CHANGE; 
}

private float generatorTone() {
    return 1.0f; 
}

}
}