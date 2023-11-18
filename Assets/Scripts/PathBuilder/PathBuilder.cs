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
        // �������� �������� ����
        _currentPath.Clear();

        // ��������� ����������, ��� ���� ����������
        NotifyObservers(new CancelledBuildingPath());
    }

    public void AddElement(PathElement element)
    {
        // �������� ����� ������� � ����� ��������� ����
        _currentPath.AddElement(element);

        // �������� ��������� ���������, ���������� ��������� ����������� �������
        element.UpdateElementsStatesAround();

        // ���� �������� ���� �������� ����������� �����...
        if (_currentPath.IsFinishedPath())
        {
            // ...��������� �������� ���� � ������ ��������� �����
            _paths.Add(_currentPath.Copy());
            // ...�������� �������� ����
            _currentPath.Clear();
            // ...��������� ����������, ��� �������� ����� ����
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
