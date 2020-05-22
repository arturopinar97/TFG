using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
using Valve.VR;
using Microsoft.VisualBasic; 
using HI5.VRCalibration;



public class StateController : MonoBehaviour {

//public GameObject box1; 
public AudioClip colliderFeedback; 
public AudioClip wrongMove; 
public AudioClip goodMove; 

public static int state; 
private static int distanceError; // usado para el margen de error de movimiento de la mano.
private bool activeTimer; // cerrojo para el timer. 
private static double lastDistance; 
private static bool lastMoveOk;
public GameObject rightHand; 
public double originalTime; 
public bool tiempoAcabado = false; 
private bool newState = false;


// checkpoints aux boxes:
private static bool checkAux1; 
private static bool checkAux2; 

private bool firstMove; 
AudioSource fuenteAudio; 

// resolver repetidos colliders dentro del cubo: 

private static bool lockCollider1; 
private static bool lockCollider2; 
private static bool lockCollider3; 
private static bool lockCollider4; 

    public StateController(/* int maxStates */){
        //maxStates = 3; // 2 por 4
    }
    public void Start() {
        fuenteAudio = GetComponent<AudioSource> ();
        state = 1; 
        originalTime = 1; 
        newState = false; 
        activeTimer = false; 
        distanceError = 100; 
        lastDistance = Double.PositiveInfinity; 
        lastMoveOk = true; 
        checkAux1 = false; 
        checkAux2 = false; 
        firstMove = true; 
         // evitar collider repetidos al salir la mano de la caja: 
            
        lockCollider1 = false; 
        lockCollider2 = true; 
        lockCollider3 = true; 
        lockCollider4 = true; 
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
        }
        else if(state == 2) {
            GameObject.Find("state1").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GameObject.Find("state2").GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }
    }
   

    private void OnTriggerEnter(Collider other) {
        if(this.name == "state2" && !lockCollider3) { // tocas el cubo 3
            lockColliders(false, false, true, false); 
            if(checkAux1) { // pasas por el checkpoint
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
                Debug.Log("Movimiento mal: No has pasado para la caja 2"); 
                fuenteAudio.clip = wrongMove; 
                fuenteAudio.Play(); 
                resetTempo(); 
            }
            checkAux1 = false; 
        }
        if(this.name == "state1" && !lockCollider1) { // tocas el cubo 1
            lockColliders(true, false, false, false); 
            if(firstMove) {
                firstMove = false; 
                Debug.Log("Entro en primer movimiento. "); 
            }
            else if(checkAux2) { // has pasado por el checkpoint 2
                if(state == 1) {
                    Debug.Log("Segunda parte del movimiento correcta. "); 
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
                Debug.Log("Movimiento mal: No has pasado por la caja 4. "); 
                fuenteAudio.clip = wrongMove; 
                fuenteAudio.Play(); 
                resetTempo(); 
            }
            checkAux2 = false; 

        }
        // aux check boxes: 

        if(this.name == "aux1" && !lockCollider2) {
            lockColliders(false, true, false, false); 
            checkAux1 = true; 
            Debug.Log("Has pasado por el checkpoint 1"); 

        }
        if(this.name == "aux2" && !lockCollider4) {
            lockColliders(false, false, false, true); 
            checkAux2 = true; 
            Debug.Log("Has pasado por el checkpoint 2"); 
        }
    }

    private void lockColliders(bool lock1, bool lock2, bool lock3, bool lock4) {
        lockCollider1 = lock1; 
        lockCollider2 = lock2; 
        lockCollider3 = lock3; 
        lockCollider4 = lock4; 
    }

    private void resetTempo() {
        // reset: 
        checkAux1 = false; 
        checkAux2 = false; 
        lockColliders(true, false, false, false); 
    }
   







}