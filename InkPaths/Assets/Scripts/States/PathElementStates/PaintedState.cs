using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintedState : PathElementState
{
    public override void Enter()
    {
        Debug.Log("Now I'm painted!");
        Element.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public override void Exit()
    {
        Debug.Log("Now I'm not painted!");
        Element.GetComponent<SpriteRenderer>().color = Color.red;
    }

}
