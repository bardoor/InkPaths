using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="States/PathElement/Painted")]

public class PaintedState : PathElementState
{
    public override void Enter()
    {
        Debug.Log("Now I'm painted!");
    }
    public override void Exit()
    {
        Debug.Log("Now I'm not painted!");
    }
}
