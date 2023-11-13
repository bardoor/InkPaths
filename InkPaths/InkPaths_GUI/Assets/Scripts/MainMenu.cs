using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void SelectLevels()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
