using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private readonly string MainGameSceneName = "MainGameProcess";
    public void FastPlay()
    {
        SceneManager.LoadScene(MainGameSceneName);
    }
    public void SelectLevels()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
