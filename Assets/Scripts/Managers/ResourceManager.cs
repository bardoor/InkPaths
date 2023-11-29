using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json.Linq;
using System.IO;

public static class ResourceManager
{
    private static readonly string _levelsPath = "Assets/Prefabs/Levels";
    private static readonly string _audioConfigPath = "Assets/Config/AudioConfig.json";
    private static readonly JObject _audioConfig = GetAudioConfigJson();

    static ResourceManager()
    {

    }

    public static GameObject LoadLevel(int index)
    {
        return AssetDatabase.LoadAssetAtPath<GameObject>($"{_levelsPath}/Level_{index}.prefab");
    }

    private static JObject GetAudioConfigJson()
    {
        string jsonText = File.ReadAllText(_audioConfigPath);
        return JObject.Parse(jsonText);
    }

    public static AudioClip LoadSound(string elementName, string stateName, string moment)
    {
        AudioClip clip = Resources.Load<AudioClip>(_audioConfig[elementName][stateName][moment].ToString());
        if (clip == null)
        {
            Debug.LogError($"Can not load {elementName}/{stateName}/{moment}!");
        }
        return clip;
    }
}