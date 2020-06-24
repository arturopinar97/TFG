using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
using Valve.VR;
using Microsoft.VisualBasic; 
using HI5.VRCalibration;



public class StateController3_4v2 : MonoBehaviour {

//public GameObject box1; 
public AudioClip colliderFeedback; 
public AudioClip wrongMove; 
public AudioClip goodMove; 

private static bool activeTimer; // cerrojo para el timer. 


// hands: 
private const string LEFT_HAND_TAG = "LeftHand"; 
private const string RIGHT_HAND_TAG = "RightHand"; 

private const string RESPONSE_TEXT = "text_response_right_hand_gesture"; 
private const string SECOND_RESPONSE_TEXT = "text_response_second_line"; 


private GameObject textResponseRightHandGesture;
private GameObject textSecond; 
private static bool writtenText; 
private static bool writtenSecondText; 
private const float LIVE_SECONDS_TEXT = 10f; 
private static float liveSecondsText; 
private static float liveSecondsSecondText; 


private const float SECONDS_PULSE = 1f; 



/* ------------ NEW ------------------- */

private static string lastTouched; 
private static int state; 
private static bool afterMove; 
private static  List<bool> okStates;
private AudioSource fuenteAudio; 
private static bool disableMiddleBox;

/* ---------- new after 26/05 init v2 ------------- */ 


private const string RIGHT_HAND_NAME = "Human_RightHand"; 
    
    public void Start() {
        textResponseRightHandGesture = GameObject.Find(RESPONSE_TEXT); 
        textSecond = GameObject.Find(SECOND_RESPONSE_TEXT); 
        fuenteAudio = GetComponent<AudioSource> ();
        writtenText = false; 
        writtenSecondText = false; 
       
        activeTimer = false; 
    
        liveSecondsText = LIVE_SECONDS_TEXT; 
        liveSecondsSecondText = LIVE_SECONDS_TEXT; 
        /* --------- NEW ---------- */ 
        lastTouched = ""; 
        state = 1; 
        afterMove = false; 
        okStates = new List<bool>(); 
        disableMiddleBox = false; 

            
    }


