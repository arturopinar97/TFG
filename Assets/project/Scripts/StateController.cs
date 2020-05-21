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

public static int state; 
private bool activeTimer; // cerrojo para el timer. 
public double originalTime; 
public bool tiempoAcabado = false; 
private int maxStates; 
private bool newState = false;
AudioSource fuenteAudio; 

    public StateController(/* int maxStates */){
        //maxStates = 3; // 2 por 4
    }
    public void Start() {
        fuenteAudio = GetComponent<AudioSource> ();
        state = 1; 
        originalTime = 1; 
        maxStates = 3; 
        newState = false; 
        activeTimer = false; 
    }


    IEnumerator waiter()
    {
			
        if(!activeTimer) {
            fuenteAudio.clip = colliderFeedback; 
            activeTimer = true; // los estados no pueden reiniciarse hasta llegar al final. 
            state = 1; 
            Debug.Log("Estado pasa a ser: " + state);
            fuenteAudio.Play(); 

            yield return new WaitForSecondsRealtime(1); 
            state = 2; 
            Debug.Log("Estado pasa a ser: " + state); 
            fuenteAudio.Play(); 
            yield return new WaitForSecondsRealtime(1);

            state = 3; 
            Debug.Log("Estado pasa a ser: " + state); 
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
        // iluminar cubos checkeando cada estado en cada frame. 
    
    }

    private void luce() {
        if(state == 1) {
        }
        else if(state == 2) {
            // luce 2
        }
        else if(state == 3) {
            // luce 3
        }
    }


    public bool CheckDirection(Vector3 auxPos, Vector3 destPosition, float dist)
    {
        float distTemp = Vector3.Distance(destPosition, auxPos);
        //if(est2)  Debug.Log(distTemp);

        if (distTemp <= dist)
        {
           // mal = false;
            dist = distTemp;
            return true;
        }
        else if (distTemp > dist)
        { 
            dist = distTemp;
          //  mal = true;
           
            return false;
        }
        else return true;


    }

    private void timer() {
        int tmp = -1; 
        originalTime -= Time.deltaTime; 
        
        //originalTime = Math.Floor(Math.Abs(originalTime));  
        //Debug.Log("tiempo: " + originalTime); 
       
        if(originalTime < 0 && !newState) { // el tiempo del estado se ha acabado
            newState = true; // cerrojo
            tmp = state + 1; 
            if(tmp <= 3) {
                state += 1; 
            }
            else if(tmp >= 3) {
                state = 1; 
            }
            Debug.Log("Timer coloca el estado: " + state); 
            originalTime = 1; // se restablece el contador para el siguiente estado
            newState = false; 
            fuenteAudio.clip = colliderFeedback; 
            fuenteAudio.Play(); 
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("SIGUIENTE ESTADO: " + state); 
        if(this.name == "Cube1" && 
            state == 1){
                
                //Debug.Log("collider: " + other.gameObject.tag);
                //Debug.Log("CHOCO CON " + other.gameObject.name); 
                //fuenteAudio.clip = colliderFeedback; 
                //fuenteAudio.Play(); 
                Debug.Log("Has tocado correctamente el primer cubo "); 
                state += 1;  
            }


        if(this.name == "Cube2" && 
            state == 2) {
                //fuenteAudio.clip = colliderFeedback; 
                //fuenteAudio.Play(); 
                Debug.Log("Has tocado correctamente el segundo cubo"); 
                state += 1; 
            }
        if(this.name == "Cube3" && 
            state == 3) {
                //fuenteAudio.clip  = colliderFeedback; 
                //fuenteAudio.Play(); 
                Debug.Log("Has tocado correctamente el tercer cubo "); 
                state = 0; 
            }

        else {
            Debug.Log("No estas haciendo correctamente el gesto. "); 
            Debug.Log("Has cometido un error porque estas en el estado: " + state + "y tocando el cubo: " + this.name); 
        }

      
        //Debug.Log("Ya has activado el cubo 1"); 
        
    }







}