using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderOnSettingsController : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public GameObject buttonMax;
    public GameObject buttonMedium;
    public GameObject buttonOff;

    private float _sliderValue;

    public void SliderChange(float value)
    {
        gameObject.GetComponent<Slider>().minValue = 0f;
        gameObject.GetComponent<Slider>().maxValue = 100f;
        gameObject.GetComponent<Slider>().wholeNumbers = true;

        valueText.text = value.ToString() + " %";
        this._sliderValue = value;

        ActiveButtons();
    }

    private void ActiveButtons()
    {
        if(this._sliderValue >= 50f)
        {
            buttonMax.SetActive(true);
            buttonMedium.SetActive(false);
            buttonOff.SetActive(false);
        }
        else if(this._sliderValue > 0)
        {
            buttonMax.SetActive(false);
            buttonMedium.SetActive(true);
            buttonOff.SetActive(false);
        }
        else
        {
            buttonMax.SetActive(false);
            buttonMedium.SetActive(false);
            buttonOff.SetActive(true);
        }
    }

    public float getSliderValue()
    {
        return  _sliderValue;
    }
}
