using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
using Valve.VR;
using Microsoft.VisualBasic; 
using HI5.VRCalibration;



public class StateController4_4v2 : MonoBehaviour {

//public GameObject box1; 
public AudioClip colliderFeedback; 
public AudioClip wrongMove; 
public AudioClip goodMove; 

private static bool activeTimer; // cerrojo para el timer. 


// hands: 
private const string LEFT_HAND_TAG = "LeftHand"; 
private const string RIGHT_HAND_TAG = "RightHand"; 

private const string RESPONSE_TEXT = "text_response_right_hand_gesture"; 

private GameObject textResponseRightHandGesture;
private static bool writtenText; 
private const float LIVE_SECONDS_TEXT = 10f; 
private static float liveSecondsText; 

private const float SECONDS_PULSE = 1f; 



/* ------------ NEW ------------------- */

private static string lastTouched; 
private static int state; 
private static bool afterMove; 
private static bool disableBox5; 
private List<bool> okStates;
private AudioSource fuenteAudio; 

/* ---------- new after 26/05 init v2 ------------- */ 

private const string RIGHT_HAND_NAME = "Human_RightHand"; 

// Detect asynchronus gestures: 

class Vector3Wrapper {
    bool validValue; 
    Vector3 vector; 

    public Vector3Wrapper() {
        validValue = false; 
    }
    
    // getters and setters: 
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
private static Vector3Wrapper originalPosition; 
private static Vector3Wrapper finalPosition; 
public AudioClip testing; 
public const float STATIC_BORDER_MOVE = 0.01f; // umbral testeado para 0.05 segundos. 
private const float TIME_DETECTION_ASYNC_GESTURE = 0.05f;
private static bool lockTimeAsyncGesture; 





    public void Start() {
        textResponseRightHandGesture = GameObject.Find(RESPONSE_TEXT); 
        fuenteAudio = GetComponent<AudioSource> ();
        writtenText = false; 
       
        activeTimer = false; 
        disableBox5 = false; 
    
        liveSecondsText = LIVE_SECONDS_TEXT; 
        /* --------- NEW ---------- */ 
        lastTouched = ""; 
        state = 1; 
        afterMove = false; 
        okStates = new List<bool>(); 
        /* -------- NEW V2 --------- */ 
        originalPosition = new Vector3Wrapper(); 
        finalPosition = new Vector3Wrapper(); 
        lockTimeAsyncGesture = false; 

            
    }


    IEnumerator waiter()
    {
			
        if(!activeTimer) {
            fuenteAudio.clip = colliderFeedback; 
            activeTimer = true; // los estados no pueden reiniciarse hasta llegar al final. 
            state = 1; 
            //Debug.Log("Estado pasa a ser: " + state); 
            fuenteAudio.Play(); 
            //Debug.Log("Original position: " + originalPosition); 
            //detectAsynchronusGesture(GameObject.Find(RIGHT_HAND_NAME)); 
            yield return new WaitForSecondsRealtime(SECONDS_PULSE); 
            //detectAsynchronusGesture(GameObject.Find(RIGHT_HAND_NAME)); 
            //Debug.Log("Final position: " + finalPosition); 
            state = 2; 
            //Debug.Log("Estado pasa a ser: " + state); 
            fuenteAudio.Play(); 
            yield return new WaitForSecondsRealtime(SECONDS_PULSE);
            //detectAsynchronusGesture(GameObject.Find(RIGHT_HAND_NAME)); 
            state = 3; 
            fuenteAudio.Play(); 
            yield return new WaitForSecondsRealtime(SECONDS_PULSE); 
            state = 4; 
            fuenteAudio.Play(); 
            yield return new WaitForSecondsRealtime(SECONDS_PULSE); 
            activeTimer = false; // ya pueden volver a reiniciarse los estados.
            
           
        }
        //StartCoroutine(waiter());
    }

