using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static AudioManager _audioManager;
    private static LevelManager _levelManager;
    private static CameraController _mainCamera;
    private static GUIManager _guiManager;

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
        //InitCamera();
        InitLevelManager();
        InitGuiManager();
    }

    private void StartLevel(int index)
    {
        //InitCamera();
        //_levelManager.StartLevel(index);
        //_mainCamera.FrameLevel();
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

    private void InitCamera()
    {
        _mainCamera = FindAnyObjectByType<CameraController>();
        _mainCamera.Init();
    }

    private void InitGuiManager()
    {
        _guiManager = new GUIManager();
        GUIManager.OnButtonClick += HandleButtonClick;
    }

    // Метод, который будет вызываться при нажатии кнопок
    private void HandleButtonClick(string buttonName)
    {
        if (buttonName == "StartButton")
        {
            StartLevel(1);
        }
    }


}