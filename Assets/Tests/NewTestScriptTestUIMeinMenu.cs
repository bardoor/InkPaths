using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;

// Проверка на работу событий в Main Menu
public class NewTestScriptTestUIMeinMenu
{
    // Проверка на работу событий при нажатии на кнопку "Settings"
    [UnityTest]
    public IEnumerator ClickButtonOpenSettings()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/GUI/MainMenu.unity");
        // Arrange

        GameObject buttonObject = GameObject.Find("SettingsButton");
        Button button = buttonObject.GetComponent<Button>();
        GameObject optionPanel = null;

        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "OptionPanel")
            {
                optionPanel = obj;
                break;
            }
        }

        // Act
        button.onClick.Invoke();

        yield return null;

        // Assert
        Assert.IsTrue(optionPanel.activeSelf);
    }

    // Проверка при нажатии кнопки закрытия OptionPanel
    [UnityTest]
    public IEnumerator ClickButtonCloseSettings()
    {

        // Arrange
        EditorSceneManager.OpenScene("Assets/Scenes/GUI/MainMenu.unity");
        GameObject mainMenu = GameObject.Find("MainMenu");
        GameObject optionPanel = null;

        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "OptionPanel")
            {
                optionPanel = obj;
                break;
            }
        }

        mainMenu.SetActive(false);
        optionPanel.SetActive(true);

        GameObject objectExitButton = GameObject.Find("ExitOption");
        Button exitButton = objectExitButton.GetComponent<Button>();


        // Act
        exitButton.onClick.Invoke();

        yield return null;

        // Assert
        Assert.IsTrue(mainMenu.activeSelf);
    }
}
