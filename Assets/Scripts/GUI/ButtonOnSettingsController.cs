using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnSettingsController : MonoBehaviour
{
    private float _sliderValue = 100;
    public Slider slider;

    public void SliderOff()
    {
        this._sliderValue = slider.value;
        slider.value = 0;
    }

    public void SliderOn()
    {
        slider.value = _sliderValue;
    }
}
