using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;

public class Connection : PathElement
{
    protected List<Node> _connectedNodes = new List<Node>();
    public List<Node> ConnectedNodes { get { return _connectedNodes; } }

    private void InitState()
    {
        _stateMachine.Initialize(this, new PaintableState());
    }

    private void Awake()
    {
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
        if (_connectedNodes[0].InkColor != PathElementState.NoColor && _connectedNodes[0].InkColor == _connectedNodes[1].InkColor)
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
        _stateMachine.CurrentState.HandleTouch();
    }
}
