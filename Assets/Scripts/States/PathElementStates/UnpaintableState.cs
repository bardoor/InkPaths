using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpaintableState : PathElementState
{
    public override void Enter()
    {
        Debug.Log($"{Element.name} entered UnpaintableState!!!");
    }

    public override void HandleTouch()
    {
        Debug.Log($"{Element.name} handling touch!!!");
    }

    public override void Exit()
    {
        Debug.Log($"{Element.name} exited UnpaintableState!!!");
    }
}
