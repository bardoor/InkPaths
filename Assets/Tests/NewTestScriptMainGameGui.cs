using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class NewTestScriptMainGameGui
{
    private IEnumerator LoadLevel(int levelNumber)
    {
        string mainMenuPath = "Assets/Scenes/MainMenu.unity";

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuPath);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        GameManager gm = GameObject.FindFirstObjectByType<GameManager>();

        gm.InitManagers();
        yield return gm.StartLevel(1);
    }

    // �������� ��� ������� �� ������ �����
    [UnityTest]
    public IEnumerator ClickButtonPauseTest()
    { // Arrange
      // ��������� ������ ����� ��� �����
      //EditorSceneManager.OpenScene("Assets/Scenes/GUI/MainGameProcess.unity");
      yield return LoadLevel(1);

        // ������
        Button pauseButton = (GameObject.Find("ButtonPause")).GetComponent<Button>();
        Button optionButton = (GameObject.Find("ButtonSettings")).GetComponent<Button>();
        Button questionButton = (GameObject.Find("ButtonQustion")).GetComponent<Button>();

        // ������
        GameObject pauseBlurPanel = null;
        GameObject pauseMenu = null;
        GameObject optionPanel = null;
        GameObject lossPanel = null;
        GameObject winerPanel = null;
        GameObject infoPanel = null;

        // ������� ������ ������
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "PauseMenu")
            {
                pauseMenu = obj;
            }
            else if(obj.name == "PauseBlurPanel")
            {
                pauseBlurPanel = obj;
            }
            else if(obj.name == "OptionPanel")
            {
                optionPanel = obj;
            }
            else if (obj.name == "LossPanel")
            {
                lossPanel = obj;
            }
            else if (obj.name == "WinnerPanel")
            {
                winerPanel = obj;
            }
            else if (obj.name == "InfoPanel")
            {
                infoPanel = obj;
            }
        }

        // Act
        pauseButton.onClick.Invoke();

        yield return null;

        // Assert
        // �������� �������� �������
        Assert.IsTrue(pauseBlurPanel.activeSelf);
        Assert.IsTrue(pauseMenu.activeSelf);
        Assert.IsFalse(optionPanel.activeSelf);
        Assert.IsFalse(lossPanel.activeSelf);
        Assert.IsFalse(winerPanel.activeSelf);
        Assert.IsFalse(infoPanel.activeSelf);

        // �������� ��������������� ������
        Assert.IsFalse(pauseButton.interactable);
        Assert.IsFalse(optionButton.interactable);
        Assert.IsFalse(questionButton.interactable);
    }

    // �������� ��� ������� �� ������ ��������
    [UnityTest]
    public IEnumerator ClickButtonSettingsTest()
    { // Arrange
        // ��������� ������ ����� ��� �����
        //EditorSceneManager.OpenScene("Assets/Scenes/GUI/MainGameProcess.unity");
        yield return LoadLevel(1);

        // ������
        Button pauseButton = (GameObject.Find("ButtonPause")).GetComponent<Button>();
        Button optionButton = (GameObject.Find("ButtonSettings")).GetComponent<Button>();
        Button questionButton = (GameObject.Find("ButtonQustion")).GetComponent<Button>();

        // ������
        GameObject pauseBlurPanel = null;
        GameObject pauseMenu = null;
        GameObject optionPanel = null;
        GameObject lossPanel = null;
        GameObject winerPanel = null;
        GameObject infoPanel = null;

        // ������� ������ ������
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "PauseMenu")
            {
                pauseMenu = obj;
            }
            else if (obj.name == "PauseBlurPanel")
            {
                pauseBlurPanel = obj;
            }
            else if (obj.name == "OptionPanel")
            {
                optionPanel = obj;
            }
            else if (obj.name == "LossPanel")
            {
                lossPanel = obj;
            }
            else if (obj.name == "WinnerPanel")
            {
                winerPanel = obj;
            }
            else if (obj.name == "InfoPanel")
            {
                infoPanel = obj;
            }
        }

        // Act
        optionButton.onClick.Invoke();

        yield return null;

        // Assert
        // �������� �������� �������
        Assert.IsTrue(pauseBlurPanel.activeSelf);
        Assert.IsFalse(pauseMenu.activeSelf);
        Assert.IsTrue(optionPanel.activeSelf);
        Assert.IsFalse(lossPanel.activeSelf);
        Assert.IsFalse(winerPanel.activeSelf);
        Assert.IsFalse(infoPanel.activeSelf);

        // �������� ��������������� ������
        Assert.IsFalse(pauseButton.interactable);
        Assert.IsFalse(optionButton.interactable);
        Assert.IsFalse(questionButton.interactable);
    }

    // �������� ��� ������� �� ������ ����������
    [UnityTest]
    public IEnumerator ClickButtonQuestionTest()
    { // Arrange
        // ��������� ������ ����� ��� �����
        //EditorSceneManager.OpenScene("Assets/Scenes/GUI/MainGameProcess.unity");
        yield return LoadLevel(1);

        // ������
        Button pauseButton = (GameObject.Find("ButtonPause")).GetComponent<Button>();
        Button optionButton = (GameObject.Find("ButtonSettings")).GetComponent<Button>();
        Button questionButton = (GameObject.Find("ButtonQustion")).GetComponent<Button>();

        // ������
        GameObject pauseMenu = null;
        GameObject optionPanel = null;
        GameObject lossPanel = null;
        GameObject winerPanel = null;
        GameObject infoPanel = null;

        // ������� ������ ������
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "PauseMenu")
            {
                pauseMenu = obj;
            }
            else if (obj.name == "OptionPanel")
            {
                optionPanel = obj;
            }
            else if (obj.name == "LossPanel")
            {
                lossPanel = obj;
            }
            else if (obj.name == "WinnerPanel")
            {
                winerPanel = obj;
            }
            else if (obj.name == "InfoPanel")
            {
                infoPanel = obj;
            }
        }

        // Act
        questionButton.onClick.Invoke();

        yield return null;

        // Assert
        // �������� �������� �������
        Assert.IsFalse(pauseMenu.activeSelf);
        Assert.IsFalse(optionPanel.activeSelf);
        Assert.IsFalse(lossPanel.activeSelf);
        Assert.IsFalse(winerPanel.activeSelf);
        Assert.IsTrue(infoPanel.activeSelf);
    }
}
