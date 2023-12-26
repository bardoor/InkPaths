using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class NewTestScriptTimerController
{
    [UnityTest]
    public IEnumerator TimerStart()
    {
        // Arrange
        GameObject timer = new GameObject();
        timer.AddComponent<Slider>();
        timer.GetComponent<Slider>().minValue = 0;
        timer.GetComponent<Slider>().maxValue = 60;
        timer.GetComponent<Slider>().value = 60;

        TimerController2 timerController = timer.AddComponent<TimerController2>();
        GameObject blurPanel = new GameObject();
        blurPanel.SetActive(false);
        GameObject lossPanel = new GameObject();
        lossPanel.SetActive(false);

        timerController.slider = timer;
        timerController.text = new GameObject().AddComponent<TextMeshProUGUI>();
        timerController.maxValueTimer = 60f;
        timerController.pauseGameBlurPanel = blurPanel;
        timerController.lossPanel = lossPanel;

        // Act
        yield return null;

        // Assert
        Assert.AreEqual(60, timer.GetComponent<Slider>().value);
        Assert.IsFalse(blurPanel.activeSelf);
        Assert.IsFalse(lossPanel.activeSelf);
    }
}
