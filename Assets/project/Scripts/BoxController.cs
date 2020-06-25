using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
using Valve.VR;
using Microsoft.VisualBasic; 
using HI5.VRCalibration;

namespace ChoVR_Core{
public class BoxController : MonoBehaviour {

private static bool box1Right; 
private static bool box2Right; 
private static bool box1Left; 
private static bool box2Left; 
private static bool lockMove; 

private static bool badMove; 

private const string RIGHT_HAND_TAG = "RightHand"; 
private const string LEFT_HAND_TAG = "LeftHand"; 
private const string SCREEN = "Screen"; 

private const string BOX1_LEFT_NAME = "box1"; 
private const string BOX2_LEFT_NAME = "box2"; 
private const string BOX1_RIGHT_NAME = "box3"; 
private const string BOX2_RIGHT_NAME = "box4"; 

public Material materialGestosAvanzada; 



public void Start() {
    box1Right = false; 
    box2Right = false; 
    box1Left = false; 
    box2Left = false; 
    badMove = false; 
    lockMove = false; 
}

public void Update() {
    StartCoroutine(checkBothHandsMoveBox1()); 
    StartCoroutine(checkBothHandsMoveBox2()); 
    checkCorrectMove(); 


}

IEnumerator checkBothHandsMoveBox1() {
    if((box1Left == true || box1Right == true) && 
        !(box1Left == true && box1Right == true)) {
        // En el caso en que se ha tocado con una mano la primera caja
        // entonces tienes un segundo y medio para tocar la otra caja. 
        yield return new WaitForSecondsRealtime(1.5f); 
        if(!(box1Left == true && box1Right == true)) {
            badMove = true; 
        }
    }
}

IEnumerator checkBothHandsMoveBox2() {
    if((box2Left == true || box2Right == true) && 
        !(box2Left == true && box2Right == true)) {
            yield return new WaitForSecondsRealtime(1.5f); 
            if(!(box2Left == true && box2Right == true)) {
                badMove = true;
            }
        }
}

void checkCorrectMove() {
    if(box1Left == true && box1Right == true && 
        box2Left == true && box2Right == true) {
            handlerGoodMove();
        }
}

private void handlerGoodMove() {
    if(!badMove && !lockMove) {
        lockMove = true; 
        //Debug.Log("Buen movimiento"); 
        unableBoxes(); 
        changeScreenMaterial(); 
        CharacterController.StartSong(); 
    }
}

private void changeScreenMaterial() {
    GameObject.Find(SCREEN).GetComponent<Renderer>().material = materialGestosAvanzada; 
}

private void unableBoxes() {
    GameObject.Find(BOX1_LEFT_NAME).GetComponent<MeshRenderer>().enabled = false; 
    GameObject.Find(BOX1_RIGHT_NAME).GetComponent<MeshRenderer>().enabled = false;
    GameObject.Find(BOX2_LEFT_NAME).GetComponent<MeshRenderer>().enabled = false;
    GameObject.Find(BOX2_RIGHT_NAME).GetComponent<MeshRenderer>().enabled = false;
}


 private void OnTriggerEnter(Collider other) {
    if(!lockMove) {
        if(this.tag == RIGHT_HAND_TAG && other.tag == RIGHT_HAND_TAG) {
            handlerBoxes(other); 
        }
        else if(this.tag == LEFT_HAND_TAG && other.tag == LEFT_HAND_TAG) {
            handlerBoxes(other); 
        }
        else{
            Debug.Log("Error de tags"); 
        }
    }
}

private void handlerBoxes(Collider other) {
    switch(this.name) {
        case BOX1_LEFT_NAME: 
            box1Handler();  
            break; 
        case BOX1_RIGHT_NAME: 
            box1Handler(); 
            break; 
        case BOX2_LEFT_NAME: 
            box2Handler(); 
            break; 
        case BOX2_RIGHT_NAME: 
            box2Handler(); 
            break; 
    }
}

private void box1Handler() {
    
    if(badMove) {
        resetBadMove(); 
    }
    badMove = false; 
    if(this.name == BOX1_LEFT_NAME) {
        box1Left = true; 
    }
    else if(this.name == BOX1_RIGHT_NAME) {
        box1Right = true; 
    }
}

private void resetBadMove() {
    badMove = false; 
    box1Left = false;
    box2Left = false; 
    box1Right = false; 
    box2Right = false; 
}

private void box2Handler() {
    if(!badMove) {
        if(box1Left == true && box1Right == true) {
            if(this.name == BOX2_LEFT_NAME) {
                box2Left = true; 
            }
            else if(this.name == BOX2_RIGHT_NAME) {
                box2Right = true; 
            }

        }

    }
}





}



}