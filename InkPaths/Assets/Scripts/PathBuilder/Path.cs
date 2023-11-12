using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    private List<PathElement> pathElements = new List<PathElement>();
    public List<PathElement> GetPath()
    {
        return pathElements;
    }
    public void AddElement(PathElement element)
    {
        pathElements.Add(element);
    }

    public static object Combine(string dataPath, string v)
    {
        throw new NotImplementedException();
    }
}
