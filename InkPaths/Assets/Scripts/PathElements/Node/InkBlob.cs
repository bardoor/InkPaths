using UnityEngine;

public class InkBlob : Node
{
    void Start()
    {
        _stateMachine.Initialize(new PaintedState());
    }

    void Update()
    {
        if (_stateMachine.CurrentState != null)
        {
            _stateMachine.CurrentState.Update();
        }
    }
}
