using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathElement : MonoBehaviour, IStateChangeObservable
{
    protected StateMachine _stateMachine = new StateMachine();
    protected List<IStateChangeListener> _listeners { get; } = new List<IStateChangeListener>();
    protected Color _inkColor { get; set; }

    public virtual void UpdateElementsStatesAround() { }


    public virtual void Subscribe(IStateChangeListener listener) => _listeners.Add(listener);

    public virtual void Unsubscribe(IStateChangeListener listener) => _listeners.Remove(listener);

    public virtual void ReportStateChanged() 
    {
        foreach (IStateChangeListener listener in _listeners)
        {
            listener.OnStateEnter(this, _stateMachine.CurrentState);
        }
    }
   
}
