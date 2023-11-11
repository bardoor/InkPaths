using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBuilder
{
    private static PathBuilder instance;

    private Path[] _paths { get; }

    private Path _currentPath;

    private PathBuilder() { }

    public static PathBuilder Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PathBuilder();
            }
            return instance;
        }
    }
    
    public void CancelBuilding()
    {

    }

    public void StartBuild()
    {

    }

    public void AddElement(PathElement element)
    {

    }

}
