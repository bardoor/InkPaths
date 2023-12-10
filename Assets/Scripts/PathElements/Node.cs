﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Node : PathElement
{
    protected HashSet<Connection> _connections = new HashSet<Connection>();
    protected readonly Type[] _validColorabilityStateCycle = new Type[] { typeof(UnpaintableState), typeof(PaintableState) };
    protected readonly Type[] _validColorationStateCycle = new Type[] { typeof(UnpaintedState), typeof(PaintedState) };

    private void InitState()
    {
        _colorabiltyStateMachine.SetValidStateTransitions(_validColorabilityStateCycle);
        _colorabiltyStateMachine.Initialize(this, new UnpaintableState());

        _colorationStateMachine.SetValidStateTransitions(_validColorationStateCycle);
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

        Debug.LogAssertion($"~~~~~~{gameObject.name}~~~~~~");
        _connections.ToList().ForEach(element => Debug.Log((element)));
    }

    private void Awake()
    {
        InitCollider();
        InitState();
    }

    private void Start()
    {
        InitConnections();
    }

}
