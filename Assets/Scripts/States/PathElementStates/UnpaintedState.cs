using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpaintedState : PathElementState
{

    public override void Enter()
    {
        Debug.Log("Я стал незакрашенным.");
    }

    public override void Exit()
    {
        Debug.Log("Я перестал быть незакрашенным.");
    }

}
