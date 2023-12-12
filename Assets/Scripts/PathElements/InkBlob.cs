using UnityEngine;
using System;
using UnityEngine.UI;

public class InkBlob : Node
{
    private void InitState()
    {
        _stateMachine.Initialize(this, new PaintableState());
    }

    private void Awake()
    {
        InitTextDebug(200);
        InitCollider();
        InitState();
    }

    private void Start()
    {
        InitInkColor();
        InitConnections();
    }

    // InkBlob
    public override void HandleTouch()
    {
        if (!_stateMachine.ChangeState(new PaintedState()))
        {
            if (PathBuilder.Instance.Count > 0 && InkColor != PathBuilder.Instance.Last.InkColor) {
                PathBuilder.Instance.CancelBuilding();
            }
            
            return;
        }

        SetPaintableAround();
        PathBuilder.Instance.AddElement(this);
    }

    private void InitInkColor()
    {
        InkColor = gameObject.GetComponentInChildren<Image>().color;
    }
}
