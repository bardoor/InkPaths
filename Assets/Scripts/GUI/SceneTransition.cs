using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void SelectScene(int numberScene)
    {
        SceneManager.LoadSceneAsync(numberScene);
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
