using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathElementState : State
{
    public static Color NoColor { get { return Color.clear; } }

    public PathElement Element { get; set; }

    public abstract void HandleTouch();
}
