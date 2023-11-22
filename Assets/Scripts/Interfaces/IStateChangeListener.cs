using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateChangeListener
{
    void OnStateEnter(IStateChangeObservable element, State newState);
}
