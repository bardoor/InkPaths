using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public delegate void ButtonClickEventHandler(string buttonName);
    public static event ButtonClickEventHandler OnButtonClick;

    private void Awake()
    {
        // ������������� �� ������� ��������� �����
        SceneManager.sceneLoaded += OnSceneLoaded;

        // �������� ����� ��� ������ ������ �� ������� �����
        SetupButtonListeners();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button button in buttons)
        {
            string buttonName = button.gameObject.name;
            // ������� ������ ����������, ����� �������� ���������� ��� ������ ����� �����
            button.onClick.RemoveAllListeners();
            // ��������� ����� ����������
            button.onClick.AddListener(() => HandleButtonClick(buttonName));
        }
    }

    private void HandleButtonClick(string buttonName)
    {
        OnButtonClick?.Invoke(buttonName);
    }
}
