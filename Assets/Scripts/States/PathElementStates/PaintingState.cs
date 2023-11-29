using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaintingState : PathElementState
{
    private bool _alreadyTouched = false;

    public override void Enter()
    {
        Debug.Log(Element.GetType().Name + "Entered state Painting");
        Element.SetPaintableAround();
    }

    public override void Exit()
    {
        _alreadyTouched = false;
        Debug.Log(Element.GetType().Name + "Exited state Painting");
    }

    public override void HandleTouch()
    {
        if (!_alreadyTouched)
        {
            Connection conn = Element as Connection;
            if (conn != null)
            {
                List<Node> nodes = conn.ConnectedNodes;

                Node start = nodes[0].InkColor != NoColor ? nodes[0] : nodes[1];
                Node end = nodes[0].InkColor == NoColor ? nodes[0] : nodes[1];

                start.SetUnpaintableAround();
                end.ChangeState(new PaintableState());
            }

            _alreadyTouched = true;
        }
    }
}
