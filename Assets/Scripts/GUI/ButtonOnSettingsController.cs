using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnSettingsController : MonoBehaviour
{
    public Slider slider;

    private float _sliderValue;

    private void Start()
    {
        _sliderValue = slider.value;
    }

    public void SoundOff()
    {
        this._sliderValue = slider.value;
        slider.value = 0f;
    }

    public void SoundOn()
    {
        slider.value = _sliderValue;
    }
}
