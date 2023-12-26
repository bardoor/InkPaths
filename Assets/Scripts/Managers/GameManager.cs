using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IObserver
{
    private static GameManager _instance;
    private AudioManager _audioManager;
    private LevelManager _levelManager;
    private GUIManager _guiManager;
    private TouchManager _touchManager;
    private DBManager _dbManager;
    
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (!NeedBootstraping()) return;

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private bool NeedBootstraping()
        => _instance == null ? true : false;

    private void Start()
    {
        InitManagers();
        PathBuilder.Instance.AddObserver(this);
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    public IEnumerator StartLevel(int levelIndex)
    {
        yield return _levelManager.StartLevel(levelIndex);
    }

    private void OnLevelStarted(int levelIndex)
    {
        _audioManager = gameObject.AddComponent<AudioManager>();
        PathElement[] auditionElements = FindObjectsByType<PathElement>(FindObjectsSortMode.None);
        _audioManager.Init(auditionElements);
    }

    public void InitManagers()
    {
        _levelManager = gameObject.AddComponent<LevelManager>();
        _guiManager = gameObject.AddComponent<GUIManager>();
        _touchManager = gameObject.AddComponent<TouchManager>();
        _dbManager = gameObject.AddComponent<DBManager>();

        GUIManager.OnButtonClick += HandleButtonClick;
        LevelManager.OnLevelStarted += OnLevelStarted;
    }

    private void HandleButtonClick(string buttonName)
    {
        Debug.Log(buttonName);
        if (buttonName == "StartButton")
        {
            StartLevel(LevelManager.levelNumber);
        }
        else if (buttonName == "LevelsButton")
        {
            SceneManager.LoadScene("Levels");
        }
        else if (buttonName == "ExitButton")
        {
            Application.Quit();
        }
        else if (buttonName.StartsWith("Level "))
        {
            int index = buttonName[buttonName.Length - 1] - '0';
            if (!(index == 1 || index == 2)) return;
            StartCoroutine(StartLevel(index));
        }
    }

    private void Update()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "PauseBlurPanel")
            {
                if (!obj.activeInHierarchy)
                {
                    _touchManager.UnblockTouches();
                }
                else
                {
                    _touchManager.BlockTouches();
                }
            }
        }
    }

    public void ProcessEvent(IEvent e)
    {
        if (e is CancelledBuildingPath || e is FinishedBuildingPath)
        {
            _touchManager.StopTouching();
        }

        if (e is FinishedBuildingPath finishEvent)
        {
            if (finishEvent.PathsCount == FindObjectsByType<InkBlob>(FindObjectsSortMode.None).Length / 2)
            {
                GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
                foreach (GameObject obj in allObjects)
                {
                    if (obj.name == "WinnerPanel" && !obj.activeInHierarchy)
                    {
                        obj.SetActive(true);
                        PathBuilder.Instance.ClearCompletePaths();
                        StopTouching();
                    }
                }
            }

        }
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene {scene.name}");
        if (_guiManager == null)
            _guiManager = gameObject.AddComponent<GUIManager>();
        _guiManager = gameObject.GetComponent<GUIManager>();   
        GUIManager.OnButtonClick += HandleButtonClick;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneChanged;

        // Отписываемся от события начала уровня
        LevelManager.OnLevelStarted -= OnLevelStarted;
    }

    public void StopTouching() => _touchManager.StopTouching();

    public void StartTouching() => _touchManager.StartTouching();
}