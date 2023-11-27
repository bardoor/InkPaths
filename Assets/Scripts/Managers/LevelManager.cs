using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class LevelManager : ScriptableObject
{
    [SerializeField]
    private GameObject _currentLevel;
    
    public void StartLevel(int number)
    {
        EndLevel();
        _currentLevel = ResourceManager.LoadLevel(number);
        if (_currentLevel == null)
        {
            Debug.Log($"Can't load Level_{number}! It was not found!!!");
        }
        Instantiate(_currentLevel, Vector3.zero, Quaternion.identity);
    }

    private void EndLevel()
    {
        Destroy(_currentLevel);
        _currentLevel = null;
    }

}
