using UnityEngine;
using System;

public class InkBlob : Node
{
    private new readonly Type[] _validColorabilityStateCycle = new Type[] { typeof(PaintableState) };
    private new readonly Type[] _validColorationStateCycle = new Type[] { typeof(PaintedState) };

    private void InitState()
    {
        _colorabiltyStateMachine.SetValidStateTransitions(_validColorabilityStateCycle);
        _colorabiltyStateMachine.Initialize(this, new PaintableState());

        _colorationStateMachine.SetValidStateTransitions(_validColorationStateCycle);
        _colorationStateMachine.Initialize(this, new PaintedState());
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
