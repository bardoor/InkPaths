using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBlurPanelController : MonoBehaviour
{
    private GameObject mainGamePanel;
    
    void Start()
    {
        mainGamePanel = GameObject.Find("Level_1(Clone)");
        if (mainGamePanel != null)
        {
            mainGamePanel.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        }
        else
        {
            Debug.LogError("mainGamePanel is NULL");
        }
    }
}
