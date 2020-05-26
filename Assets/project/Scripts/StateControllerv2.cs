using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
using Valve.VR;
using Microsoft.VisualBasic; 
using HI5.VRCalibration;



public class StateControllerv2 : MonoBehaviour {

//public GameObject box1; 
public AudioClip colliderFeedback; 
public AudioClip wrongMove; 
public AudioClip goodMove; 

private bool activeTimer; // cerrojo para el timer. 


// hands: 
private const string LEFT_HAND_TAG = "LeftHand"; 
private const string RIGHT_HAND_TAG = "RightHand"; 

private const string RESPONSE_TEXT = "text_response_right_hand_gesture"; 

private GameObject textResponseRightHandGesture;
private bool writtenText; 
private const float LIVE_SECONDS_TEXT = 2; 
private float liveSecondsText; 

private const float SECONDS_PULSE = 1.2f; 



/* ------------ NEW ------------------- */

private static string lastTouched; 
private static int state; 
private static bool afterMove; 
private static bool disableBox3; 
private List<bool> okStates;
private AudioSource fuenteAudio; 





    public StateControllerv2(/* int maxStates */){
        //maxStates = 3; // 2 por 4
    }
    public void Start() {
        textResponseRightHandGesture = GameObject.Find(RESPONSE_TEXT); 
        fuenteAudio = GetComponent<AudioSource> ();
        this.writtenText = false; 
       
        activeTimer = false; 
        disableBox3 = false; 
    
        liveSecondsText = LIVE_SECONDS_TEXT; 
        /* --------- NEW ---------- */ 
        lastTouched = ""; 
        state = 1; 
        afterMove = false; 
        okStates = new List<bool>(); 

            
    }


    IEnumerator waiter()
    {
			
        if(!activeTimer) {
            fuenteAudio.clip = colliderFeedback; 
            activeTimer = true; // los estados no pueden reiniciarse hasta llegar al final. 
            state = 1; 
            //Debug.Log("Estado pasa a ser: " + state);
            fuenteAudio.Play(); 

            yield return new WaitForSecondsRealtime(SECONDS_PULSE); 
            state = 2; 
            //Debug.Log("Estado pasa a ser: " + state); 
            fuenteAudio.Play(); 
            yield return new WaitForSecondsRealtime(SECONDS_PULSE);
            state = 3; 
            fuenteAudio.Play(); 
            yield return new WaitForSecondsRealtime(SECONDS_PULSE); 
            activeTimer = false; // ya pueden volver a reiniciarse los estados.
            
           
        }
        //StartCoroutine(waiter());
    }
    public void Update() {
        //Debug.Log("---- GUANTE: ------  X" + transform.position.x + "Y: " + transform.position.y +  "Z: " + transform.position.z); 
        //Debug.Log("---- CUBO: ------ X: " + box1.transform.position.x + "Y: " + transform.position.y + "Z: " + transform.position.z); 
        //timer(); 
        StartCoroutine(waiter());
        luce(); 
        
        // iluminar cubos checkeando cada estado en cada frame. 
    
    }

    
    private void luce() {
        
        if(state == 1) {
            GameObject.Find("state1").GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            GameObject.Find("state2").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state3").GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); 
        }
        else if(state == 2) {
            GameObject.Find("state1").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state2").GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            GameObject.Find("state3").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
        else if(state == 3) {
            GameObject.Find("state1").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state2").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state3").GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
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
            // 2. Handler del gesto: 
            switch(this.name) {
                case "state1": 
                    handlerState1(); 
                    break; 
                case "aux1": 
                    handlerCommonBoxes(1); 
                    break; 
                case "aux2": 
                    handlerAux2(); 
                    break; 
                case "state2": 
                    handlerCommonBoxes(2); 
                    break; 
                case "aux3": 
                    handlerCommonBoxes(2); 
                    break; 
                case "state3": 
                    handlerCommonBoxes(3); 
                    break; 
                case "aux4": 
                    handlerAux4(); 
                    break; 
                
            }
        }

    }

    private void handlerState1() {
        if(state == 1) {
            disableBox3 = false; 
            if(afterMove == false) {
                okStates.Add(true); // comienza el ciclo. 
                afterMove = true; 
            }
            else {
                if(!okStates.Contains(false)) { // si todos los movimientos han sido correctos
                    feedbackGoodGesture(); 
                    okStates.Clear(); // prepara el nuevo ciclo. 
                    okStates.Add(true); 
                }
            }
        }
        else{
            feedbackError(" --- " + this.name + "--- fuera de tiempo"); 
            handlerError(); 
        }
    }
    private void handlerCommonBoxes(int s) {
        if(afterMove == false) {
            feedbackError("Comienza por la caja 1"); 
        }
        else{
            if(state == s) {
                if(!okStates.Contains(false)) {
                    okStates.Add(true); 
                }
                else{
                    feedbackError("No has pasado por alguna caja"); 
                    handlerError(); 
                }
            }
            else{
                feedbackError(" --- " + this.name + "--- fuera de tiempo");  
                handlerError(); 
            }
        }
    }

    private void handlerAux2() {
        if(disableBox3 == false) { // para el caso cuando bajas de la caja 7 a la 1.
            if(afterMove == false) {
                feedbackError("Comienza por la caja 1"); 
            }
            else{
                if(state == 1) {
                    if(!okStates.Contains(false)) {
                        okStates.Add(true); 
                    }
                    else{
                        feedbackError("No has pasado por alguna caja"); 
                        handlerError();
                    }
                }
                else{
                    feedbackError(" --- " + this.name + "--- fuera de tiempo");  
                    handlerError(); 
                }
            }
        }
    }

    private void handlerAux4() {
        if(afterMove == false) {
            feedbackError("Comienza por la caja 1"); 
        }
        else{
            if(state == 3) {
                if(!okStates.Contains(false)) {
                    //afterMove = true; 
                    disableBox3 = true; // para permitir bajar de la caja 7 a la 1. 
                    okStates.Add(true); 
                }
                else{
                    feedbackError("No has pasado por alguna caja"); 
                    handlerError(); 
                }
            }
            else{
                feedbackError(" --- " + this.name + "--- fuera de tiempo");  
                handlerError(); 
            }
        }
    }

    private void feedbackError(string msg) {
        //Debug.Log("Error: " + msg); 
        updateResponse(msg); 
        fuenteAudio.clip = wrongMove; 
        fuenteAudio.Play(); 
    }

    private void handlerError() {
        okStates.Clear(); 
        afterMove = false; 
        disableBox3 = false; 
        //lastTouched = ""; // desbloquear collider
    }

    private void feedbackGoodGesture() {
        Debug.Log("Buen gesto. "); 
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
        this.writtenText = true; 
         
    }

    private void waitToEraseText() {
        if(this.writtenText){
            this.liveSecondsText -= Time.deltaTime; 
            if(liveSecondsText <= 0) {
                updateResponse(""); 
                this.writtenText = false; 
            }
        }
    }







}