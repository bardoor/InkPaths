using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedBuildingPath : PathBuildingEvent
{
    private int _pathsCount;

    public int PathsCount { get { return _pathsCount; } }

    public FinishedBuildingPath(int pathsCount)
    {
        _pathsCount = pathsCount;
    }
}
