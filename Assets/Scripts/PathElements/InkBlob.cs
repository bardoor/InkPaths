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
        // Если мы пытаемся провести путь в чернильную точку не того же цвета,
        // что и прошлый элемент пути, прекратить создание пути
        if (PathBuilder.Instance.CurrentPathElementsCount > 0 && InkColor != PathBuilder.Instance.First.InkColor)
        {
            PathBuilder.Instance.CancelBuildingCurrentPath();
            return;
        }

        if (_stateMachine.ChangeState(new PaintedState()))
        {
            // Не нужно устанавалить окружающие соединения в Paintable,
            // если мы пришли в конечную чернильную точку
            if (PathBuilder.Instance.CurrentPathElementsCount == 0)
            {
                SetPaintableAround();
            }

            PathBuilder.Instance.AddElement(this);
        }
    }

    private void InitInkColor()
    {
        Image[] images = gameObject.GetComponentsInChildren<Image>(true);
        if (images.Length > 1)
        {
            InkColor = images[1].color;
        }
    }
}
