using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class AudioManager : MonoBehaviour, IStateChangeListener
{
    private PathElement[] _auditionElements;
    private readonly string _pathToConfig = "Assets/Config/AudioConfig.json";
    private JObject _config;
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    public void Init(PathElement[] auditionElements)
    {
        SubscribeToElements(auditionElements);
        _config = GetAudioConfigJson(_pathToConfig);
    }

    private void SubscribeToElements(PathElement[] auditionElements)
    {
        foreach (PathElement element in auditionElements)
        {
            element.Subscribe(this);
        }
        _auditionElements = auditionElements;

        _config = GetAudioConfigJson(_pathToConfig);
    }
    private JObject GetAudioConfigJson(string pathToJson)
    {
        if (File.Exists(pathToJson))
        {
            string jsonText = File.ReadAllText(pathToJson);
            return JObject.Parse(jsonText);
        }
        Debug.LogError($"File not found: {pathToJson}");
        return null;
    }

    public void OnStateEnter(IStateChangeObservable element, State newState)
    {
        string moment = "OnEnter";
        string elementName = element.GetType().Name;
        string stateName = newState.GetType().Name;
        try
        {
            audioClip = Resources.Load<AudioClip>(_config[elementName][stateName][moment].ToString());
            Debug.Log(audioClip);
            audioSource.clip = audioClip;
            audioSource.Play();
            Debug.Log($"Playing {_config[elementName][stateName][moment]}");
        }
        catch (System.Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
        }
    }
}
