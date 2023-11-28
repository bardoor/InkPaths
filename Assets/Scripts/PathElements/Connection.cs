using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : PathElement
{
    protected HashSet<Node> _connectedNodes = new HashSet<Node>();

    private void InitState()
    {
        _stateMachine.Initialize(this, PaintableState());
    }

    private void Awake()
    {
        InitState();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (_stateMachine.CurrentState != null)
        {
            _stateMachine.CurrentState.Update();
        }
    }

    public void AddNode(Node node)
    {
        _connectedNodes.Add(node);
    }
}
