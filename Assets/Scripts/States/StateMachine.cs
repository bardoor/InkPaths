using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class StateMachine
{
    public PathElementState CurrentState { get; set; }
    private PathElement _element;
    private Dictionary<Type, Type[]> _validStateTransitions;

    public void SetValidStateTransitions(Type[] stateTransitionCycle)
    {
        _validStateTransitions = stateTransitionCycle
            .Zip(stateTransitionCycle.Skip(1), (curState, nextState) => new {key = curState, value = nextState})
            .ToDictionary(pair => pair.key, pair => new Type[] { pair.value });
    }

    public bool TransitionIsValid(State newState)
    {
        return IsValidTransition(CurrentState, newState);
    }

    private bool IsValidTransition(State startState, State nextState)
    {
        Type[] validTransitions = GetValidTransitions(startState);
        return validTransitions.Contains(nextState.GetType());
    }

    private Type[] GetValidTransitions(State startState)
    {
        Type startStateType = startState.GetType();
        Type[] validTransitions = new Type[] { };

        if (_validStateTransitions.ContainsKey(startStateType))
        {
            validTransitions = _validStateTransitions[startStateType];
        }

        return validTransitions;
    }

    public virtual bool Initialize(PathElement element, PathElementState startState)
    {
        _element = element;
        startState.Element = element;
        CurrentState = startState;
        
        if (_validStateTransitions.ContainsKey(startState.GetType()))
        {
            CurrentState.Enter();
            return true;
        }
        
        return false;
    }

    public virtual bool ChangeState(PathElementState newState)
    {
        if (!IsValidTransition(CurrentState, newState))
            return false;

        CurrentState.Exit();
        newState.Element = _element;
        CurrentState = newState;
        CurrentState.Enter();
        _element.ReportStateChanged();
        return true;
    }
}
