using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FailedManeger : MonoBehaviour
{
    public string PassedsceneName;

    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadPassedSceneByName(string sceneName)
    {
        SceneManager.LoadScene(PassedsceneName);
    }

    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(currentScene.buildIndex); 
    }
    public void ExitSystem()
    {
        Application.Quit();
    }

    public void Close()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
