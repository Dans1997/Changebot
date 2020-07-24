using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadScene() 
    {
        Debug.Log("Reloading Scene");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextScene()
    {
        Debug.Log("Loading Next Scene");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // Game Over Scene Will Always Be 1
    public void LoadTitleScreen()
    {
        Debug.Log("Loading Title Screen");
        SceneManager.LoadScene(1);
    }

    // Game Over Scene Will Always Be The Last One
    public void LoadGameOverScreen()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void ExitGame()
    {
        Debug.Log("Quitting Application.");
        Application.Quit();
    }

}
