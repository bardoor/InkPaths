using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public delegate void ButtonClickEventHandler(string buttonName);
    public static event ButtonClickEventHandler OnButtonClick;

    private bool isSubscribed = false;

    private void Awake()
    {
        // Подписываемся на событие изменения сцены только, если еще не подписаны
        if (!isSubscribed)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            isSubscribed = true;
        }

        // Вызываем метод для поиска кнопок на текущей сцене
        SetupButtonListeners();
    }

    private void Update()
    {
        SetupButtonListeners();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        Object[] buttons = Resources.FindObjectsOfTypeAll(typeof(Button));
        foreach (Object obj in buttons)
        {
            var btn = obj as Button;
            if (btn == null) 
                continue;
            
            string buttonName = btn.gameObject.name;
            // Удаляем старый обработчик, чтобы избежать накопления при каждой смене сцены
            btn.onClick.RemoveAllListeners();
            // Добавляем новый обработчик
            btn.onClick.AddListener(() => HandleButtonClick(buttonName));
        }
    }

    private void HandleButtonClick(string buttonName)
    {
        OnButtonClick?.Invoke(buttonName);
    }
}
