using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;


public class AudioManager : MonoBehaviour, IStateChangeListener
{
    [SerializeField] private AudioClip _currentSound;
    [SerializeField] private AudioClip _currentMusic;
    public List<PathElement> _auditionElements = new List<PathElement>();
    private AudioSource _musicSource;
    private AudioSource _soundSource;

    private static readonly string _audioConfigPath = "Assets\\Config\\AudioConfig.json";
    private static readonly JObject _audioConfig = GetAudioConfigJson();
    

    public void Awake()
    {
        _musicSource = gameObject.AddComponent<AudioSource>();
        //_musicSource.clip = _currentMusic;
        _soundSource = gameObject.AddComponent<AudioSource>();
        //_soundSource.clip = _currentSound;
    }

    public void Init(PathElement[] auditionElements)
    {
        SubscribeToElements(auditionElements);
    }

    private void SubscribeToElements(PathElement[] auditionElements)
    {
        Debug.LogAssertion($"{auditionElements.Length}");
        foreach(var el in auditionElements)
        {
            Debug.Log($"{el.gameObject.name}");
        }

        foreach (PathElement element in auditionElements)
        {
            var el = element.GetComponent<PathElement>();
            el.Subscribe(this);
            _auditionElements.Add(el);
        }
    }

    private static JObject GetAudioConfigJson()
    {
        string jsonText = File.ReadAllText(_audioConfigPath);
        return JObject.Parse(jsonText);
    }

    public static string GetPathTo(string elementName, string stateName, string moment)
    {
        if (_audioConfig == null || _audioConfig[elementName] == null || _audioConfig[elementName][stateName] == null || _audioConfig[elementName][stateName][moment] == null)
        {
            return null;
        }

        //return JsonConvert.SerializeObject(_audioConfig[elementName][stateName][moment]);
        return _audioConfig[elementName][stateName][moment].ToString().Trim();
    }

    public void OnStateEnter(IStateChangeObservable element, State newState)
    {
        string moment = "OnEnter";
        string elementName = element.GetType().Name;
        string stateName = newState.GetType().Name;

        string audioPath = GetPathTo(elementName, stateName, moment);
        if (audioPath != null)
        {
            _soundSource.clip = Resources.Load<AudioClip>(audioPath);
            if (_soundSource.clip != null)
            {
                Debug.LogAssertion($"~~~Audio Manager~~~~\nHell yeah {elementName} changed to {stateName} I will play {_soundSource.clip.name}");

                PlaySound();
            }
        }
    }

    private void PlaySound()
    {
        if (!_soundSource.isPlaying)
        {
            _soundSource.Play();
        }
    }

    private void PlayMusic()
    {
        if (!_musicSource.isPlaying)
        {
            _musicSource.Play();
        }
    }
}
