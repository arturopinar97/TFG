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

// maximos valores de tono y volumen: 

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


// cerrojos: 
private static bool lockSopranoIncreaseTone; 
private static bool lockContraltoIncreaseTone; 
private static bool lockTenorIncreaseTone; 
private static bool lockBajoIncreaseTone; 

private static bool lockSopranoLowerTone; 
private static bool lockContraltoLowerTone; 
private static bool lockTenorLowerTone; 
private static bool lockBajoLowerTone; 

private static bool lockSopranoIncreaseVolume; 
private static bool lockContraltoIncreaseVolume; 
private static bool lockTenorIncreaseVolume; 
private static bool lockBajoIncreaseVolume; 

private static bool lockSopranoLowerVolume; 
private static bool lockContraltoLowerVolume; 
private static bool lockTenorLowerVolume; 
private static bool lockBajoLowerVolume; 

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



    // cerrojos: 
    lockSopranoIncreaseTone = false; 
    lockContraltoIncreaseTone = false; 
    lockTenorIncreaseTone = false; 
    lockBajoIncreaseTone = false; 

    lockSopranoLowerTone = false; 
    lockContraltoLowerTone = false; 
    lockTenorLowerTone = false; 
    lockBajoLowerTone = false; 

    lockSopranoIncreaseVolume = false; 
    lockContraltoIncreaseVolume = false; 
    lockTenorIncreaseVolume = false; 
    lockBajoIncreaseVolume = false; 

    lockSopranoLowerVolume = false; 
    lockContraltoLowerVolume = false; 
    lockTenorLowerVolume = false; 
    lockBajoLowerVolume = false; 

    
}

private static void playSong() {
        startedSong = true; 
        audioSourceSoprano.Play();
        audioSourceContralto.Play();
        audioSourceTenor.Play();
        audioSourceBajo.Play();

        choristSoprano.GetComponent<Animator>().Play("Talking");
        choristContralto.GetComponent<Animator>().Play("Talking");
        choristTenor.GetComponent<Animator>().Play("Talking");
        choristBajo.GetComponent<Animator>().Play("Talking");
    
}

public static bool isSongStarted() {
    //Debug.Log("isSongStarted: " + startSong); 
    return startSong; 
}

