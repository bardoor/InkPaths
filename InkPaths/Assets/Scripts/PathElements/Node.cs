using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : PathElement
{
    [SerializeField] protected List<Connection> _connections = new List<Connection>();
    private void OnMouseDrag()
    {
        _stateMachine.ChangeState(new PaintedState());
    }

    void Start()
    {
        InitState();
        InitConnections();
    }

    void InitState()
    {
        _stateMachine.Initialize(new UnpaintedState());
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
    }


    void Update()
    {
        if (_stateMachine.CurrentState != null)
        {
            _stateMachine.CurrentState.Update();
        }
    }

}
