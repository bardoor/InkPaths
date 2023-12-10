using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : PathElement
{
    protected HashSet<Connection> _connections = new HashSet<Connection>();

    void InitState()
    {
        _colorabiltyStateMachine.Initialize(this, new UnpaintableState());
        _colorationStateMachine.Initialize(this, new UnpaintedState());
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

    public override void SetPaintableAround()
    {
        throw new System.NotImplementedException();
    }

    public override void SetUnpaintableAround()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleTouch()
    {
        throw new System.NotImplementedException();
    }
}
