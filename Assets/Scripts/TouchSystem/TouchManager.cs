using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.AdaptivePerformance;

public class TouchManager : MonoBehaviour
{
    private TouchAction _action;

    private void Awake()
    {
        _action = new TouchAction();
    }

    private void OnEnable()
    {
        _action.Enable();
        _action.Touch.TouchPress.started += ctx => StartTouch(ctx);
        _action.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
        _action.Touch.TouchHold.performed += ctx => HoldTouch(ctx);
    }

    private void OnDisable()
    {
        _action.Touch.TouchPress.started -= ctx => StartTouch(ctx);
        _action.Touch.TouchPress.canceled -= ctx => EndTouch(ctx);
        _action.Touch.TouchHold.performed -= ctx => HoldTouch(ctx);
        _action.Disable();
    }

    private void StartTouch(InputAction.CallbackContext ctx)
    {
        Debug.Log("Touch started " + _action.Touch.TouchPosition.ReadValue<Vector2>());
    }

    private void EndTouch(InputAction.CallbackContext ctx)
    {
        Debug.Log("Touch ended");
    }

    private void HoldTouch(InputAction.CallbackContext ctx)
    {
        Debug.Log("HOLDING");
    }

    /*
    private PlayerInput _playerInput;

    private InputAction _touchPressAction;

    private InputAction _touchPositionAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _touchPressAction = _playerInput.actions.FindAction("TouchPress");
        _touchPositionAction = _playerInput.actions.FindAction("TouchPosition");
    }

    private void OnEnable()
    {
        _touchPositionAction.performed += TouchPressed;
        _touchPositionAction.canceled += TouchUnpressed;
    }

    private void OnDisable()
    {
        _touchPositionAction.performed -= TouchPressed;
        _touchPositionAction.canceled -= TouchUnpressed;
    }

    private void TouchUnpressed(InputAction.CallbackContext context)
    {
        Debug.Log("Finished pressing");
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Called TouchPressed");

        Ray ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null) continue;

            hit.collider.TryGetComponent(out PathElement pathElement);
            //pathElement.onPressed();
        }
    }

    private Vector3 getPositionOnScreen(InputAction action)
    {
        float originalZ = Camera.main.transform.position.z;
        Vector3 position = Camera.main.ScreenToWorldPoint(action.ReadValue<Vector2>());
        position.z = originalZ;
        return position;
    }

    private void Update()
    {
        
    }
    */
}
