using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;

public abstract class PathElement : MonoBehaviour, IStateChangeObservable
{
    protected StateMachine _stateMachine = new StateMachine();
    protected List<IStateChangeListener> _listeners { get; } = new List<IStateChangeListener>();
    public Color InkColor { get; set; }

    public abstract void SetPaintableAround();

    public abstract void SetUnpaintableAround();

    public abstract void HandleTouch();

    public virtual void Subscribe(IStateChangeListener listener) => _listeners.Add(listener);

    public virtual void Unsubscribe(IStateChangeListener listener) => _listeners.Remove(listener);

    public virtual void ReportStateChanged() 
    {
        foreach (IStateChangeListener listener in _listeners)
        {
            Debug.Log("Element " + GetType().Name + " is in " + _stateMachine.CurrentState.GetType().Name);
            listener.OnStateEnter(this, _stateMachine.CurrentState);
        }
    }
   
    public virtual void ChangeState(PathElementState state)
    {
        _stateMachine.ChangeState(state);
    }
}
