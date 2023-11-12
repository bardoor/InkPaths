using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : PathElement
{
    [SerializeField] protected List<Node> _connectedNodes = new List<Node>();

    void Start()
    {
        _stateMachine.Initialize(new UnpaintedState());
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