public void Update() {
    repeatSong(); 
    
    evaluateStates(); 

    // waiters gestures: 
    StartCoroutine(waiterIncreaseToneSoprano()); 
    StartCoroutine(waiterIncreaseToneContralto());
    StartCoroutine(waiterIncreaseToneTenor());
    StartCoroutine(waiterIncreaseToneBajo());

    StartCoroutine(waiterLowerToneSoprano()); 
    StartCoroutine(waiterLowerToneContralto());
    StartCoroutine(waiterLowerToneTenor());
    StartCoroutine(waiterLowerToneBajo());

    StartCoroutine(waiterIncreaseVolumeSoprano()); 
    StartCoroutine(waiterIncreaseVolumeContralto());
    StartCoroutine(waiterIncreaseVolumeTenor());
    StartCoroutine(waiterIncreaseVolumeBajo());

    StartCoroutine(waiterLowerVolumeSoprano()); 
    StartCoroutine(waiterLowerVolumeContralto());
    StartCoroutine(waiterLowerVolumeTenor());
    StartCoroutine(waiterLowerVolumeBajo());


    
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
    Debug.Log("start song"); 
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
    //Debug.Log("evaluateStates"); 
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







// HANDLERS: 

public static void signSoprano() {
    choristSoprano.GetComponent<Animator>().Play("Talking");
    if(audioSourceSoprano.isPlaying) {
        audioSourceSoprano.mute = false; 
    }
    
}

public static void signContralto() {
    choristContralto.GetComponent<Animator>().Play("Talking");
    if(audioSourceContralto.isPlaying) {
        audioSourceContralto.mute = false; 
    }
    
}

public static void signTenor() {
    choristTenor.GetComponent<Animator>().Play("Talking");
    if(audioSourceTenor.isPlaying) {
        audioSourceTenor.mute = false; 
    }
    
}

public static void signBajo() {
    choristBajo.GetComponent<Animator>().Play("Talking");
    if(audioSourceBajo.isPlaying) {
        audioSourceBajo.mute = false; 
    }
   
}

public static void muteSoprano() {
    choristSoprano.GetComponent<Animator>().Play("StopTalking"); 
    audioSourceSoprano.mute = true; 
}

public static void muteContralto() {
    choristContralto.GetComponent<Animator>().Play("StopTalking"); 
    audioSourceContralto.mute = true; 
}

public static void muteTenor() {
    choristTenor.GetComponent<Animator>().Play("StopTalking"); 
    audioSourceTenor.mute = true;
}

public static void muteBajo() {
    choristBajo.GetComponent<Animator>().Play("StopTalking"); 
    audioSourceBajo.mute = true;
}


private void increaseToneSoprano() {
    //if(!lockSopranoIncreaseTone) {
        //lockSopranoIncreaseTone = true; // lock only one thread.
        changeTone("pitchFieldSoprano", EState.INCREASE_TONE); 
        setSopranoNoChangeState(); 
    //}
}

private void increaseToneContralto() {
    //if(!lockContraltoIncreaseTone) {
        //lockContraltoIncreaseTone = true; // lock only one thread.
        changeTone("pitchFieldContralto", EState.INCREASE_TONE); 
        setContraltoNoChangeState(); 
    //}
}

private void increaseToneTenor() {
    //if(!lockTenorIncreaseTone) {
        //lockTenorIncreaseTone = true; // lock only one thread.
        changeTone("pitchFieldTenor", EState.INCREASE_TONE); 
        setTenorNoChangeState(); 
    //}
}

private void increaseToneBajo() {
    //if(!lockBajoIncreaseTone) {
        //lockBajoIncreaseTone = true; // lock only one thread.
        changeTone("pitchFieldBajo", EState.INCREASE_TONE); 
        setBajoNoChangeState(); 
    //}
}


private void lowerToneSoprano() {
    //if(!lockSopranoLowerTone) {
        //lockSopranoLowerTone = true; // lock only one thread.
        changeTone("pitchFieldSoprano", EState.LOWER_TONE); 
        setSopranoNoChangeState(); 
    //}
}

private void lowerToneContralto() {
    //if(!lockContraltoLowerTone) {
        //lockContraltoLowerTone = true; // lock only one thread.
        changeTone("pitchFieldContralto", EState.LOWER_TONE); 
        setContraltoNoChangeState(); 
    //}
}

private void lowerToneTenor() {
    //if(!lockTenorLowerTone) {
        //lockTenorLowerTone = true; // lock only one thread.
        changeTone("pitchFieldTenor", EState.LOWER_TONE); 
        setTenorNoChangeState(); 
    //}
}

private void lowerToneBajo() {
    //if(!lockBajoLowerTone) {
        //lockBajoLowerTone = true; // lock only one thread.
        changeTone("pitchFieldBajo", EState.LOWER_TONE); 
        setBajoNoChangeState(); 
    //}
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
    //if(!lockSopranoIncreaseVolume) {
        //lockSopranoIncreaseVolume = false; // lock only one thread
        changeVolume("volumeFieldSoprano", EState.INCREASE_VOLUME); 
        setSopranoNoChangeState(); 
    //}
}

private void increaseVolumeContralto() {
    //if(!lockContraltoIncreaseVolume) {
        //lockContraltoIncreaseVolume = false; // lock only one thread
        changeVolume("volumeFieldContralto", EState.INCREASE_VOLUME); 
        setContraltoNoChangeState(); 
    //}
}

private void increaseVolumeTenor() {
    //if(!lockTenorIncreaseVolume) {
        //lockTenorIncreaseVolume = false; // lock only one thread
        changeVolume("volumeFieldTenor", EState.INCREASE_VOLUME); 
        setTenorNoChangeState(); 
    //}
}

private void increaseVolumeBajo() {
    //if(!lockBajoIncreaseVolume) {
        //lockBajoIncreaseVolume = false; // lock only one thread
        changeVolume("volumeFieldBajo", EState.INCREASE_VOLUME); 
        setBajoNoChangeState(); 
    //}
}


private void lowerVolumeSoprano() {
    //if(!lockSopranoLowerVolume) {
        //lockSopranoLowerVolume = false; // lock only one thread
        changeVolume("volumeFieldSoprano", EState.LOWER_VOLUME); 
        setSopranoNoChangeState(); 
    //}
}

private void lowerVolumeContralto() {
    //if(!lockContraltoLowerVolume) {
        //lockContraltoLowerVolume = false; // lock only one thread
        changeVolume("volumeFieldContralto", EState.LOWER_VOLUME); 
        setContraltoNoChangeState(); 
    //}
}

private void lowerVolumeTenor() {
    //if(!lockTenorLowerVolume) {
        //lockTenorLowerVolume = false; // lock only one thread
        changeVolume("volumeFieldTenor", EState.LOWER_VOLUME); 
        setTenorNoChangeState(); 
    //}
}

private void lowerVolumeBajo() {
    //if(!lockBajoLowerVolume) {
        //lockBajoLowerVolume = false; // lock only one thread
        changeVolume("volumeFieldBajo", EState.LOWER_VOLUME); 
        setBajoNoChangeState(); 
    //}
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
IEnumerator waiterIncreaseToneSoprano() {
    if(lockSopranoIncreaseTone) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockSopranoIncreaseTone = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterIncreaseToneContralto() {
    if(lockContraltoIncreaseTone) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockContraltoIncreaseTone = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterIncreaseToneTenor() {
    if(lockTenorIncreaseTone) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockTenorIncreaseTone = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterIncreaseToneBajo() {
    if(lockBajoIncreaseTone) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockBajoIncreaseTone = false;  // unlock increase tone soprano
    }
}


IEnumerator waiterLowerToneSoprano() {
    if(lockSopranoLowerTone) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockSopranoLowerTone = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterLowerToneContralto() {
    if(lockContraltoLowerTone) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockContraltoLowerTone = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterLowerToneTenor() {
    if(lockTenorLowerTone) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockTenorLowerTone = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterLowerToneBajo() {
    if(lockBajoLowerTone) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockBajoLowerTone = false;  // unlock increase tone soprano
    }
}



IEnumerator waiterIncreaseVolumeSoprano() {
    if(lockSopranoIncreaseVolume) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockSopranoIncreaseVolume = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterIncreaseVolumeContralto() {
    if(lockContraltoIncreaseVolume) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockContraltoIncreaseVolume = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterIncreaseVolumeTenor() {
    if(lockTenorIncreaseVolume) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockTenorIncreaseVolume = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterIncreaseVolumeBajo() {
    if(lockBajoIncreaseVolume) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockBajoIncreaseVolume = false;  // unlock increase tone soprano
    }
}


IEnumerator waiterLowerVolumeSoprano() {
    if(lockSopranoLowerVolume) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockSopranoLowerVolume = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterLowerVolumeContralto() {
    if(lockContraltoLowerVolume) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockContraltoLowerVolume = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterLowerVolumeTenor() {
    if(lockTenorLowerVolume) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockTenorLowerVolume = false;  // unlock increase tone soprano
    }
}

IEnumerator waiterLowerVolumeBajo() {
    if(lockBajoLowerVolume) {
        yield return new WaitForSecondsRealtime(2.0f); 
        lockBajoLowerVolume = false;  // unlock increase tone soprano
    }
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