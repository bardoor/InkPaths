using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;

public abstract class PathElement : MonoBehaviour, IStateChangeObservable
{
    protected StateMachine _colorabiltyStateMachine = new StateMachine();
    protected StateMachine _colorationStateMachine = new StateMachine();

    protected List<IStateChangeListener> _listeners { get; } = new List<IStateChangeListener>();
    public Color InkColor { get; set; }



    public virtual void Subscribe(IStateChangeListener listener) => _listeners.Add(listener);

    public virtual void Unsubscribe(IStateChangeListener listener) => _listeners.Remove(listener);

    public virtual void ReportStateChanged() 
    {
        foreach (IStateChangeListener listener in _listeners)
        {
            Debug.Log($"Element {GetType().Name} is in {_colorationStateMachine.CurrentState.GetType().Name} and in {_colorabiltyStateMachine.CurrentState.GetType().Name}");
            listener.OnStateEnter(this, _colorabiltyStateMachine.CurrentState);
            listener.OnStateEnter(this, _colorationStateMachine.CurrentState);
        }
    }

    // Здравствуй дорогой читатель, прибыл ты издалека?
    // Коль читаешь, значит хочешь знать ты всё наверняка?
    // Осуждаю тебя, путник, но однако помогу!
    // Метод сей приватным сделал чтоб решить одну беду:
    // Изменяя состоянье, ты, негодник озороной
    // Не хотел бы репу мучать, выбор делать сложный свой
    // И поэтому, чуть ниже, под собачий, волчий вой,
    // Непременно ты увидишь public метод золотой
    // За тебя он пусть решает - расслабляй свою мозгу!
    // Да благодари разраба, коль купаешься в жиру!  
    protected virtual void ChangeColorationState(PathElementState state)
    {
        _colorationStateMachine.ChangeState(state);
    }

    protected virtual void ChangeColorabilityState(PathElementState state)
    {
        _colorabiltyStateMachine.ChangeState(state);
    }

    public virtual void ChangeState(PathElementState state)
    {
        if (_colorabiltyStateMachine.TransitionIsValid(state))
        {
            ChangeColorabilityState(state);
        }
        else if (_colorationStateMachine.TransitionIsValid(state))
        {
            ChangeColorationState(state);
        }
    }

}
