using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintedState : PathElementState
{
    public override void Enter()
    {
        Debug.Log(Element.GetType().Name + "Entered state Painted");
    }

    public override void Exit()
    {
        Debug.Log(Element.GetType().Name + "Exited state Painted");
    }

    public override void HandleTouch()
    {
        Debug.Log("PaintedState handles touch");
        Element.SetPaintableAround();
    }
}
