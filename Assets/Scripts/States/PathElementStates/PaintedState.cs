using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintedState : PathElementState
{
    public override void Enter()
    {
        if (PathBuilder.Instance.Count > 0)
        {
            Debug.LogAssertion($"~~~Current elements in path: {PathBuilder.Instance.Count}. They are: ~~~");
            PathBuilder.Instance.PrintAllElements();
            Debug.LogAssertion($"And now I grab {PathBuilder.Instance.Last.InkColor} from {PathBuilder.Instance.Last.gameObject.name}");
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
