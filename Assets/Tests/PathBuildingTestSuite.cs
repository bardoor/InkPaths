using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PathBuildingTestSuite
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
    public IEnumerator EmptyPathBuilderFromTheStart()
    {
        yield return LoadLevel(1);

        Assert.AreEqual(0, PathBuilder.Instance.CompletePathsCount);
        Assert.AreEqual(0, PathBuilder.Instance.CurrentPathElementsCount);
    }

    [UnityTest]
    public IEnumerator PathBuilderStoresPathElements()
    {
        yield return LoadLevel(1);

        Assert.AreEqual(0, PathBuilder.Instance.CompletePathsCount);
        Assert.AreEqual(0, PathBuilder.Instance.CurrentPathElementsCount);

        InkBlob blob = GameObject.FindFirstObjectByType<InkBlob>();
        PathBuilder.Instance.AddElement(blob);
        Assert.AreEqual(0, PathBuilder.Instance.CompletePathsCount);
        Assert.AreEqual(1, PathBuilder.Instance.CurrentPathElementsCount);

        PathBuilder.Instance.Clear();
        PathBuilder.Instance.ClearCompletePaths();
    }

    [UnityTest]
    public IEnumerator PathBuilderCancelesPath()
    {
        yield return LoadLevel(1);

        Assert.AreEqual(0, PathBuilder.Instance.CompletePathsCount);
        Assert.AreEqual(0, PathBuilder.Instance.CurrentPathElementsCount);

        InkBlob blob = GameObject.FindFirstObjectByType<InkBlob>();
        PathBuilder.Instance.AddElement(blob);
        Assert.AreEqual(0, PathBuilder.Instance.CompletePathsCount);
        Assert.AreEqual(1, PathBuilder.Instance.CurrentPathElementsCount);

        PathBuilder.Instance.CancelBuildingCurrentPath();
        Assert.AreEqual(0, PathBuilder.Instance.CompletePathsCount);
        Assert.AreEqual(0, PathBuilder.Instance.CurrentPathElementsCount);

        PathBuilder.Instance.Clear();
        PathBuilder.Instance.ClearCompletePaths();
    }

    [UnityTest]
    public IEnumerator PathBuilderRecognizesCompletePath()
    {
        yield return LoadLevel(1);

        Assert.AreEqual(0, PathBuilder.Instance.CompletePathsCount);
        Assert.AreEqual(0, PathBuilder.Instance.CurrentPathElementsCount);

        InkBlob start_blob = GameObject.FindFirstObjectByType<InkBlob>();
        PathBuilder.Instance.AddElement(start_blob);

        Connection start_conn = start_blob.Connections().ElementAt(1);
        PathBuilder.Instance.AddElement(start_conn);

        Node middle_node = start_conn.ConnectedNodes.ElementAt(1);
        PathBuilder.Instance.AddElement(middle_node);

        Connection end_conn = middle_node.Connections().ElementAt(0);
        PathBuilder.Instance.AddElement(end_conn);

        InkBlob end_blob = end_conn.ConnectedNodes.ElementAt(0) as InkBlob;
        PathBuilder.Instance.AddElement(end_blob);

        Assert.AreEqual(2, PathBuilder.Instance.CurrentPathElementsCount);

        PathBuilder.Instance.Clear();
        PathBuilder.Instance.ClearCompletePaths();
    }

    [UnityTest]
    public IEnumerator PathBuilderHasNoPathWithSuchElement()
    {
        yield return LoadLevel(1);

        Assert.AreEqual(0, PathBuilder.Instance.CompletePathsCount);
        Assert.AreEqual(0, PathBuilder.Instance.CurrentPathElementsCount);

        InkBlob start_blob = GameObject.FindFirstObjectByType<InkBlob>();
        PathBuilder.Instance.AddElement(start_blob);

        Assert.IsNull(PathBuilder.Instance.GetCompletePathThatHas(start_blob));

        PathBuilder.Instance.Clear();
        PathBuilder.Instance.ClearCompletePaths();
    }

    [UnityTest]
    public IEnumerator PathBuilderFirstAndLastAreSame()
    {
        yield return LoadLevel(1);

        Assert.AreEqual(0, PathBuilder.Instance.CompletePathsCount);
        Assert.AreEqual(0, PathBuilder.Instance.CurrentPathElementsCount);

        InkBlob start_blob = GameObject.FindFirstObjectByType<InkBlob>();
        PathBuilder.Instance.AddElement(start_blob);

        Assert.AreSame(start_blob, PathBuilder.Instance.First);
        Assert.AreSame(PathBuilder.Instance.First, PathBuilder.Instance.Last);

        PathBuilder.Instance.Clear();
        PathBuilder.Instance.ClearCompletePaths();
    }

    [UnityTest]
    public IEnumerator PathBuilderSeesPathIsIncomplete()
    {
        yield return LoadLevel(1);

        Assert.AreEqual(0, PathBuilder.Instance.CompletePathsCount);
        Assert.AreEqual(0, PathBuilder.Instance.CurrentPathElementsCount);

        InkBlob start_blob = GameObject.FindFirstObjectByType<InkBlob>();
        PathBuilder.Instance.AddElement(start_blob);

        Assert.IsFalse(PathBuilder.Instance.IsFinishedPath());

        PathBuilder.Instance.Clear();
        PathBuilder.Instance.ClearCompletePaths();
    }
}
