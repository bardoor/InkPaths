using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : ScriptableObject 
{
    public delegate void ButtonClickEventHandler(string buttonName);
    public static event ButtonClickEventHandler OnButtonClick;

    private void Awake()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button button in buttons)
        {
            string buttonName = button.gameObject.name; 
            button.onClick.AddListener(() => HandleButtonClick(buttonName));
        }
    }

    private void HandleButtonClick(string buttonName)
    {
        OnButtonClick?.Invoke(buttonName);
    }
}
