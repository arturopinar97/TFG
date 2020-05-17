/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Gesture : MonoBehaviour {

    // cuadrante donde se encuentra cada caja: 
    public enum PositionSquare{
        UP, 
        DOWN, 
        LEFT, 
        RIGHT,
        LEFT_UP, 
        LEFT_DOWN, 
        RIGHT_UP, 
        RIGHT_DOWN,
        CENTER

    }

    // Tipos de gestos: 
    public enum Gestures {
        DOS_POR_CUATRO,
        TRES_POR_CUATRO, 
        CUATRO_POR_CUATRO
    }
    // atributos de gesto: 
    private float time; 
    ArrayList<GestureState> states; 

    public Gesture(Gestures gestureName){
        initGestureStates(gestureName); 
    }

    private void initGestureStates(Gestures gestureName) {
        switch(gestureName) {
            case Gestures.DOS_POR_CUATRO: 
                initStatesTwoFourGesture(); 
        }
    }

    private void initStatesTwoFourGesture() {
        // 3 estados: 
        int i = 0; 
        numberStates = 3; 
        float stateTime = this.time / numberStates; 
        ArrayList<PositionSquare> squares = new ArrayList<>(); 
        squares.Add(PositionSquare.LEFT_DOWN); 
        squares.Add(PositionSquare.RIGHT); 
        squares.Add(PositionSquare.LEFT_UP); 

        foreach( object obj in squares) {
            GestureState state = new GestureState(stateTime, obj); 
        }
        
    }

    







}*/