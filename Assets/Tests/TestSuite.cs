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
        PathBuilder.Instance.Clear();
        Node[] blobs = GameObject.FindObjectsByType<InkBlob>(FindObjectsSortMode.None);

        foreach (InkBlob blob in blobs)
        {
            blob.HandleTouch();
            Type realState = blob.GetState().GetType();
            Type desiredState = typeof(PaintedState);
            Assert.AreEqual(desiredState, realState); 

            PathBuilder.Instance.Clear();
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
                Assert.AreEqual(desiredState, realState);
            }
            PathBuilder.Instance.Clear();
        }
    }

    [UnityTest]
    public IEnumerator TestNodeStateOnTouch()
    {
        yield return LoadLevel(1);

        PathBuilder.Instance.Clear();

        Node[] nodes = GameObject.FindObjectsByType<Node>(FindObjectsSortMode.None);

        foreach (Node node in nodes)
        {
            node.HandleTouch();

            if (node.GetType() == typeof(Node))
            {
                Assert.AreEqual(typeof(UnpaintableState), node.GetState().GetType());
            }
            else
            {
                Assert.AreEqual(typeof(PaintedState), node.GetState().GetType());
                PathBuilder.Instance.Clear();
            }
        }
    }

    [UnityTest]
    public IEnumerator TestNodeResetAround()
    {
        yield return LoadLevel(1);
        PathBuilder.Instance.Clear();
        Node[] nodes = GameObject.FindObjectsByType<Node>(FindObjectsSortMode.None);

        foreach (Node node in nodes)
        {
            node.ResetAnythingAround();
            foreach (Connection con in node.Connections())
            {
                Assert.AreEqual(typeof(UnpaintableState), con.GetState().GetType());
            }
        }
    }

    [UnityTest]
    public IEnumerator TestConnectionResetAround()
    {
        yield return LoadLevel(1);
        PathBuilder.Instance.Clear();
        Connection[] cons = GameObject.FindObjectsByType<Connection>(FindObjectsSortMode.None);

        foreach (Connection con in cons)
        {
            con.ResetAnythingButInkBlob();
            foreach (Node node in con.ConnectedNodes)
            {
                if (node is InkBlob)
                    Assert.AreEqual(typeof(PaintableState), node.GetState().GetType());
                else
                    Assert.AreEqual(typeof(UnpaintableState), node.GetState().GetType());
            }
        }
    }

    [UnityTest]
    public IEnumerator TestNodeSetPaintableAround()
    {
        yield return LoadLevel(1);
        PathBuilder.Instance.Clear();
        Node[] nodes = GameObject.FindObjectsByType<Node>(FindObjectsSortMode.None);

        foreach (Node node in nodes)
        {
            node.SetPaintableAround();
            foreach (Connection con in node.Connections())
            {
                Assert.AreEqual(typeof(PaintableState), con.GetState().GetType());
            }
        }
    }

    [UnityTest]
    public IEnumerator TestConnectionSetUnpaintableAround()
    {
        yield return LoadLevel(1);
        PathBuilder.Instance.Clear();
        Connection[] cons = GameObject.FindObjectsByType<Connection>(FindObjectsSortMode.None);

        foreach (Connection con in cons)
        {
            con.ResetAnythingButInkBlob();
            foreach (Node node in con.ConnectedNodes)
            {
                if (node is InkBlob)
                    Assert.AreEqual(typeof(PaintableState), node.GetState().GetType());
                else
                    Assert.AreEqual(typeof(UnpaintableState), node.GetState().GetType());
            }
        }
    }

    [UnityTest]
    public IEnumerator TestConnectionSetPaintableAround()
    {
        yield return LoadLevel(1);
        PathBuilder.Instance.Clear();
        Connection[] cons = GameObject.FindObjectsByType<Connection>(FindObjectsSortMode.None);

        foreach (Connection con in cons)
        {
            con.SetPaintableAround();
            foreach (Node node in con.ConnectedNodes)
            {
                Debug.Log($"Node {node.GetState()} {node.gameObject.name}");
                Assert.AreEqual(typeof(PaintableState), node.GetState().GetType());
            }
        }
    }

    [UnityTest]
    public IEnumerator TestConnectionTouched()
    {
        yield return LoadLevel(1);
        PathBuilder.Instance.Clear();
        Connection[] cons = GameObject.FindObjectsByType<Connection>(FindObjectsSortMode.None);

        foreach (Connection con in cons)
        {
            con.HandleTouch();
            Assert.AreEqual(typeof(UnpaintableState), con.GetState().GetType());
        }
    }

    [UnityTest]
    public IEnumerator TestAroundOnConnectionTouched()
    {
        yield return LoadLevel(1);
        PathBuilder.Instance.Clear();
        Connection[] cons = GameObject.FindObjectsByType<Connection>(FindObjectsSortMode.None);

        foreach (Connection con in cons)
        {
            con.HandleTouch();
            foreach (Node node in con.ConnectedNodes)
            {
                if (node is InkBlob)
                    Assert.AreEqual(typeof(PaintableState), node.GetState().GetType());
                else
                    Assert.AreEqual(typeof(UnpaintableState), node.GetState().GetType());
            }
        }
    }
}
