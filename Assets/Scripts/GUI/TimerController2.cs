using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController2 : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float maxValueTimer = 100f;
    void Start()
    {
        gameObject.GetComponent<Slider>().minValue = 0f;
        gameObject.GetComponent<Slider>().maxValue = maxValueTimer;
        gameObject.GetComponent<Slider>().value = maxValueTimer;
    }

    void FixedUpdate()
    {
        if(gameObject.GetComponent<Slider>().value > 0f)
        {
            float timerValue = Mathf.Ceil(gameObject.GetComponent<Slider>().value);

            if (gameObject.GetComponent<Slider>().value > maxValueTimer * 0.2f)
                text.text = timerValue.ToString();
            else
            {
                Color textColor = new Color(249f / 255f, 100f / 255f, 111f / 255f, 1f);
                text.color = textColor;
                text.text = timerValue.ToString();
            }

            gameObject.GetComponent<Slider>().value -= Time.deltaTime;

        }
        else
        {
            Debug.LogAssertion("Timer's expired");
            gameObject.SetActive(false);
        }
        
    }
}
