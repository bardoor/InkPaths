using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerPanelController : MonoBehaviour
{
    public GameObject buttonPause;
    public GameObject buttonSettings;
    public GameObject buttonQuestion;
    public GameObject pauseBlurPanel;
    public Image star_1;
    public Image star_2;
    public Image star_3;
    public Slider timer;

    private void OnEnable()
    {
        if (timer.value / timer.maxValue < 0.25f)
        {
            star_3.gameObject.SetActive(false);
            star_2.gameObject.SetActive(false);
            star_1.gameObject.SetActive(false);
        }
        else if (timer.value / timer.maxValue < 0.5f)
        {
            star_3.gameObject.SetActive(false);
            star_2.gameObject.SetActive(false);
        }
        else if (timer.value / timer.maxValue < 0.75f)
        {
            star_3.gameObject.SetActive(false);
        }
    }
}
