using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : PathElement
{
    [SerializeField] protected Node[] connectedNodes = new Node[2];
    private readonly StateMachine _stateMachine = new StateMachine();

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
}
