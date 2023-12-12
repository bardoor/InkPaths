using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;

public class Node : PathElement
{
    protected HashSet<Connection> _connections = new HashSet<Connection>();

    private void InitState()
    {
        _stateMachine.Initialize(this, new UnpaintableState());
    }

    public override bool ChangeState(PathElementState newState)
    {
        return _stateMachine.ChangeState(newState);
    }

    protected void InitConnections()
    {
        Collider2D collider = GetComponent<Collider2D>();
        List<Collider2D> overlappedColliders = new List<Collider2D>();
        collider.Overlap(overlappedColliders);

        foreach (Collider2D otherCollider in overlappedColliders)
        {
            Connection interconnection = otherCollider.GetComponent<Connection>();
            _connections.Add(interconnection);
            interconnection.AddNode(GetComponent<Node>());
        }

        // Debug.LogAssertion($"~~~~~~{gameObject.name}~~~~~~");
        // _connections.ToList().ForEach(element => Debug.Log((element)));
    }

    private void Awake()
    {
        InitTextDebug(200);
        InitCollider();
        InitState();
    }

    private void Start()
    {
        InitConnections();
    }

    public override void SetPaintableAround(params PathElement[] ignoredElements)
    {
        foreach (var conn in _connections)
        {
            if (ignoredElements.Contains(conn))
            {
                Debug.Log($"Element {conn.name} is ignored in Node::SetPaintableAround");
                continue;
            }

            conn.ChangeState(new PaintableState());
        }
    }

    public override void SetUnpaintableAround(params PathElement[] ignoredElements)
    {
        foreach (var conn in _connections)
        {
            if (ignoredElements.Contains(conn))
            {
                Debug.Log($"Element {conn.name} is ignored in Node::SetUnpaintableAround");
                continue;
            }

            conn.ChangeState(new UnpaintableState());
        }
    }

    public override void HandleTouch()
    {
        // Если мы пытаемся провести путь в чернильную точку не того же цвета,
        // что и прошлый элемент пути, прекратить создание пути
        if (PathBuilder.Instance.Count > 0 && InkColor != PathElementState.NoColor && InkColor != PathBuilder.Instance.First.InkColor)
        {
            PathBuilder.Instance.CancelBuilding();
            return;
        }


        if (_stateMachine.ChangeState(new PaintedState()))
        {
            // Добавляем себя в чернильный путь
            PathBuilder.Instance.AddElement(this);
            // Устанавливаем все соединения вокруг себя в PaintableState
            SetPaintableAround();
        }
    }
}
