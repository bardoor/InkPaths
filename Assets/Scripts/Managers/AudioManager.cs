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


    public void Awake()
    {
    }

    public void Init(PathElement[] auditionElements)
    {
       
    }

    public void OnStateEnter(IStateChangeObservable element, State newState)
    {
        
    }

}
