using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IObserver
{
    private static GameManager instance;
    private static AudioManager _audioManager;
    private static LevelManager _levelManager;
    private static GUIManager _guiManager;
    private static TouchManager _touchManager;
    private DBManager _dbManager;

    private void Awake()
    {
        if (!NeedBootstraping()) return;

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private bool NeedBootstraping()
        => instance == null ? true : false;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneChanged;
        InitLevelManager();
        InitGuiManager();
        InitTouchManager();
        InitDBManager();
        PathBuilder.Instance.AddObserver(this);

        LevelManager.OnLevelStarted += OnLevelStarted;
    }

    private void StartLevel(int levelIndex)
    {
        _levelManager.StartLevel(levelIndex);
    }

    private void OnLevelStarted(int levelIndex)
    {
        InitAudioManager();
    }

    private void InitAudioManager()
    {
        _audioManager = gameObject.AddComponent<AudioManager>();
        PathElement[] auditionElements = FindObjectsByType<PathElement>(FindObjectsSortMode.None);
        _audioManager.Init(auditionElements);
    }

    private void InitLevelManager()
    {
        _levelManager = gameObject.AddComponent<LevelManager>();
    }

    private void InitGuiManager()
    {
        _guiManager = gameObject.AddComponent<GUIManager>();
        GUIManager.OnButtonClick += HandleButtonClick;
    }

    private void InitTouchManager()
    {
        _touchManager = gameObject.AddComponent<TouchManager>();
    }

    private void InitDBManager()
    {
        _dbManager = gameObject.AddComponent<DBManager>();
    }

    private void HandleButtonClick(string buttonName)
    {
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
            StartLevel(index);
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
        InitGuiManager();
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