using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintedState : PathElementState
{
    public override void Enter()
    {
        Debug.Log($"{Element.name} entered PaintedState!!!");
    }

    public override void HandleTouch()
    {
        Debug.Log($"{Element.name} handling touch!!!");
    }

    public override void Exit()
    {
        Debug.Log($"{Element.name} exited PaintedState!!!");
    }
}
