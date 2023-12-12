using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

public class StateMachine
{
    public PathElementState CurrentState { get; set; }
    private PathElement _element;

    // публичный, иначе не работает IsValidTransition
    public static readonly Dictionary<Type, Type[]> validStateTransitions = new Dictionary<Type, Type[]>()
    {
        { typeof(PaintedState), new Type[]{ typeof(UnpaintableState) } },
        { typeof(UnpaintableState), new Type[]{ typeof(PaintableState) } },
        { typeof(PaintableState), new Type[] { typeof(UnpaintableState), typeof(PaintedState) } }
    };

    public bool IsValidTransition(State newState)
    {
        return IsValidTransition(CurrentState, newState);
    }

    public static bool IsValidTransition(State startState, State nextState)
    {
        Type[] validTransitions = validStateTransitions[startState.GetType()];
        return validTransitions.Contains(nextState.GetType());
    }

    public virtual bool Initialize(PathElement element, PathElementState startState)
    {
        if (validStateTransitions.ContainsKey(startState.GetType()))
        {
            _element = element;
            startState.Element = element;
            CurrentState = startState;
            CurrentState.Enter();
            UpdateElementText();
            return true;
        }
        
        return false;
    }

    public virtual bool ChangeState(PathElementState newState)
    {
        if (!IsValidTransition(newState))
        {
            return false;
        }

        CurrentState.Exit();
        newState.Element = _element;
        CurrentState = newState;
        CurrentState.Enter();
        _element.ReportStateChanged();
        UpdateElementText();
        return true;
    }

    private void UpdateElementText()
    {
        GameObject debugTextContainer = _element._debugTextContainer;

        TextMeshProUGUI textMeshPro = debugTextContainer.GetComponent<TextMeshProUGUI>();

        textMeshPro.text = CurrentState.ToString();

        debugTextContainer.transform.localPosition = new Vector2(0.0f, 0.0f);
    }
}
