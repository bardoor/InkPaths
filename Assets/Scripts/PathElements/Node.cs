using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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

        Debug.LogAssertion($"~~~~~~{gameObject.name}~~~~~~");
        _connections.ToList().ForEach(element => Debug.Log((element)));
    }

    private void Awake()
    {
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
                continue;
            }

            conn.ChangeState(new UnpaintableState());
        }
    }

    public override void HandleTouch()
    {
        if (!_stateMachine.ChangeState(new PaintedState()))
        {
            if (PathBuilder.Instance.Count > 0 && InkColor != PathBuilder.Instance.Last.InkColor)
            {
                PathBuilder.Instance.CancelBuilding();
            }

            return;
        }

        SetPaintableAround();
        PathBuilder.Instance.AddElement(this);
        //InkColor = PathBuilder.Instance.Last.InkColor;
    }
}
