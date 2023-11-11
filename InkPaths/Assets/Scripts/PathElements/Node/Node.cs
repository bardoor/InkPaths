using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : PathElement
{
    [SerializeField] protected Connection[] _connections = new Connection[4];

    private void OnMouseDrag()
    {
        _stateMachine.ChangeState(new PaintedState());
    }

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
