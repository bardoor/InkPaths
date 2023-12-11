using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static AudioManager _audioManager;
    private static LevelManager _levelManager;
    private static GUIManager _guiManager;
    private static TouchManager _touchManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitLevelManager();
        InitGuiManager();
        InitTouchManager();
    }

    private void StartLevel(int index)
    {
        _levelManager.StartLevel(index);
        InitAudioManager();
    }

    private void InitAudioManager()
    {
        _audioManager = gameObject.AddComponent<AudioManager>();
        PathElement[] auditionElements = FindObjectsByType<PathElement>(FindObjectsSortMode.None);
        _audioManager.Init(auditionElements);
    }

    private void InitLevelManager()
    {
        _levelManager = gameObject.AddComponent<LevelManager>();
    }

    private void InitGuiManager()
    {
        _guiManager = new GUIManager();
        GUIManager.OnButtonClick += HandleButtonClick;
    }

    private void InitTouchManager()
    {
        _touchManager = gameObject.AddComponent<TouchManager>();
    }

    private void HandleButtonClick(string buttonName)
    {
        if (buttonName == "StartButton")
        {
            StartLevel(1);
        }
        else if (buttonName == "LevelsButton")
        {
            SceneManager.LoadScene("Levels");
        }
        else if (buttonName == "ExitButton")
        {
            Application.Quit();
        }
    }
}