using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour 
{
    public static event Action<int> OnLevelStarted;

    private GameObject _currentLevel;
    private int _levelNumber;

    private float topMarginPercentage = 5f;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void StartLevel(int number)
    {
        _currentLevel = ResourceManager.LoadLevel(number);
        _levelNumber = number;
        if (_currentLevel == null)
        {
            Debug.Log($"Can't load Level_{number}! It was not found!!!");
        }
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainGameProcess"))
        {
            SceneManager.LoadScene("MainGameProcess");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGameProcess")
        {
            GameObject canvasObject = GameObject.Find("Canvas");

            if (canvasObject != null && _currentLevel != null)
            {
                float offsetY = Screen.height * (topMarginPercentage / 100f);
                GameObject level = Instantiate(_currentLevel, Vector3.zero, Quaternion.identity, canvasObject.transform);
                RectTransform levelRectTransform = level.GetComponent<RectTransform>();
                levelRectTransform.anchoredPosition = new Vector2(0f, -offsetY);
            }
            else
            {
                Debug.LogError("Canvas object not found in the scene. Make sure it's named 'Canvas'.");
            }
            OnLevelStarted?.Invoke(_levelNumber);
        }
    }
}
