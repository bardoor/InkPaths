using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController2 : MonoBehaviour
{
    public GameObject slider;
    public TextMeshProUGUI text;
    public float maxValueTimer = 100f;
    public GameObject pauseGameBlurPanel;
    public GameObject lossPanel;

    void Start()
    {
        Debug.Log("Timer start");
        slider.GetComponent<Slider>().minValue = 0f;
        slider.GetComponent<Slider>().maxValue = maxValueTimer;
        slider.GetComponent<Slider>().value = maxValueTimer;
    }

    void FixedUpdate()
    {
        if (!pauseGameBlurPanel.activeSelf)
        {
            if (slider.GetComponent<Slider>().value > 0f)
            {
                float timerValue = Mathf.Ceil(slider.GetComponent<Slider>().value);

                if (slider.GetComponent<Slider>().value > maxValueTimer * 0.2f)
                    text.text = timerValue.ToString();
                else
                {
                    Color textColor = new Color(249f / 255f, 100f / 255f, 111f / 255f, 1f);
                    text.color = textColor;
                    text.text = timerValue.ToString();
                }

                slider.GetComponent<Slider>().value -= Time.deltaTime;
            }
            else
            {
                text.text = "0";
                Debug.Log("Timer's expired");
                OpenLossPanel();
                //gameObject.SetActive(false);
                PathBuilder.Instance.ClearCompletePaths();
                GameObject.FindFirstObjectByType<GameManager>().StopTouching();
            }
        }
    }

    private void OpenLossPanel()
    {
        lossPanel.gameObject.SetActive(true);
    }
}

