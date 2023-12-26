using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System;
using System.Xml;

public class TestSuite
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
        yield return gm.StartLevel(levelNumber);
    }

    [UnityTest]
    public IEnumerator TestNodesInitStates()
    {
        yield return LoadLevel(1);

        Node[] nodes = GameObject.FindObjectsByType<Node>(FindObjectsSortMode.None);

        List<PathElementState> realStates = new List<PathElementState>();
        foreach (Node node in nodes)
        {
            if (node.GetType() != typeof(InkBlob))
            {
                realStates.Add(node.GetState());
            }
        }

        Assert.IsTrue(realStates.Count > 0);

        PathElementState desiredState = new UnpaintableState();
        foreach (PathElementState realState in realStates)
        {
            Assert.AreEqual(desiredState.GetType(), realState.GetType());
        }
    }

    [UnityTest]
    public IEnumerator TestInkBlobsInitState()
    {
        yield return LoadLevel(1);

        Node[] blobs = GameObject.FindObjectsByType<InkBlob>(FindObjectsSortMode.None);

        List<Type> realStates = new List<Type>();
        foreach (Node blob in blobs)
        {
            realStates.Add(blob.GetState().GetType());
        }

        Assert.IsTrue(realStates.Count > 0);

        Type desiredState = typeof(PaintableState);
        foreach (Type realState in realStates)
        {
            Assert.AreEqual(desiredState, realState);
        }
    }

    [UnityTest]
    public IEnumerator TestInkBlobTouched()
    {
        yield return LoadLevel(1);

        Node[] blobs = GameObject.FindObjectsByType<InkBlob>(FindObjectsSortMode.None);

        foreach (InkBlob blob in blobs)
        {
            blob.HandleTouch();
            Type realState = blob.GetState().GetType();
            Type desiredState = typeof(PaintedState);
            PathBuilder.Instance.ClearCompletePaths();
        }
    }

    [UnityTest]
    public IEnumerator TestConnectionsAroundInkBlobWhenItTouched()
    {
        yield return LoadLevel(1);

        Node[] blobs = GameObject.FindObjectsByType<InkBlob>(FindObjectsSortMode.None);

        foreach (InkBlob blob in blobs)
        {
            blob.HandleTouch();
            var cons = blob.Connections();
            foreach (Connection con in cons)
            {
                Type realState = con.GetState().GetType();
                Type desiredState = typeof(PaintableState);
                Assert.AreEqual(realState, desiredState);
            }
            PathBuilder.Instance.Clear();
        }
    }

    [UnityTest]
    public IEnumerator TestNodeStateOnConnectionTouch()
    {
        yield return LoadLevel(1);

        Node[] blobs = GameObject.FindObjectsByType<InkBlob>(FindObjectsSortMode.None);

        foreach (InkBlob blob in blobs)
        {
            blob.HandleTouch();
            var cons = blob.Connections();
            foreach (Connection con in cons)
            {
                con.HandleTouch();
                Node endNode = con.ConnectedNodes.Find(node => node != blob);

                if (endNode.GetType() != typeof(InkBlob))
                    Assert.AreEqual(endNode.GetState().GetType(), typeof(PaintableState));
            }
            PathBuilder.Instance.Clear();
        }
    }
}
