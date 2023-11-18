using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float maxValueTimer;

    void Start()
    {
        StartCoroutine(TimerControl());
    }

    public void StartTimer()
    {
        TimerControl();
    }

    public IEnumerator TimerControl()
    {
        gameObject.GetComponent<Slider>().maxValue = maxValueTimer;
        gameObject.GetComponent<Slider>().value = maxValueTimer;
        text.text = gameObject.GetComponent<Slider>().value.ToString();

        while (gameObject.GetComponent<Slider>().value >= 0f)
        {
            gameObject.GetComponent<Slider>().value -= 1f;

            text.text = gameObject.GetComponent<Slider>().value.ToString();

            yield return new WaitForSeconds(1f);
        }
    }
}
