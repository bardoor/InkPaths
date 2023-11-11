using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingState : PathElementState
{

    public override void Enter()
    {
        Debug.Log("Меня закрашиваютъ!");
    }

    public override void Exit()
    {
        Debug.Log("Меня перестали закрашивать!");
    }

}
