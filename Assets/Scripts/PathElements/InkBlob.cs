using UnityEngine;

public class InkBlob : Node
{
    void Start()
    {
        InitState();
        InitConnections();
    }

    void InitState()
    {
        _stateMachine.Initialize(this, new PaintableState());
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
