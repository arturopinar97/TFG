using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
using Valve.VR;
using Microsoft.VisualBasic; 
using HI5.VRCalibration;

namespace ChoVR_Core{
public class AdvancedController : MonoBehaviour {


private static bool activeTimer; 


class Vector3Wrapper {
    bool validValue; 
    Vector3 vector; 

    public Vector3Wrapper() {
        validValue = false; 
    }
    
    public void setValid(bool value) {
        this.validValue = value; 
    }
    public bool getValid() {
        return this.validValue; 
    }

    public void setVector(Vector3 vector) {
        this.vector = vector; 
    }
    public Vector3 getVector() {
        return this.vector; 
    }
}

private static Vector3Wrapper originalPositionLeft; 
private static Vector3Wrapper finalPositionLeft; 

private static Vector3Wrapper originalPositionRight; 
private static Vector3Wrapper finalPositionRight; 
private const float SECONDS_PULSE = 1.5f; 

AudioSource fuenteAudio; 

private static int state; 




private const string RIGHT_HAND_NAME = "Human_RightHand"; 
private const string LEFT_HAND_NAME = "Human_LeftHand"; 

public const float STATIC_BORDER_MOVE = 0.01f; 
private const float TIME_DETECTION_ASYNC_GESTURE = 0.05f;
private static bool lockTimeAsyncGestureLeft; 
private static bool lockTimeAsyncGestureRight; 

private static bool asyncGestureLeft; 
private static bool asyncGestureRight; 


public enum ETarget {
    ESoprano, 
    EContralto, 
    ETenor, 
    EBajo
}

public enum EGesture {
    EIncreaseTone, 
    ELowerTone, 
    EIncreaseVolume, 
    ELowerVolume, 
    EMute, 
    ESign
}

public enum EHand {
    ELeftHand, 
    ERightHand
}
private static  ETarget target; 
private static EGesture gesture; 

private static EHand currentHand; 

private static List<string> collisionTargets; 


private const string CHARACTER_SOPRANO = "SopranoCharacterArea"; 
private const string CHARACTER_CONTRALTO = "ContraltoCharacterArea"; 
private const string CHARACTER_TENOR = "TenorCharacterArea"; 
private const string CHARACTER_BAJO = "BassCharacterArea"; 

private const string RIGHT_HAND_TAG = "RightHand"; 
private const string LEFT_HAND_TAG = "LeftHand"; 


public static void setTarget(ETarget targetVar) {
    target = targetVar; 
}
public static ETarget getTarget() {
    return target;
}


public static void setCurrentHand(string handTag) {
    switch(handTag) {
        case RIGHT_HAND_TAG: 
            currentHand = EHand.ERightHand; 
            break; 
        case LEFT_HAND_TAG: 
            currentHand = EHand.ELeftHand; 
            break; 
    }
}

public static void setGesture(EGesture gestureVar) {
    gesture = gestureVar; 
}
public static EGesture getGesture(EGesture gesture) {
    return gesture; 
}

public static List<string> getCollisionTargets() {
    return collisionTargets; 
}
public static void setCollisionTargets(List<string> collisionTargetsVar) {
    collisionTargets = collisionTargetsVar; 
}


public static void selectTarget(string targetName) {
    switch(targetName) {
        case CHARACTER_SOPRANO: 
            setTarget(ETarget.ESoprano); 
            break; 
        case CHARACTER_CONTRALTO: 
            setTarget(ETarget.EContralto); 
            break; 
        case CHARACTER_TENOR: 
            setTarget(ETarget.ETenor); 
            break; 
        case CHARACTER_BAJO: 
            setTarget(ETarget.EBajo); 
            break; 
    }
}

public static void handlerGesture() {
    if(verifyAsyncGesture() && isGestureAllowedByHand() 
        && CharacterController.isSongStarted()) {
            //Debug.Log("gesto activos"); 
        actWithGesture(); 
    }
}

private static void actWithGesture() {
    switch(gesture) {
        case EGesture.ESign: 
            signGesture(); 
            break; 
        case EGesture.EMute: 
            muteGesture(); 
            break;
        case EGesture.EIncreaseVolume: 
            increaseVolumeGesture(); 
            break; 
        case EGesture.ELowerVolume: 
            lowerVolumeGesture(); 
            break; 
        case EGesture.EIncreaseTone: 
            increaseToneGesture(); 
            break; 
        case EGesture.ELowerTone: 
            lowerToneGesture(); 
            break; 

    }
}

private static void signGesture() {
    switch(target) {
        case ETarget.ESoprano: 
            CharacterController.signSoprano(); 
            break;
        case ETarget.EContralto: 
            CharacterController.signContralto(); 
            break; 
        case ETarget.ETenor: 
            CharacterController.signTenor(); 
            break; 
        case ETarget.EBajo: 
            CharacterController.signBajo(); 
            break; 
    }
}

private static void muteGesture() {
    switch(target) {
        case ETarget.ESoprano: 
            CharacterController.muteSoprano(); 
            break; 
        case ETarget.EContralto: 
            CharacterController.muteContralto(); 
            break; 
        case ETarget.ETenor: 
            CharacterController.muteTenor(); 
            break; 
        case ETarget.EBajo: 
            CharacterController.muteBajo(); 
            break; 
    }
}

private static void increaseVolumeGesture() {
    switch(target) {
        case ETarget.ESoprano: 
            CharacterController.setStateSoprano(CharacterController.EState.INCREASE_VOLUME); 
            break; 
        case ETarget.EContralto: 
            CharacterController.setStateContralto(CharacterController.EState.INCREASE_VOLUME);
            break; 
        case ETarget.ETenor: 
            CharacterController.setStateTenor(CharacterController.EState.INCREASE_VOLUME);  
            break; 
        case ETarget.EBajo: 
            CharacterController.setStateBajo(CharacterController.EState.INCREASE_VOLUME);  
            break; 
    }
}

private static void lowerVolumeGesture() {
    switch(target) {
        case ETarget.ESoprano: 
            CharacterController.setStateSoprano(CharacterController.EState.LOWER_VOLUME); 
            break; 
        case ETarget.EContralto: 
            CharacterController.setStateContralto(CharacterController.EState.LOWER_VOLUME); 
            break; 
        case ETarget.ETenor: 
            CharacterController.setStateTenor(CharacterController.EState.LOWER_VOLUME); 
            break; 
        case ETarget.EBajo: 
            CharacterController.setStateBajo(CharacterController.EState.LOWER_VOLUME); 
            break;
    }
}

private static void increaseToneGesture() {
    switch(target) {
        case ETarget.ESoprano: 
            CharacterController.setStateSoprano(CharacterController.EState.INCREASE_TONE); 
            break; 
        case ETarget.EContralto: 
            CharacterController.setStateContralto(CharacterController.EState.INCREASE_TONE);
            break; 
        case ETarget.ETenor: 
            CharacterController.setStateTenor(CharacterController.EState.INCREASE_TONE); 
            break; 
        case ETarget.EBajo: 
            CharacterController.setStateBajo(CharacterController.EState.INCREASE_TONE); 
            break; 
    }
}

private static void lowerToneGesture() {
    switch(target) {
        case ETarget.ESoprano: 
            CharacterController.setStateSoprano(CharacterController.EState.LOWER_TONE); 
            break; 
        case ETarget.EContralto: 
            CharacterController.setStateContralto(CharacterController.EState.LOWER_TONE); 
            break; 
        case ETarget.ETenor: 
            CharacterController.setStateTenor(CharacterController.EState.LOWER_TONE); 
            break; 
        case ETarget.EBajo: 
            CharacterController.setStateBajo(CharacterController.EState.LOWER_TONE); 
            break; 
    }
}


private static bool isGestureAllowedByHand() {
    //Debug.Log("handisGestureAllowedByHand: " + currentHand); 
    //Debug.Log("gestureIsGestureAllowedByHand: " + gesture);
    if(currentHand == EHand.ELeftHand) {
        if(gesture == EGesture.EIncreaseVolume) {
            return true;
        }
        else if(gesture == EGesture.ELowerVolume) {
            return true; 
        }
        else if(gesture == EGesture.EIncreaseTone) {
            return true; 
        }
        else if(gesture == EGesture.ELowerTone) {
            return true; 
        }
        else if(gesture == EGesture.EMute) {
            return true; 
        }
        else if(gesture == EGesture.ESign) {
            return true; 
        }
    }
    else if(currentHand == EHand.ERightHand) {
        if(gesture == EGesture.EMute) {
            //Debug.Log("gesto permitido"); 
            return true; 
        }
        else if(gesture == EGesture.ESign) {
            return true; 
        }
    }
    return false; 
}

private static bool verifyAsyncGesture() {
    //Debug.Log("verifyAsyncGesture- current hand: " + currentHand);
    //Debug.Log("verifyAsyncGesture - asyncGestureLeft: " + asyncGestureLeft); 
    //Debug.Log("verifyAsyncGesture - asyncGestureRight: " + asyncGestureRight); 
    if(currentHand == EHand.ELeftHand) {
        if(asyncGestureLeft) {
            //Debug.Log("gesto asincrono"); 
            return true; 
        }
        else{
            return false; 
        }
    }
    else if(currentHand == EHand.ERightHand){
        if(asyncGestureRight) {
            return true; 
        }
        else{
            return false;
        }
    }
    else{
        Debug.Log("Error de tags"); 
        return false; 
    }
}



    
    public void Start() {
        collisionTargets = new List<string>(); 
        collisionTargets.Add(CHARACTER_SOPRANO); 
        collisionTargets.Add(CHARACTER_CONTRALTO); 
        collisionTargets.Add(CHARACTER_TENOR); 
        collisionTargets.Add(CHARACTER_BAJO);
        lockTimeAsyncGestureLeft = false; 
        lockTimeAsyncGestureRight = false; 
        originalPositionLeft = new Vector3Wrapper(); 
        finalPositionLeft = new Vector3Wrapper();
        originalPositionRight = new Vector3Wrapper(); 
        finalPositionRight = new Vector3Wrapper();
    }
    public void Update() {

        StartCoroutine(waiterDetectionAsyncGestureRight());  
        StartCoroutine(waiterDetectionAsyncGestureLeft()); 
    }