    IEnumerator waiterDetectionAsyncGesture() {
        if(lockTimeAsyncGesture == false) {
            lockTimeAsyncGesture = true; 
            yield return new WaitForSecondsRealtime(TIME_DETECTION_ASYNC_GESTURE);
            detectAsynchronusGesture(GameObject.Find(RIGHT_HAND_NAME)); 
            lockTimeAsyncGesture = false; 
        }
        
         

    }
    public void Update() {
        //Debug.Log("---- GUANTE: ------  X" + transform.position.x + "Y: " + transform.position.y +  "Z: " + transform.position.z); 
        //Debug.Log("---- CUBO: ------ X: " + box1.transform.position.x + "Y: " + transform.position.y + "Z: " + transform.position.z); 
        //timer(); 
        StartCoroutine(waiter());
        luce(); 
        waitToEraseText(); 

        // --- v2 --- 
        StartCoroutine(waiterDetectionAsyncGesture()); 
        
        // iluminar cubos checkeando cada estado en cada frame. 

        /* quitar */ 
        if(Input.GetKeyDown(KeyCode.G)) {
            feedbackGoodGesture(); 
        }
        else if(Input.GetKeyDown(KeyCode.H)) {
            feedbackError("Out of time! Start from box 1");
        }
        else if(Input.GetKeyDown(KeyCode.J)) {
            feedbackError("Error, follow the path! Start from box 1");
        }
        

    
    }

    
    private void luce() {
        
        if(state == 1) {
            GameObject.Find("state1").GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            GameObject.Find("state2").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state3").GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); 
            GameObject.Find("state4").GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); 
        }
        else if(state == 2) {
            GameObject.Find("state1").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state2").GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            GameObject.Find("state3").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state4").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
        else if(state == 3) {
            GameObject.Find("state1").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state2").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state3").GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            GameObject.Find("state4").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
        else if(state == 4) {
            GameObject.Find("state1").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state2").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state3").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state4").GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }
    }
   

    private void OnTriggerEnter(Collider other) {
        if(other.tag == RIGHT_HAND_TAG) {
            handlerTempo(other); 
        }
        else if(other.tag == LEFT_HAND_TAG) {
            Debug.Log("Ese gesto se debe realizar con la mano derecha"); 
        }
    }
    private void handlerTempo(Collider other) { 
        // 1. Comprobar que la mano no este dentro del la caja ya con anterioridad: 
        if(checkIfColliderAvailable(this.name) == true) { 
            Debug.Log("Paso por: " + this.name + "en el estado: " + state); 
            // 2. Handler del gesto: 
            switch(this.name) {
                case "state1": 
                    handlerState1(); 
                    break; 
                case "aux1": 
                    handlerCommonBoxes(1); 
                    break; 
                case "state2": 
                    handlerCommonBoxes(2); 
                    break; 
                case "aux2": 
                    handlerCommonBoxes(2); 
                    break; 
                case "aux3": 
                    handlerMiddleBox(); 
                    break; 
                case "state3": 
                    handlerCommonBoxes(3); 
                    break; 
                case "aux4": 
                    handlerCommonBoxes(3); 
                    break; 
                case "state4": 
                    handlerCommonBoxes(4); 
                    break; 
                case "aux5": 
                    handlerLastBox(); 
                    break; 
                
            }
        }

    }

    private void handlerState1() {
        if(state == 1) {
            disableBox5 = false; 
            if(afterMove == false) {
                okStates.Add(true); // comienza el ciclo. 
                afterMove = true; 
            }
            else {
                if(!okStates.Contains(false)) { // si todos los movimientos han sido correctos
                    //feedbackGoodGesture(); 
                    okStates.Clear(); // prepara el nuevo ciclo. 
                    okStates.Add(true); 
                }
            }
        }
        else{
            //feedbackError("Out of time! Start from box 1"); 
            //handlerError(); 
        }
    }
    private void handlerCommonBoxes(int s) {
        if(afterMove == false) {
            Debug.Log("Te has equivocado.Comienza por la caja 1"); 
        }
        else{
            if(state == s) {
                if(!okStates.Contains(false)) {
                    okStates.Add(true); 
                }
                else{
                    //feedbackError("Error, follow the path! Start from box 1"); 
                    //handlerError(); 
                }
            }
            else{
                //feedbackError("Out of time! Start from box 1"); 
                //handlerError(); 
            }
        }
    }

