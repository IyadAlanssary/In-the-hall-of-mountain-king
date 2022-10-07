using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    float previousTimeScale;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                AudioListener.pause = false;
                Time.timeScale = previousTimeScale;
            }
            else
            {
                previousTimeScale = Time.timeScale;
                AudioListener.pause = true;
                Time.timeScale = 0;
            }
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReloadGame()
    {
        SceneManager.LoadScene("1");
    }
}