    IEnumerator waiterDetectionAsyncGestureRight() {
        if(lockTimeAsyncGestureLeft == false) {
            lockTimeAsyncGestureLeft = true; 
            yield return new WaitForSecondsRealtime(TIME_DETECTION_ASYNC_GESTURE);
            detectAsynchronusGesture(GameObject.Find(RIGHT_HAND_NAME)); 
            lockTimeAsyncGestureLeft = false; 
        }
    }

    IEnumerator waiterDetectionAsyncGestureLeft() {
        if(lockTimeAsyncGestureRight == false) {
            lockTimeAsyncGestureRight = true; 
            yield return new WaitForSecondsRealtime(TIME_DETECTION_ASYNC_GESTURE);
            detectAsynchronusGesture(GameObject.Find(LEFT_HAND_NAME)); 
            lockTimeAsyncGestureRight = false; 
        }
    }


     private void setOriginalPosition(GameObject hand) {
        if(hand.tag == LEFT_HAND_TAG) {
            originalPositionLeft.setVector(hand.transform.position); 
            originalPositionLeft.setValid(true); 
        }
        else if(hand.tag == RIGHT_HAND_TAG) {
            originalPositionRight.setVector(hand.transform.position); 
            originalPositionRight.setValid(true);
        }
        else{
            Debug.Log("Error de tags"); 
        }
    }

