using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction _touchPositionAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _touchPositionAction = _playerInput.actions.FindAction("TouchPosition");
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
        if (_touchPositionAction.WasPressedThisFrame())
        {
            Vector3 position = getPositionOnScreen(_touchPositionAction);
            Debug.Log("Pressed " + position);
        }

        else if (_touchPositionAction.WasPerformedThisFrame())
        {
            Vector3 position = getPositionOnScreen(_touchPositionAction);
            Debug.Log("Holding " + position);
        }

        else if (_touchPositionAction.WasReleasedThisFrame())
        {
            Vector3 position = getPositionOnScreen(_touchPositionAction);
            Debug.Log("Released " + position);
        }
    }
}
