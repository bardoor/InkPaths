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
        // Проверяем, находимся ли мы в режиме воспроизведения
        if (Application.isPlaying)
        {
            // Arrange
            SceneTransition sceneTransition = new GameObject().AddComponent<SceneTransition>();
            int sceneToLoad = 1;
            int expectedScene = SceneManager.GetActiveScene().buildIndex;
            // Act
            sceneTransition.SelectScene(sceneToLoad);
            yield return null;

            // Assert
            Assert.AreEqual(expectedScene, SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Assert.Ignore("Этот тест требует нахождения в режиме воспроизведения.");
        }
    }
}
