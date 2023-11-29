using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public PathElementState CurrentState { get; set; }
    private PathElement _element;

    public void Initialize(PathElement element, PathElementState startState)
    {
        _element = element;
        startState.Element = element;
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(PathElementState newState)
    {
        if (newState.GetType() == CurrentState.GetType())
            return;

        CurrentState.Exit();
        newState.Element = _element;
        CurrentState = newState;
        CurrentState.Enter();
        _element.ReportStateChanged();
    }
}
