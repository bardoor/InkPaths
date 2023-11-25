using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderOnSettingsController : MonoBehaviour
{
    public Text valueText;
    public GameObject buttonOff;
    public GameObject buttonOn;

    public void SliderChange(float value)
    {
        valueText.text = value.ToString();
        valueText.text += " %";

        if(value > 0)
        {
            buttonOff.SetActive(false);
            buttonOn.SetActive(true);
        }
        else
        {
            buttonOff.SetActive(true);
            buttonOn.SetActive(false);
        }
    }
}
