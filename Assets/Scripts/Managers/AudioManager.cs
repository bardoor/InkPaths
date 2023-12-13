using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class AudioManager : MonoBehaviour, IStateChangeListener
{
    [SerializeField] private AudioClip _currentSound;
    [SerializeField] private AudioClip _currentMusic;
    public List<PathElement> _auditionElements = new List<PathElement>();
    private AudioSource _musicSource;
    private AudioSource _soundSource;

    public void Start()
    {
        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.clip = _currentMusic;
        _soundSource = gameObject.AddComponent<AudioSource>();
        _soundSource.clip = _currentSound;
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

    public void OnStateEnter(IStateChangeObservable element, State newState)
    {
        string moment = "OnEnter";
        string elementName = element.GetType().Name;
        string stateName = newState.GetType().Name;

        _soundSource.clip = ResourceManager.LoadSound(elementName, stateName, moment);
        if (_soundSource.clip != null)
        {
            Debug.LogAssertion($"~~~Audio Manager~~~~\nHell yeah {elementName} changed to {stateName} I will play {_soundSource.clip.name}");
            PlaySound();
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
