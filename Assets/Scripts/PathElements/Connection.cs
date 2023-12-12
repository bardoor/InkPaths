using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;
using System;
using System.Linq;

public class Connection : PathElement
{
    protected List<Node> _connectedNodes = new List<Node>();
    public List<Node> ConnectedNodes { get { return _connectedNodes; } }
    private void InitState()
    {
        _stateMachine.Initialize(this, new UnpaintableState());
    }

    private void Awake()
    {
        InitTextDebug(24);
        InitCollider();
        InitState();
    }

    public void AddNode(Node node)
    {
        if (!_connectedNodes.Contains(node))
        {
            _connectedNodes.Add(node);
        }
    }

    public void Update()
    {
        if (_connectedNodes.Count == 0)
        {
            // чето тут
        }
        else if (_connectedNodes[0].InkColor != PathElementState.NoColor && _connectedNodes[0].InkColor == _connectedNodes[1].InkColor)
        {
            ChangeState(new UnpaintableState());
        }
    }

    public override void SetPaintableAround(params PathElement[] ignoredElements)
    {
        foreach (var node in _connectedNodes)
        {
            if (ignoredElements.Contains(node))
            {
                Debug.Log($"Element {node.name} is ignored in Connection::SetPaintableAround");
                continue;
            }

            node.ChangeState(new PaintableState());
        }
    }

    public override void SetUnpaintableAround(params PathElement[] ignoredElements)
    {
        foreach (var node in _connectedNodes)
        {
            if (ignoredElements.Contains(node))
            {
                Debug.Log($"Element {node.name} is ignored in Connection::SetUnpaintableAround");
                continue;
            }

            node.ChangeState(new UnpaintableState());
        }
    }

    public override void HandleTouch()
    {
        // Состояние меняется только в случае если соединение находится в PaintableState
        if (!_stateMachine.ChangeState(new PaintedState()))
        {
            return;
        }

        // Устанавливаем все соединения кроме текущего (выбранного)
        // в UnpaintableState у точки, из которой мы пришли
        PathBuilder.Instance.Last.SetUnpaintableAround(this);
        // Добавляем себя в чернильный путь
        PathBuilder.Instance.AddElement(this);
        // Устанавливаем точку, в которую ведем палец, в PaintableState.
        // У начальной точки на этот момент состояние PaintedState,
        // поэтому этот метод её не затронет.
        // Если вторая точка чернильная, то этот метод так же ничего не изменит,
        // т.к. у этой точки уже состояние PaintableState
        SetPaintableAround();
    }
}
