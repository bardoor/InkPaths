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
    private static JObject _audioConfig;

    static ResourceManager()
    {
        GetAudioConfigJson();
    }

    public static GameObject LoadLevel(int index)
    {
        return AssetDatabase.LoadAssetAtPath<GameObject>($"{_levelsPath}/Level_{index}.prefab");
    }

    private static void GetAudioConfigJson()
    {
        if (File.Exists(_audioConfigPath))
        {
            string jsonText = File.ReadAllText(_audioConfigPath);
            _audioConfig = JObject.Parse(jsonText);
        }
        Debug.LogError($"File not found: {_audioConfigPath}");
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
