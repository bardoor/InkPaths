using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static AudioManager _audioManager;
    private static LevelManager _levelManager;
    private static MainCamera _mainCamera;

    private void Awake()
    {
    }

    private void Start()
    {
        InitCamera();
        InitLevelManager();
        _levelManager.StartLevel(1);
        _mainCamera.FrameLevel();
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
        _levelManager = ScriptableObject.CreateInstance<LevelManager>();
    }

    private void InitCamera()
    {
        _mainCamera = FindAnyObjectByType<MainCamera>();
    }

}
