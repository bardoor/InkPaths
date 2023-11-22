using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateChangeObservable
{
    public void Subscribe(IStateChangeListener listener);
    public void Unsubscribe(IStateChangeListener listener);
}
