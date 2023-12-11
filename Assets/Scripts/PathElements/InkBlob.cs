using UnityEngine;
using System;

public class InkBlob : Node
{
    private void InitState()
    {
        _stateMachine.Initialize(this, new PaintableState());
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


    public override void SetPaintableAround()
    {
        foreach (Connection conn in _connections)
        {
            conn.ChangeState(new PaintableState());
        }
    }

    public override void SetUnpaintableAround()
    {
        foreach (Connection conn in _connections)
        {
            conn.ChangeState(new UnpaintableState());
        }
    }
}
