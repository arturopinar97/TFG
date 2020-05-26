using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
using Valve.VR;
using Microsoft.VisualBasic; 
using HI5.VRCalibration;



public class StateController3_4 : MonoBehaviour {

//public GameObject box1; 
public AudioClip colliderFeedback; 
public AudioClip wrongMove; 
public AudioClip goodMove; 

public static int state; 
private bool activeTimer; // cerrojo para el timer. 


// hands: 
private const string LEFT_HAND_TAG = "LeftHand"; 
private const string RIGHT_HAND_TAG = "RightHand"; 

private const string RESPONSE_TEXT = "text_response_right_hand_gesture"; 

private GameObject textResponseRightHandGesture;
private bool writtenText; 
private const float LIVE_SECONDS_TEXT = 2; 
private float liveSecondsText; 

// checkpoints aux boxes:
private static bool checkAux1; 
private static bool checkAux2; 
private static bool checkAux3; 
private static bool checkAux4; 

private bool firstMove; 
AudioSource fuenteAudio; 

// resolver repetidos colliders dentro del cubo: 

private static bool lockCollider1; 
private static bool lockCollider2; 
private static bool lockCollider3; 
private static bool lockCollider4; 
private static bool lockCollider5;
private static bool lockCollider6;
private static bool lockCollider7; 

    public StateController3_4(/* int maxStates */){
        //maxStates = 3; // 2 por 4
    }
    public void Start() {
        textResponseRightHandGesture = GameObject.Find(RESPONSE_TEXT); 
        fuenteAudio = GetComponent<AudioSource> ();
        this.writtenText = false; 
        state = 1; 
        activeTimer = false; 
        checkAux1 = false; 
        checkAux2 = false; 
        checkAux3 = false; 
        checkAux4 = false; 
        firstMove = true; 
        liveSecondsText = LIVE_SECONDS_TEXT; 
         // evitar collider repetidos al salir la mano de la caja: 
            
        lockCollider1 = false; 
        lockCollider2 = true; 
        lockCollider3 = true; 
        lockCollider4 = true; 
        lockCollider5 = true;
        lockCollider6 = true;
        lockCollider7 = true; 
    }


    IEnumerator waiter()
    {
			
        if(!activeTimer) {
            fuenteAudio.clip = colliderFeedback; 
            activeTimer = true; // los estados no pueden reiniciarse hasta llegar al final. 
            state = 1; 
            //Debug.Log("Estado pasa a ser: " + state);
            fuenteAudio.Play(); 

            yield return new WaitForSecondsRealtime(1); 
            state = 2; 
            //Debug.Log("Estado pasa a ser: " + state); 
            fuenteAudio.Play(); 
            yield return new WaitForSecondsRealtime(1);
            state = 3; 
            fuenteAudio.Play(); 
            yield return new WaitForSecondsRealtime(1); 
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
        if(this.name == "state3" && !lockCollider6) { // tocas el cubo 6
            lockColliders(false, false, false, false, false, true, false); 
            if(checkAux3) { // has pasado por el checkpoint 3
                if(state == 3) {
                    Debug.Log("Segunda parte del movimiento correcta. "); 
                }
                else{
                    Debug.Log("Movimiento demasiado lento o demasiado rapido. "); 
                    fuenteAudio.clip = wrongMove; 
                    fuenteAudio.Play(); 
                    resetTempo(); 
                }
            }
            else{
                Debug.Log("Movimiento mal: No has pasado por la caja 5. "); 
                fuenteAudio.clip = wrongMove; 
                fuenteAudio.Play(); 
                resetTempo(); 
            }
            checkAux3 = false; 

        }
        if(this.name == "state2" && !lockCollider4) { // tocas el cubo 4
            lockColliders(false, false, false, true, false, false, false); 
            if(checkAux1 && checkAux2) { // has pasado por los checkpoints 1 y 2.
                if(state == 2) { // lo has hecho en el tiempo adecuado
                    Debug.Log("Primera parte del movimiento correcta. "); 
                }
                else{
                    Debug.Log("Movimiento demasiado lento o rapido. "); 
                    fuenteAudio.clip = wrongMove; 
                    fuenteAudio.Play();
                    resetTempo(); 
                }
            }
            else{
                Debug.Log("Movimiento mal: No has pasado para la caja 2 o la 3"); 
                fuenteAudio.clip = wrongMove; 
                fuenteAudio.Play(); 
                resetTempo(); 
            }
            checkAux1 = false;
            checkAux2 = false;  
        }
        if(this.name == "state1" && !lockCollider1) { // tocas el cubo 1
            Debug.Log("ENTRO ESTADO 1"); 
            lockColliders(true, false, false, false, false, false, false); 
            if(firstMove) {
                firstMove = false; 
                Debug.Log("Entro en primer movimiento. "); 
            }
            else if(checkAux4) { // has pasado por el checkpoint 4
                if(state == 1) {
                    Debug.Log("Movimiento correcto. "); 
                    fuenteAudio.clip = goodMove; 
                    fuenteAudio.Play(); 
                }
                else{
                    Debug.Log("Movimiento demasiado lento o demasiado rapido. "); 
                    fuenteAudio.clip = wrongMove; 
                    fuenteAudio.Play(); 
                    resetTempo(); 
                }
            }
            else{
                Debug.Log("Movimiento mal: No has pasado por la caja 7. "); 
                fuenteAudio.clip = wrongMove; 
                fuenteAudio.Play(); 
                resetTempo(); 
            }
            checkAux4 = false; 

        }
        // aux check boxes: 

        if(this.name == "aux1" && !lockCollider2) {
            lockColliders(false, true, false, false, false, false, false); 
            checkAux1 = true; 
            Debug.Log("Has pasado por el checkpoint 1"); 

        }
        if(this.name == "aux2" && !lockCollider3) {
            lockColliders(false, false, true, false, false, false, false); 
            checkAux2 = true; 
            Debug.Log("Has pasado por el checkpoint 2"); 
        }
        if(this.name == "aux3" && !lockCollider5) {
            lockColliders(false, false, false, false, true, false, false); 
            checkAux3 = true; 
            Debug.Log("Has pasado por el checkpoint 3"); 
        }
        if(this.name == "aux4" && !lockCollider7) {
            lockColliders(false, false, false, false, false, false, true); 
            checkAux4 = true; 
            Debug.Log("Has pasado por el checkpoint 4"); 
        }
    }

    private void lockColliders(bool lock1, bool lock2, bool lock3, bool lock4, bool lock5, bool lock6, bool lock7) {
        lockCollider1 = lock1; 
        lockCollider2 = lock2; 
        lockCollider3 = lock3; 
        lockCollider4 = lock4; 
        lockCollider5 = lock5; 
        lockCollider6 = lock6; 
        lockCollider7 = lock7; 
    }

    private void resetTempo() {
        // reset: 
        checkAux1 = false; 
        checkAux2 = false; 
        checkAux3 = false; 
        checkAux4 = false; 
        lockColliders(true, false, false, false, false, false, false); 
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