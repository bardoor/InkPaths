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
        // ������������� �� ������� ��������� ����� ������, ���� ��� �� ���������
        if (!isSubscribed)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            isSubscribed = true;
        }

        // �������� ����� ��� ������ ������ �� ������� �����
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
            // ������� ������ ����������, ����� �������� ���������� ��� ������ ����� �����
            btn.onClick.RemoveAllListeners();
            // ��������� ����� ����������
            btn.onClick.AddListener(() => HandleButtonClick(buttonName));
        }
    }

    private void HandleButtonClick(string buttonName)
    {
        OnButtonClick?.Invoke(buttonName);
    }
}
