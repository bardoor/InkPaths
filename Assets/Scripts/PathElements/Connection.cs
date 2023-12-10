using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;
using System;

public class Connection : PathElement
{
    protected List<Node> _connectedNodes = new List<Node>();
    public List<Node> ConnectedNodes { get { return _connectedNodes; } }
    protected readonly Type[] _validColorabilityStateTransitions = new Type[] { typeof(UnpaintableState), typeof(PaintableState) };
    protected readonly Type[] _validColorationStateTransitions = new Type[] { typeof(UnpaintedState), typeof(PaintedState) };

    private void InitState()
    {
        _colorabiltyStateMachine.SetValidStateTransitions(_validColorabilityStateTransitions);
        _colorabiltyStateMachine.Initialize(this, new UnpaintableState());
        
        _colorationStateMachine.SetValidStateTransitions(_validColorationStateTransitions);
        _colorationStateMachine.Initialize(this, new UnpaintedState());
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

    public override void SetPaintableAround()
    {
        foreach (Node node in _connectedNodes)
        {
            node.ChangeState(new PaintableState());
        }
    }

    public override void SetUnpaintableAround()
    {
        foreach (Node node in _connectedNodes)
        {
            node.ChangeState(new UnpaintableState());
        }
    }

    public override void HandleTouch()
    {
        PathBuilder.Instance.AddElement(this);
        _colorabiltyStateMachine.CurrentState.HandleTouch();
        _colorationStateMachine.CurrentState.HandleTouch();
    }
}
