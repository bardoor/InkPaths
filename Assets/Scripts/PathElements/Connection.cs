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
                continue;
            }

            node.ChangeState(new UnpaintableState());
        }
    }

    public override void HandleTouch()
    {
        if (!_stateMachine.ChangeState(new PaintedState()))
        {
            return;
        }

        SetPaintableAround();
        //InkColor = PathBuilder.Instance.Last.InkColor;

        Debug.Log("FUCK I M GONNA BE ADDED");
        PathBuilder.Instance.Last.SetUnpaintableAround(this);
        PathBuilder.Instance.AddElement(this);
    }
}
