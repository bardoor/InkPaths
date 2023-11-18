using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBuilder : IObservable
{
    private static PathBuilder _instance;

    private HashSet<InkPath> _paths { get; }

    private InkPath _currentPath;

    private HashSet<IObserver> _observers;

    private PathBuilder() {
        _paths = new HashSet<InkPath>();
        _currentPath = new InkPath();
        _observers = new HashSet<IObserver>();
    }

    public static PathBuilder Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PathBuilder();
            }
            return _instance;
        }
    }
    
    public void CancelBuilding()
    {
        // ќчистить активный путь
        _currentPath.Clear();

        // ”ведомить слушателей, что путь разрушилс€
        NotifyObservers(new CancelledBuildingPath());
    }

    public void AddElement(PathElement element)
    {
        // ƒобавить новый элемент в конец активного пути
        _currentPath.AddElement(element);

        // ќбновить состо€ние элементов, окружающих последний добавленный элемент
        element.UpdateElementsStatesAround();

        // ≈сли активный путь €вл€етс€ законченным путем...
        if (_currentPath.IsFinishedPath())
        {
            // ...поместить активный путь в список созданных путей
            _paths.Add(_currentPath.Copy());
            // ...очистить активный путь
            _currentPath.Clear();
            // ...уведомить слушателей, что создалс€ новый путь
            NotifyObservers(new FinishedBuildingPath());
        }
    }

    public void AddObserver(IObserver o) => _observers.Add(o);

    public void RemoveObserver(IObserver o) => _observers.Remove(o);

    public void NotifyObservers(IEvent e)
    {
        foreach (IObserver o in _observers)
        {
            o.Update(e);
        }
    }
}
