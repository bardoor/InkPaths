using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

public static class ResourceManager
{
    private static readonly string _levelsPath = "Assets/Prefabs/Levels";
    private static readonly string _audioConfigPath = "Assets/Config/AudioConfig.json";
    private static readonly JObject _audioConfig = GetAudioConfigJson();

    public static GameObject LoadLevel(int index)
    {
        return AssetDatabase.LoadAssetAtPath<GameObject>($"{_levelsPath}/Level_{index}.prefab");
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

    public static AudioClip LoadSound(string elementName, string stateName, string moment)
    {
        if (_audioConfig == null || _audioConfig[elementName] == null || _audioConfig[elementName][stateName] == null || _audioConfig[elementName][stateName][moment] == null)
        {
            return null;
        }

        string audioPath = _audioConfig[elementName][stateName][moment].ToString();
        if (string.IsNullOrEmpty(audioPath))
        {
            return null;
        }

        AudioClip clip = Resources.Load<AudioClip>(audioPath);
        if (clip == null)
        {
            Debug.LogError($"Failed to load audio clip: {elementName}/{stateName}/{moment}");
        }
        return Resources.Load<AudioClip>("Audio\\Music\\The Lost Song");
    }
}