    IEnumerator waiter()
    {
			
        if(!activeTimer) {
            fuenteAudio.clip = colliderFeedback; 
            activeTimer = true; // los estados no pueden reiniciarse hasta llegar al final. 
            state = 1; 
            fuenteAudio.Play(); 

            yield return new WaitForSecondsRealtime(SECONDS_PULSE); 
            state = 2; 
            fuenteAudio.Play(); 
            yield return new WaitForSecondsRealtime(SECONDS_PULSE); 
            state = 3; 
            fuenteAudio.Play(); 
            yield return new WaitForSecondsRealtime(SECONDS_PULSE); 
            activeTimer = false; // ya pueden volver a reiniciarse los estados.
            
           
        }
    }
         

    
    public void Update() {
        StartCoroutine(waiter());
        luce(); 
        waitToEraseText(); 
        waitToEraseSecondText(); 
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
            updateResponse("Ese gesto se debe realizar con la mano derecha"); 
        }
    }
    private void handlerTempo(Collider other) { 
        if(checkIfColliderAvailable(this.name) == true) { 
            Debug.Log("Paso por: " + this.name); 
            Debug.Log("Ok states tiene " + okStates.Count); 
            switch(this.name) {
                case "state1": 
                    handlerState1(); // estado 1
                    break; 
                case "aux1": 
                    handlerCommonBoxes(1, 1); // estado 1
                    break; 
                case "aux2": 
                    handlerMiddleBox(); // estado 1
                    break; 
                case "state2": 
                    handlerCommonBoxes(2, 3); // estado 2
                    break; 
                case "aux3": 
                    handlerCommonBoxes(2, 4); // estado 2
                    break; 
                case "state3": 
                    handlerCommonBoxes(3, 5); // estado 3
                    break;  
                case "aux4": 
                    handlerFinalBox(); // estado 3
                    break; 
                
            }
        }

    }

    private void handlerState1() {
        if(state == 1) {
            if(afterMove == false) {
                Debug.Log("Salgo por afterMove que es " + afterMove); 
                okStates.Add(true); // comienza el ciclo. 
                afterMove = true; 
                disableMiddleBox = false; 
                
            }
            else {
                if(okStates.Count == 7) { // si todos los movimientos han sido correctos
                    feedbackGoodGesture(); 
                    okStates.Clear(); // prepara el nuevo ciclo. 
                    okStates.Add(true); 
                    Debug.Log("segunda vuelta okStates: " + okStates.Count); 
                    }
                else{
                    feedbackError("Error, follow the path!"); 
                    handlerError(); 
                    okStates.Add(true); 
                    afterMove = true; 
                }
            }
        }
        else{
            feedbackError("Out of time!"); 
            handlerError(); 
            okStates.Add(true); 
            afterMove = true; 
        }
    }
    private void handlerCommonBoxes(int s, int prevBoxes) {
        if(prevBoxes > 3) { // ya has pasado middleBox.
            disableMiddleBox = true; 
        }
        if(afterMove == false) {
            updateSecondText("Start from box 1"); 
        }
        else{
            if(state == s) {
                if(okStates.Count == prevBoxes) {
                    okStates.Add(true); 
                }
                else{
                    feedbackError("Error, follow the path! "); 
                    handlerError(); 
                }
            }
            else{
                feedbackError("Out of time! "); 
                handlerError(); 
            }
        }
    }

    private void handlerMiddleBox() {
            Debug.Log("DisableMiddleBox: " + disableMiddleBox); 
            if(disableMiddleBox == false) {
                Debug.Log("Entro en middleBox"); 
                if(afterMove == false) {
                    updateSecondText("Start from box 1");  
                }
                else{
                    if(state == 1) {
                        if(okStates.Count == 2) {
                            okStates.Add(true); 
                        }
                        else{
                            feedbackError("Error, follow the path! "); 
                            handlerError();
                        }
                    }
                    else{
                        feedbackError("Out of time! ");  
                        handlerError(); 
                    }
                }
            }
    }

    private void handlerFinalBox() {
        //disableMiddleBox = true; 
        if(afterMove == false) {
            updateSecondText("Start from box 1");  
        }
        else{
            if(state == 3) {
                if(okStates.Count == 6) {
                    //afterMove = true; 
                    okStates.Add(true); 
                }
                else{
                    feedbackError("Error, follow the path! "); 
                    handlerError(); 
                }
            }
            else{
                feedbackError("Out of time! ");  
                handlerError(); 
            }
        }
    }

    private void feedbackError(string msg) {
        //Debug.Log("Error: " + msg); 
        updateResponse(msg); 
        Debug.Log("cubo " + this.name +  " " + msg); 
        fuenteAudio.clip = wrongMove; 
        fuenteAudio.Play(); 
    }

    private void handlerError() {
        okStates.Clear(); 
        afterMove = false; 
        disableMiddleBox = false; 
        //lastTouched = ""; // desbloquear collider
    }

    private void feedbackGoodGesture() {
        Debug.Log("soy: " + this.name + "Buen gesto. "); 
        updateResponse("Well done!"); 
        fuenteAudio.clip = goodMove; 
        fuenteAudio.Play(); 
        disableMiddleBox = false; 
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
        this.textSecond.GetComponent<TextMesh>().text = ""; // reseteas el segundo texto porque el primero es el importante. 
        writtenText = true; 
        liveSecondsText = LIVE_SECONDS_TEXT; 
         
    }

    private void updateSecondText(String text) {
        this.textSecond.GetComponent<TextMesh>().text = text; 
        writtenSecondText = true; 
        liveSecondsSecondText = LIVE_SECONDS_TEXT; 
    }

    private void waitToEraseText() {
        //Debug.Log("writtenText: " + writtenText);
        if(writtenText == true){
            liveSecondsText -= Time.deltaTime; 
            //Debug.Log("liveSecondsText: " + liveSecondsText); 
            if(liveSecondsText <= 0) {
                this.textResponseRightHandGesture.GetComponent<TextMesh>().text = "";
                writtenText = false; 
                //liveSecondsText = LIVE_SECONDS_TEXT; 
                
            }
        }
    }

    private void waitToEraseSecondText() {
        if(writtenSecondText == true) {
            liveSecondsSecondText -= Time.deltaTime; 
            if(liveSecondsSecondText <= 0) {
                this.textSecond.GetComponent<TextMesh>().text = "";  
                writtenSecondText = false; 
                //liveSecondsSecondText = LIVE_SECONDS_TEXT; 
            }
        }
    }


}