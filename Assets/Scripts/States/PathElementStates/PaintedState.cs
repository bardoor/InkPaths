using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintedState : PathElementState
{
    public override void Enter()
    {
        // Если имеется текущий путь
        if (PathBuilder.Instance.Count > 0)
        {
            // Перенять его цвет
            Element.InkColor = PathBuilder.Instance.First.InkColor; 
        }
    }

    public override void HandleTouch()
    {
    }

    public override void Exit()
    {
    }
}