    private void handlerMiddleBox() {
        if(disableBox5 == false) { // para el caso cuando bajas de la caja 9 a la 1.
            if(afterMove == false) {
                Debug.Log("Te has equivocado.Comienza por la caja 1"); 
            }
            else{
                if(state == 2) {
                    if(!okStates.Contains(false)) {
                        okStates.Add(true); 
                    }
                    else{
                        //feedbackError("Error, follow the path! Start from box 1"); 
                        //handlerError();
                    }
                }
                else{
                    //feedbackError("Out of time! Start from box 1");  
                    //handlerError(); 
                }
            }
        }
    }

    private void handlerLastBox() {
        if(afterMove == false) {
            Debug.Log(" Te has equivocado. Comienza por la caja 1"); 
        }
        else{
            if(state == 4) {
                if(!okStates.Contains(false)) {
                    //afterMove = true; 
                    disableBox5 = true; // para permitir bajar de la caja 9 a la 1. 
                    okStates.Add(true); 
                }
                else{
                    //feedbackError("Error, follow the path! Start from box 1"); 
                    //handlerError(); 
                }
            }
            else{
                //feedbackError("Out of time! Start from box 1");  
                //handlerError(); 
            }
        }
    }

    private void feedbackError(string msg) {
        //Debug.Log("Error: " + msg); 
        //Debug.Log(msg);
        Debug.Log("------------ ERROR LOG ----------------"); 
        Debug.Log("State: " + state); 
        Debug.Log("Cubo: " + this.name); 
        int i = 0; 
        foreach(Boolean okState in okStates) {
            Debug.Log("cubo " + i + " okState: " + okState);
        }
        Debug.Log("------------ END ERROR LOG -------------");    
        updateResponse(msg); 
        //fuenteAudio.clip = wrongMove; 
        //fuenteAudio.Play(); 
    }

    private void handlerError() {
        okStates.Clear(); 
        afterMove = false; 
        disableBox5 = false; 
        //lastTouched = ""; // desbloquear collider
    }

    private void feedbackGoodGesture() {
        Debug.Log("soy: " + this.name + "Buen gesto. "); 
        updateResponse("Well done!"); 
        fuenteAudio.clip = goodMove; 
        fuenteAudio.Play(); 
    }

    
    private bool checkIfColliderAvailable(string newCollider) {
        if(newCollider != lastTouched) {
            lastTouched = newCollider; 
            return true; 
        }
        else {
            return false; 
        }
    }

    

    /* --------------------- TEXT RESPONSE HANDLER --------------------------- */ 
    private void updateResponse(String text) {
        this.textResponseRightHandGesture.GetComponent<TextMesh>().text = text;
        writtenText = true; 
         
    }

    private void waitToEraseText() {
        //Debug.Log("writtenText: " + writtenText);
        if(writtenText == true){
            liveSecondsText -= Time.deltaTime; 
            //Debug.Log("liveSecondsText: " + liveSecondsText); 
            if(liveSecondsText <= 0) {
                updateResponse(""); 
                writtenText = false; 
                liveSecondsText = LIVE_SECONDS_TEXT; 
                
            }
        }
    }



    /* ------------------------- NEW AFTER 26/05 INIT V2 ------------------- */ 

    private void setOriginalPosition(GameObject hand) {
        originalPosition.setVector(hand.transform.position); 
        originalPosition.setValid(true); 
    }

    private void setFinalPosition(GameObject hand) {
        finalPosition.setVector(hand.transform.position);
        finalPosition.setValid(true); 
    }

    private void detectAsynchronusGesture(GameObject positionObject) {
        if(originalPosition.getValid() == false) {
            setOriginalPosition(positionObject); 
        }
        else if(finalPosition.getValid() == false) {
            setFinalPosition(positionObject); 
        }
        if(originalPosition.getValid() == true && finalPosition.getValid() == true) {
            float distance = Vector3.Distance(originalPosition.getVector(), finalPosition.getVector()); 
            //Debug.Log("Distancia entre ambos puntos: " + distance);
            // reset
            originalPosition.setValid(false); 
            finalPosition.setValid(false); 
            if(distance < STATIC_BORDER_MOVE) {
                //fuenteAudio.clip = testing; 
                //fuenteAudio.Play();
                //Debug.Log("HACIENDO GESTO ASINCRONO");  
            }
            else{
                //Debug.Log("No esta haciendo gesto asincrono"); 
            }
        }

    }   







}