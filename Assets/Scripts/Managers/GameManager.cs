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
    private DBManager _dbManager;
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
        InitDBManager();
        PathBuilder.Instance.AddObserver(this);

        LevelManager.OnLevelStarted += OnLevelStarted;
    }

    private void StartLevel(int levelIndex)
    {
        _levelManager.StartLevel(levelIndex);
    }

    private void OnLevelStarted(int levelIndex)
    {
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

    private void InitDBManager()
    {
        _dbManager = gameObject.AddComponent<DBManager>();
    }

    private void HandleButtonClick(string buttonName)
    {
        if (buttonName == "StartButton")
        {
            StartLevel(LevelManager.levelNumber);
        }
        else if (buttonName == "LevelsButton")
        {
            SceneManager.LoadScene("Levels");
        }
        else if (buttonName == "ExitButton")
        {
            Application.Quit();
        }
        else if (buttonName.StartsWith("Level "))
        {
            StartLevel((buttonName[buttonName.Length - 1] - '0'));
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
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneChanged;

        // Отписываемся от события начала уровня
        LevelManager.OnLevelStarted -= OnLevelStarted;
    }
}