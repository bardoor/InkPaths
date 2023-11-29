using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnpaintableState : PathElementState
{
    public override void Enter()
    {
        Debug.Log(Element.GetType().Name + "Entered state Unpaintable");
    }

    public override void Exit()
    {
        Debug.Log(Element.GetType().Name + "Exited state Unpaintable");
    }

    public override void HandleTouch()
    {
        
    }
}