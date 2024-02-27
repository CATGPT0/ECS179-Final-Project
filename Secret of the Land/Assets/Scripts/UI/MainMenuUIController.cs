using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("World");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
}
