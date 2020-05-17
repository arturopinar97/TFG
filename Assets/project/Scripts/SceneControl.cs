/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SceneControl : MonoBehaviour {

    // Lista de escenas de ChoVR: 
        // escena avanzado genera los gestos a partir de la lectura de MIDI. 
    public enum Scenes {
        TUTORIAL_2_4, 
        TUTORIAL_3_4, 
        TUTORIAL_4_4, 
        AVANZADO
    }



    private Scenes sceneName; 
    private ArrayList gestureNames; 



    public SceneControl(){

        // 1. Inicializa el nombre de la escena: 
        setSceneName(); 
        // 2. Genera los gestos en funcion de la escena cargada: 
        generateGesturesNames(); 



    }


    // Genera la lista de todos los gestos de la escena. 
    public void generateGesturesNames(){
        switch(this.sceneName){
            case Scenes.TUTORIAL_2_4: 
            
        }
    }

    public void setSceneName(){
        // Pregunta a Unity el nombre de la escena en la que se encuentra. 
        // ---------- TODO -----------------------
        // Usa la escena 2/4 pero cuando tengamos el menu principal usamos 
        //https://informaticaincomprendida.wordpress.com/2015/01/25/como-saber-que-nivel-se-ha-cargado-en-unity/
        string sceneName = "Tutorial 2-4"; 

        // Asigna la posicion determinada en el enum de Scenes: 
        switch(sceneName){
            case "Tutorial 2-4": 
                sceneName = Scenes.TUTORIAL_2_4; 
                break; 
            case "Tutorial 3-4": 
                sceneName = Scenes.TUTORIAL_3_4; 
                break; 
            case "Tutorial 4-4": 
                sceneName = Scenes.TUTORIAL_4_4; 
                break;
            case "Avanzado": 
                sceneName = Scenes.AVANZADO; 
                break; 
        }
    }




    // GETTERS AND SETTERS: 

    public string getSceneName(){
        return this.sceneName; 
    }








}*/