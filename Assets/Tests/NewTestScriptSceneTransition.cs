using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class NewTestScriptSceneTransition
{
    [UnityTest]
    public IEnumerator LoadSceneAsync_ValidScene_LoadsScene()
    {
        // ���������, ��������� �� �� � ������ ���������������
        if (Application.isPlaying)
        {
            // Arrange
            SceneTransition sceneTransition = new GameObject().AddComponent<SceneTransition>();
            int sceneToLoad = 1;

            // Act
            sceneTransition.SelectScene(sceneToLoad);
            yield return null;

            // Assert
            Assert.AreEqual(4, SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Assert.Ignore("���� ���� ������� ���������� � ������ ���������������.");
        }
    }
}
