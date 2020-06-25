using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.SceneManagement; 

namespace ChoVR_Core {
public class MenuController : MonoBehaviour {

private const string CHOOSE_TEMPO_SCENE = "TEMPOChooseScene"; 
private const string ADVANCED_SCENE = "Avanzado(3-4)"; 
private const string TEMPO_2_4_SCENE = "Tutorial 2_4"; 
private const string TEMPO_3_4_SCENE = "Tutorial 3-4"; 
private const string TEMPO_4_4_SCENE = "Tutorial 4_4"; 
private const string MAIN_MENU_SCENE = "MainScene"; 
    public void gameTempo() {
        SceneManager.LoadScene(CHOOSE_TEMPO_SCENE); 
    }

    
    public void gameAdvanced() {
        SceneManager.LoadScene(ADVANCED_SCENE); 
    }

    
    public void quitGame() {
        Application.Quit(); 
        Debug.Log("Saliendo del juego"); 
    }

   
    public void tempo2_4() {
        SceneManager.LoadScene(TEMPO_2_4_SCENE); 
    }

    
    public void tempo3_4() {
        SceneManager.LoadScene(TEMPO_3_4_SCENE); 
    }

   
    public void tempo4_4() {
        SceneManager.LoadScene(TEMPO_4_4_SCENE); 
    }

    
    public void backMainScene() {
        SceneManager.LoadScene(MAIN_MENU_SCENE); 
    }






}




}