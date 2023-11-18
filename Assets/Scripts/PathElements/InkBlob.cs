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
