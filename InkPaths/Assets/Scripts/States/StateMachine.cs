using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public PathElementState CurrentState { get; set; }
    private PathElement Element { get; set; }

    public void Initialize(PathElementState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(PathElementState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
