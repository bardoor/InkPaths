using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;

public class PathBuilder : IObservable
{
    private static PathBuilder _instance;

    private HashSet<InkPath> _paths { get; }

    private InkPath _currentPath;

    private HashSet<IObserver> _observers;

    public PathElement First { get => _currentPath.First; }

    public PathElement Last { get => _currentPath.Last; }

    public int Count { get => _currentPath.Count; }

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
        Debug.Log("PATHBUILDER: CANCELLED FUCKIN PATH");

        foreach (var pathElement in _currentPath.PathElements)
        {
            if (pathElement is InkBlob)
            {
                pathElement.ChangeState(new PaintableState());
            }
            else
            {
                pathElement.InkColor = PathElementState.NoColor;
                pathElement.ChangeState(new UnpaintableState());
            }
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

        // Если активный путь является законченным путем...
        if (IsFinishedPath())
        {
            Debug.Log("PATHBUILDER: BUILT FUCKIN PATH");

            // ...сделать каждый элемент пути незакрашиваемым
            foreach (var pathElement in _currentPath.PathElements)
            {
                Debug.Log($"{pathElement.name} IS IN PATH");
                pathElement.ChangeState(new UnpaintableState());
            }
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

    public void PrintAllElements()
    {
        _currentPath.PathElements.ForEach(element => Debug.Log(element.gameObject.name));
    }
}