    private void setFinalPosition(GameObject hand) {
        if(hand.tag == LEFT_HAND_TAG) {
            finalPositionLeft.setVector(hand.transform.position);
            finalPositionLeft.setValid(true); 
        }
        else if(hand.tag == RIGHT_HAND_TAG) {
            finalPositionRight.setVector(hand.transform.position);
            finalPositionRight.setValid(true); 
        }
        else{
            Debug.Log("Error de tags"); 
        }
    }

    private void detectAsynchronusGesture(GameObject hand) {
        //Debug.Log("handTag: " + hand.tag); 
        if(hand.tag == RIGHT_HAND_TAG) {
            detectAsyncGestureRight(hand);
        }
        else if(hand.tag == LEFT_HAND_TAG) {
            detectAsyncGestureLeft(hand); 
        }
        else{
            Debug.Log("Error de tags"); 
        }

    }

    private void detectAsyncGestureLeft(GameObject hand) {
        if(originalPositionLeft.getValid() == false) {
            setOriginalPosition(hand); 
        }
        else if(finalPositionLeft.getValid() == false) {
            setFinalPosition(hand); 
        }
        if(originalPositionLeft.getValid() == true && finalPositionLeft.getValid() == true) {
            float distance = Vector3.Distance(originalPositionLeft.getVector(), finalPositionLeft.getVector()); 
            originalPositionLeft.setValid(false); 
            finalPositionLeft.setValid(false); 
            if(distance < STATIC_BORDER_MOVE) {
                asyncGestureLeft = true; 
            }
            else{ 
                asyncGestureLeft = false; 
            }
        }
    }

    private void detectAsyncGestureRight(GameObject hand) {
        if(originalPositionRight.getValid() == false) {
            setOriginalPosition(hand); 
        }
        else if(finalPositionRight.getValid() == false) {
            setFinalPosition(hand); 
        }
        if(originalPositionRight.getValid() == true && finalPositionRight.getValid() == true) {
            float distance = Vector3.Distance(originalPositionRight.getVector(), finalPositionRight.getVector()); 
            originalPositionRight.setValid(false); 
            finalPositionRight.setValid(false); 
            if(distance < STATIC_BORDER_MOVE) {
                //Debug.Log("HACIENDO GESTO ASINCRONO");  
                asyncGestureRight = true; 
            }
            else{
                //Debug.Log("No esta haciendo gesto asincrono"); 
                asyncGestureRight = false; 
            }
        }
    }   
    






}

}