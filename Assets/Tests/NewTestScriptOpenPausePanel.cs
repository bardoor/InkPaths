using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class NewTestScriptOpenPausePanel
{
    // Проверка на количество звёзд в зависимости от значения таймера
    [Test]
    public void OpenPausePanelWithThreeStar()
    {
        // Arrange
        GameObject pausePanel = new GameObject();
        OpenPausePanel pausePanelController = pausePanel.AddComponent<OpenPausePanel>();
        GameObject timer = new GameObject();
        timer.AddComponent<Slider>();
        timer.GetComponent<Slider>().minValue = 0;
        timer.GetComponent<Slider>().maxValue = 100;
        timer.GetComponent<Slider>().value = 100;

        GameObject buttonPause = new GameObject();
        buttonPause.AddComponent<Button>();
        GameObject buttonSettings = new GameObject();
        buttonSettings.AddComponent<Button>();
        GameObject buttonQuestion = new GameObject();
        buttonQuestion.AddComponent<Button>();
        GameObject pauseBlurPanel = new GameObject();

        GameObject star1 = new GameObject();
        star1.AddComponent<Image>();
        GameObject star2 = new GameObject();
        star2.AddComponent<Image>();
        GameObject star3 = new GameObject();
        star3.AddComponent<Image>();

        pausePanelController.buttonPause = buttonPause;
        pausePanelController.buttonSettings = buttonSettings;
        pausePanelController.buttonQuestion = buttonQuestion;
        pausePanelController.pauseBlurPanel = pauseBlurPanel;
        pausePanelController.star_1 = star1.GetComponent<Image>();
        pausePanelController.star_2 = star2.GetComponent<Image>();
        pausePanelController.star_3 = star3.GetComponent<Image>();
        pausePanelController.timer = timer.GetComponent<Slider>();

        // Act
        pausePanelController.StarController();

        // Assert
        Assert.IsTrue(star1.activeSelf);
        Assert.IsTrue(star2.activeSelf);
        Assert.IsTrue(star3.activeSelf);
    }

    // Проверка на количество звёзд в зависимости от значения таймера
    [Test]
    public void OpenPausePanelWithTwoStar()
    {
        // Arrange
        GameObject pausePanel = new GameObject();
        OpenPausePanel pausePanelController = pausePanel.AddComponent<OpenPausePanel>();
        GameObject timer = new GameObject();
        timer.AddComponent<Slider>();
        timer.GetComponent<Slider>().minValue = 0;
        timer.GetComponent<Slider>().maxValue = 100;
        timer.GetComponent<Slider>().value = 50;

        GameObject buttonPause = new GameObject();
        buttonPause.AddComponent<Button>();
        GameObject buttonSettings = new GameObject();
        buttonSettings.AddComponent<Button>();
        GameObject buttonQuestion = new GameObject();
        buttonQuestion.AddComponent<Button>();
        GameObject pauseBlurPanel = new GameObject();

        GameObject star1 = new GameObject();
        star1.AddComponent<Image>();
        GameObject star2 = new GameObject();
        star2.AddComponent<Image>();
        GameObject star3 = new GameObject();
        star3.AddComponent<Image>();

        pausePanelController.buttonPause = buttonPause;
        pausePanelController.buttonSettings = buttonSettings;
        pausePanelController.buttonQuestion = buttonQuestion;
        pausePanelController.pauseBlurPanel = pauseBlurPanel;
        pausePanelController.star_1 = star1.GetComponent<Image>();
        pausePanelController.star_2 = star2.GetComponent<Image>();
        pausePanelController.star_3 = star3.GetComponent<Image>();
        pausePanelController.timer = timer.GetComponent<Slider>();

        // Act
        pausePanelController.StarController();

        // Assert
        Assert.IsTrue(star1.activeSelf);
        Assert.IsTrue(star2.activeSelf);
        Assert.IsFalse(star3.activeSelf);
    }

    // Проверка на количество звёзд в зависимости от значения таймера
    [Test]
    public void OpenPausePanelWithOneStar()
    {
        // Arrange
        GameObject pausePanel = new GameObject();
        OpenPausePanel pausePanelController = pausePanel.AddComponent<OpenPausePanel>();
        GameObject timer = new GameObject();
        timer.AddComponent<Slider>();
        timer.GetComponent<Slider>().minValue = 0;
        timer.GetComponent<Slider>().maxValue = 100;
        timer.GetComponent<Slider>().value = 25;

        GameObject buttonPause = new GameObject();
        buttonPause.AddComponent<Button>();
        GameObject buttonSettings = new GameObject();
        buttonSettings.AddComponent<Button>();
        GameObject buttonQuestion = new GameObject();
        buttonQuestion.AddComponent<Button>();
        GameObject pauseBlurPanel = new GameObject();

        GameObject star1 = new GameObject();
        star1.AddComponent<Image>();
        GameObject star2 = new GameObject();
        star2.AddComponent<Image>();
        GameObject star3 = new GameObject();
        star3.AddComponent<Image>();

        pausePanelController.buttonPause = buttonPause;
        pausePanelController.buttonSettings = buttonSettings;
        pausePanelController.buttonQuestion = buttonQuestion;
        pausePanelController.pauseBlurPanel = pauseBlurPanel;
        pausePanelController.star_1 = star1.GetComponent<Image>();
        pausePanelController.star_2 = star2.GetComponent<Image>();
        pausePanelController.star_3 = star3.GetComponent<Image>();
        pausePanelController.timer = timer.GetComponent<Slider>();

        // Act
        pausePanelController.StarController();

        // Assert
        Assert.IsTrue(star1.activeSelf);
        Assert.IsFalse(star2.activeSelf);
        Assert.IsFalse(star3.activeSelf);
    }

    // Проверка на количество звёзд в зависимости от значения таймера
    [Test]
    public void OpenPausePanelWithZeroStar()
    {
        // Arrange
        GameObject pausePanel = new GameObject();
        OpenPausePanel pausePanelController = pausePanel.AddComponent<OpenPausePanel>();
        GameObject timer = new GameObject();
        timer.AddComponent<Slider>();
        timer.GetComponent<Slider>().minValue = 0;
        timer.GetComponent<Slider>().maxValue = 100;
        timer.GetComponent<Slider>().value = 0;

        GameObject buttonPause = new GameObject();
        buttonPause.AddComponent<Button>();
        GameObject buttonSettings = new GameObject();
        buttonSettings.AddComponent<Button>();
        GameObject buttonQuestion = new GameObject();
        buttonQuestion.AddComponent<Button>();
        GameObject pauseBlurPanel = new GameObject();

        GameObject star1 = new GameObject();
        star1.AddComponent<Image>();
        GameObject star2 = new GameObject();
        star2.AddComponent<Image>();
        GameObject star3 = new GameObject();
        star3.AddComponent<Image>();

        pausePanelController.buttonPause = buttonPause;
        pausePanelController.buttonSettings = buttonSettings;
        pausePanelController.buttonQuestion = buttonQuestion;
        pausePanelController.pauseBlurPanel = pauseBlurPanel;
        pausePanelController.star_1 = star1.GetComponent<Image>();
        pausePanelController.star_2 = star2.GetComponent<Image>();
        pausePanelController.star_3 = star3.GetComponent<Image>();
        pausePanelController.timer = timer.GetComponent<Slider>();

        // Act
        pausePanelController.StarController();

        // Assert
        Assert.IsFalse(star1.activeSelf);
        Assert.IsFalse(star2.activeSelf);
        Assert.IsFalse(star3.activeSelf);
    }

    // Проверка активность кнопок при открытии панели
    [Test]
    public void OpenPausePanel()
    {
        // Arrange
        GameObject pausePanel = new GameObject();
        OpenPausePanel pausePanelController = pausePanel.AddComponent<OpenPausePanel>();

        GameObject buttonPause = new GameObject();
        buttonPause.AddComponent<Button>();
        GameObject buttonSettings = new GameObject();
        buttonSettings.AddComponent<Button>();
        GameObject buttonQuestion = new GameObject();
        buttonQuestion.AddComponent<Button>();
        GameObject pauseBlurPanel = new GameObject();
        pauseBlurPanel.SetActive(false);


        pausePanelController.buttonPause = buttonPause;
        pausePanelController.buttonSettings = buttonSettings;
        pausePanelController.buttonQuestion = buttonQuestion;
        pausePanelController.pauseBlurPanel = pauseBlurPanel;

        // Act
        pausePanelController.UnclickableButtons();

        // Assert
        Assert.IsFalse(buttonPause.GetComponent<Button>().interactable);
        Assert.IsFalse(buttonSettings.GetComponent<Button>().interactable);
        Assert.IsFalse(buttonQuestion.GetComponent<Button>().interactable);
        Assert.IsTrue(pauseBlurPanel.activeSelf);
    }

    // Проверка активность кнопок при закрытии панели
    [Test]
    public void ClosePausePanel()
    {
        // Arrange
        GameObject pausePanel = new GameObject();
        OpenPausePanel pausePanelController = pausePanel.AddComponent<OpenPausePanel>();

        GameObject buttonPause = new GameObject();
        buttonPause.AddComponent<Button>();
        buttonPause.GetComponent<Button>().interactable = false;
        GameObject buttonSettings = new GameObject();
        buttonSettings.AddComponent<Button>();
        buttonSettings.GetComponent<Button>().interactable = false;
        GameObject buttonQuestion = new GameObject();
        buttonQuestion.AddComponent<Button>();
        buttonQuestion.GetComponent<Button>().interactable = false;
        GameObject pauseBlurPanel = new GameObject();


        pausePanelController.buttonPause = buttonPause;
        pausePanelController.buttonSettings = buttonSettings;
        pausePanelController.buttonQuestion = buttonQuestion;
        pausePanelController.pauseBlurPanel = pauseBlurPanel;

        // Act
        pausePanelController.ClickableButtons();

        // Assert
        Assert.IsTrue(buttonPause.GetComponent<Button>().interactable);
        Assert.IsTrue(buttonSettings.GetComponent<Button>().interactable);
        Assert.IsTrue(buttonQuestion.GetComponent<Button>().interactable);
        Assert.IsFalse(pauseBlurPanel.activeSelf);
    }
}
