using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Controller : MonoBehaviour
{
    public void RestartGame(){
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Quit_My_App(){
        Application.Quit();
    }

    private void Awake() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Playy_Game(){
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void how_to_play(){
        SceneManager.LoadScene(3);
    }
    public void Back_to_start(){
        SceneManager.LoadScene(0);
    }
}
