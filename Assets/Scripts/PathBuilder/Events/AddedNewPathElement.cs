using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedNewPathElement : PathBuildingEvent
{
    private PathElement _addedElement;

    public PathElement AddedElement { get { return _addedElement; } }

    public AddedNewPathElement(PathElement addedElement)
    {
        _addedElement = addedElement;
    }
}
