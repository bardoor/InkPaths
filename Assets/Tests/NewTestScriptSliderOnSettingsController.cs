using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using UnityEngine.UI;

public class NewTestScriptSliderOnSettingsController
{
    // ѕроверка на значение слайдера больше или равно 50%
    [Test]
    public void SliderChange_ActiveButtons_MaxButtonActive()
    {
        // Arrange
        GameObject sliderObject = new GameObject();
        sliderObject.AddComponent<Slider>();
        SliderOnSettingsController sliderController = sliderObject.AddComponent<SliderOnSettingsController>();
        sliderController.buttonMax = new GameObject();
        sliderController.buttonMedium = new GameObject();
        sliderController.buttonOff = new GameObject();
        sliderController.valueText = new GameObject().AddComponent<TextMeshProUGUI>();

        // Act
        sliderController.SliderChange(70f);

        // Assert
        Assert.IsTrue(sliderController.buttonMax.activeSelf);
        Assert.IsFalse(sliderController.buttonMedium.activeSelf);
        Assert.IsFalse(sliderController.buttonOff.activeSelf);
        Assert.AreEqual("70 %", sliderController.valueText.text);
        Assert.AreEqual(70f, sliderController.getSliderValue());
    }

    // ѕроверка на значение слайдера больше 0% и меньше 50%
    [Test]
    public void SliderChange_ActiveButtons_MediumButtonActive()
    {
        // Arrange
        GameObject sliderObject = new GameObject();
        sliderObject.AddComponent<Slider>();
        SliderOnSettingsController sliderController = sliderObject.AddComponent<SliderOnSettingsController>();
        sliderController.buttonMax = new GameObject();
        sliderController.buttonMedium = new GameObject();
        sliderController.buttonOff = new GameObject();
        sliderController.valueText = new GameObject().AddComponent<TextMeshProUGUI>();

        // Act
        sliderController.SliderChange(30f);

        // Assert
        Assert.IsFalse(sliderController.buttonMax.activeSelf);
        Assert.IsTrue(sliderController.buttonMedium.activeSelf);
        Assert.IsFalse(sliderController.buttonOff.activeSelf);
        Assert.AreEqual("30 %", sliderController.valueText.text);
        Assert.AreEqual(30f, sliderController.getSliderValue());
    }

    // ѕроверка на значение слайдера равное 0%
    [Test]
    public void SliderChange_ActiveButtons_OffButtonActive()
    {
        // Arrange
        GameObject sliderObject = new GameObject();
        sliderObject.AddComponent<Slider>();
        SliderOnSettingsController sliderController = sliderObject.AddComponent<SliderOnSettingsController>();
        sliderController.buttonMax = new GameObject();
        sliderController.buttonMedium = new GameObject();
        sliderController.buttonOff = new GameObject();
        sliderController.valueText = new GameObject().AddComponent<TextMeshProUGUI>();

        // Act
        sliderController.SliderChange(0f);

        // Assert
        Assert.IsFalse(sliderController.buttonMax.activeSelf);
        Assert.IsFalse(sliderController.buttonMedium.activeSelf);
        Assert.IsTrue(sliderController.buttonOff.activeSelf);
        Assert.AreEqual("0 %", sliderController.valueText.text);
        Assert.AreEqual(0f, sliderController.getSliderValue());
    }
}
