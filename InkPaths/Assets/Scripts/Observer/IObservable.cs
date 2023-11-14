using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable
{
    void AddObserver(IObservable o);

    void RemoveObserver(IObservable o);

    void NotifyObservers();
}
