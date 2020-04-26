using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Loads scene 1 with the actual game
    }

    public void QuitGame()
    {
        Application.Quit(); // Quits the application
        Debug.Log("Quit");
    }
}
