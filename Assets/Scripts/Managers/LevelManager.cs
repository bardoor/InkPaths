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
    public static int levelNumber = 1;
    private float[] topMarginPercentages = new float[] { 5f, 25f };
    private float currentTopMargin = 5f;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public IEnumerator StartLevel(int number)
    {
        if (number > ResourceManager.LevelCount()) 
            number = ResourceManager.LevelCount();

        Debug.Log($"Loading {number} level");

        _currentLevel = ResourceManager.LoadLevel(number);
        Debug.Log($"{_currentLevel} is cur level");
        levelNumber = number;
        currentTopMargin = topMarginPercentages[levelNumber - 1];

        if (_currentLevel == null)
        {
            Debug.Log($"Can't load Level_{number}! It was not found!!!");
        }

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainGameProcess"))
        {
            yield return SceneManager.LoadSceneAsync("MainGameProcess");
        }

        yield return null;
    }

    public void NextLevel()
    {
        StartCoroutine(StartLevel(levelNumber + 1));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGameProcess" && _currentLevel != null)
        {
            GameObject[] objs = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            GameObject canvasObject = null;

            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].name == "Canvas")
                {
                    canvasObject = objs[i];
                }
            }
            
            if (canvasObject != null)
            {
                float offsetY = Screen.height * (currentTopMargin / 100f);
                GameObject level = Instantiate(_currentLevel, Vector3.zero, Quaternion.identity, canvasObject.transform);
                RectTransform levelRectTransform = level.GetComponent<RectTransform>();
                levelRectTransform.anchoredPosition = new Vector2(0f, -offsetY);
            }
            else
            {
                Debug.LogError($"Objs found: {objs.Length}. Scene is {scene.name}. Canvas object not found in the scene. Make sure it's named 'Canvas'.");
            }
            OnLevelStarted?.Invoke(levelNumber);
        }
        else
        {
            PathBuilder.Instance.Clear();
            PathBuilder.Instance.ClearCompletePaths();
        }
    }
}
