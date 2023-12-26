using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class NewTestScriptButtonOnSettingsController
{
    // Проверка на изменение значений слайдера от нажатия на кнопки громкости
    [Test]
    public void TestSound()
    {
        // Arrange
        GameObject objectSlider = new GameObject();
        objectSlider.AddComponent<Slider>();
        objectSlider.GetComponent<Slider>().wholeNumbers = true;
        objectSlider.GetComponent<Slider>().minValue = 0;
        objectSlider.GetComponent<Slider>().maxValue = 100;
        objectSlider.GetComponent<Slider>().value = 50;
        GameObject obj = new GameObject();
        ButtonOnSettingsController controller =  obj.AddComponent<ButtonOnSettingsController>();
        controller.slider = objectSlider.GetComponent<Slider>();

        // Act 1
        controller.SoundOff();

        // Assert 1
        Assert.AreEqual(0f, objectSlider.GetComponent<Slider>().value);

        // Act 2
        controller.SoundOn();

        // Assert 2
        Assert.AreEqual(50f, objectSlider.GetComponent<Slider>().value);
    }
}
