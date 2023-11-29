using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableState : PathElementState
{
    public override void Enter()
    {
        Debug.Log(Element.GetType().Name + " Entered state Paintable");
    }

    public override void Exit()
    {
        Debug.Log(Element.GetType().Name + "Exited state Paintable");
    }

    public override void HandleTouch()
    {
        switch (Element)
        {
            case Node node:
                node.ChangeState(new UnpaintableState());
                break;
            case Connection conn:
                conn.ChangeState(new PaintingState());
                break;
        }
    }
}
