using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : PathElement
{
    protected HashSet<Connection> _connections = new HashSet<Connection>();
    
    private void OnMouseDrag()
    {
        _stateMachine.ChangeState(new PaintedState());
    }
    void InitState()
    {
        _stateMachine.Initialize(this, new PaintableState());
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

    private void Awake()
    {
        InitState();
        InitConnections();
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

}
