using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame(){
        SceneManager.LoadScene("1");
    }
    public void quitGame(){
        Application.Quit();
    }
}
