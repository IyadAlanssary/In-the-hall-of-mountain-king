using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("11");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
