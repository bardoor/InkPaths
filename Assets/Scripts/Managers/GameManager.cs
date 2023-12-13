using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IObserver
{
    private static GameManager instance;
    private static AudioManager _audioManager;
    private static LevelManager _levelManager;
    private static GUIManager _guiManager;
    private static TouchManager _touchManager;
    private static readonly int _lastUncompletedLevel = 1;

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
        SceneManager.sceneLoaded += OnSceneChanged;
        InitLevelManager();
        InitGuiManager();
        InitTouchManager();
        PathBuilder.Instance.AddObserver(this);
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
        _guiManager = ScriptableObject.CreateInstance<GUIManager>();
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
            StartLevel(_lastUncompletedLevel);
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

    public void ProcessEvent(IEvent e)
    {
        if (e is CancelledBuildingPath || e is FinishedBuildingPath)
        {
            _touchManager.StopTouching();
        }
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        InitGuiManager();
    }
}