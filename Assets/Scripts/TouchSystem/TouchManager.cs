using UnityEngine.InputSystem;
using UnityEngine;
using System.ComponentModel.Design;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using static UnityEngine.ParticleSystem;

public class TouchManager : MonoBehaviour
{
    private TouchAction _action;

    private bool _isTouching = false;

    private PathElement _lastPressedPathElement = null;

    private double _holdTime = 0;

    private double _MIN_HOLD_DURATION = 3; // в секундах

    private void Awake()
    {
        _action = new();
    }

    private void OnEnable()
    {
        _action.Enable();

        _action.Touch.TouchPress.started += ctx => StartTouch(ctx);
        _action.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void OnDisable()
    {
        _action.Touch.TouchPress.started -= ctx => StartTouch(ctx);
        _action.Touch.TouchPress.canceled -= ctx => EndTouch(ctx);

        _action.Disable();
    }

    private RaycastHit2D[] GetCurrentRaycastHitsAt(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);
        return hits;
    }

    private List<PathElement> GetCurrentPressedPathElementsAt(Vector2 position)
    {
        RaycastHit2D[] hits = GetCurrentRaycastHitsAt(position);

        List<PathElement> hitElements = new();
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null)
            {
                continue;
            }

            if (hit.collider.TryGetComponent(out PathElement pathElement))
            {
                hitElements.Add(pathElement);
            }
        }

        return hitElements;
    }

    private PathElement GetCurrentPressedPathElementAt(Vector2 position)
    {
        List<PathElement> pressed = GetCurrentPressedPathElementsAt(position);
        return pressed.Count == 0 ? null : pressed[0];
    }

    private Vector2 GetCurrentPosition()
    {
        return _action.Touch.TouchPosition.ReadValue<Vector2>();
    }

    private void StartTouch(InputAction.CallbackContext ctx)
    {
        StartTouching();
        SaveCurrentPressedPathElement();
        ZeroHoldTime();

        PathElement pressed = GetCurrentPressedPathElementAt(GetCurrentPosition());
        if (pressed != null && pressed is InkBlob blob)
        {
            blob.HandleTouch();
        }
    }

    private void EndTouch(InputAction.CallbackContext ctx)
    {
        StopTouching();
        ForgetLastPressedPathElement();
        ZeroHoldTime();

        if (PathBuilder.Instance.Count > 0)
        {
            PathBuilder.Instance.CancelBuilding();
        }
    }

    private void Update()
    {
        if (_isTouching)
        {
            _holdTime += Time.deltaTime;

            PathElement pressed = GetCurrentPressedPathElementAt(GetCurrentPosition());
            if (pressed != null)
            {
                if (pressed != _lastPressedPathElement)
                {
                    ForgetLastPressedPathElement();
                }
                else if (pressed is InkBlob
                    && PathBuilder.Instance.BelongsToAnyCompletePath(pressed)
                    && GetHoldTime() >= _MIN_HOLD_DURATION)
                {
                    PathBuilder.Instance.DestroyPathThatHas(pressed);
                    StopTouching();
                    return;
                }

                pressed.HandleTouch();
            }
        }
    }

    public void StartTouching() => _isTouching = true;

    public void StopTouching() {
        _isTouching = false;
        ZeroHoldTime();
    }

    private void SaveCurrentPressedPathElement() => _lastPressedPathElement = GetCurrentPressedPathElementAt(GetCurrentPosition());

    private void ForgetLastPressedPathElement() => _lastPressedPathElement = null;

    private void ZeroHoldTime() => _holdTime = 0;

    private double GetHoldTime() => _holdTime;
}
