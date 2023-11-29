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

    public bool IsFinishedPath()
    {
        return _currentPath.IsFinishedPath();
    }

    public void CancelBuilding()
    {
        Debug.Log("CANCELLED FUCKIN PATH");

        foreach (PathElement pathElement in _currentPath.PathElements)
        {
            pathElement.SetUnpaintableAround();
        }

        // Очистить активный путь
        _currentPath.Clear();

        // Уведомить слушателей, что путь разрушился
        NotifyObservers(new CancelledBuildingPath());
    }

    public void AddElement(PathElement element)
    {
        // Добавить новый элемент в конец активного пути
        _currentPath.AddElement(element);

        // Обновить состояние элементов, окружающих последний добавленный элемент
        // element.SetPaintableAround();

        // Если активный путь является законченным путем...
        if (IsFinishedPath())
        {
            Debug.Log("BUILT FUCKIN PATH");

            // ...поместить активный путь в список созданных путей
            _paths.Add(_currentPath.Copy());
            // ...очистить активный путь
            _currentPath.Clear();
            // ...уведомить слушателей, что создался новый путь
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
