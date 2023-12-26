using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

// �������� ������������ ������ Option Panel � MainMunu
public class NewTestScriptAnimControllerOptPanel
{
    // �������� ����� �������� Option Panel
    [Test]
    public void ChoseAnimation_SetsInteger()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        AnimControllerOptPanel animController = gameObject.AddComponent<AnimControllerOptPanel>();
        gameObject.AddComponent<Animator>();
        string path = "Assets/Animations/GUI/OptionPanel.controller";
        gameObject.GetComponent<Animator>().runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(path);

        // Act 1
        animController.ChoseAnimation(1);

        // Assert 1
        Assert.AreEqual(1, animController.GetComponent<Animator>().GetInteger("switchAnim"));

        // Act 2
        animController.ChoseAnimation(2);

        // Assert 2
        Assert.AreEqual(2, animController.GetComponent<Animator>().GetInteger("switchAnim"));
    }

    [Test]
    public void InactivePanel_DeactivatesGameObject()
    {
        // �������� �� ��������� ���������� Option Panel
        // Arrange
        GameObject gameObject = new GameObject();
        AnimControllerOptPanel animController = gameObject.AddComponent<AnimControllerOptPanel>();

        // Act
        animController.InactivePanel();

        // Assert
        Assert.IsFalse(gameObject.activeSelf);
    }
}
