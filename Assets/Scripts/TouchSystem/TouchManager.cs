using UnityEngine.InputSystem;
using UnityEngine;
using System.ComponentModel.Design;

public class TouchManager : MonoBehaviour
{
    private TouchAction _action;

    private bool _isTouching = false;

    private void Awake()
    {
        _action = new TouchAction();
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

    private RaycastHit2D[] GetCurrentRaycastHits()
    {
        Vector2 position = _action.Touch.TouchPosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);
        return hits;
    }

    private void StartTouch(InputAction.CallbackContext ctx)
    {
        _isTouching = true;

        RaycastHit2D[] hits = GetCurrentRaycastHits();
        // Судя по всему в меню нельзя нажать куда-нибудь, чтобы задеть 2 объекта,
        // в самой игре 2 объекта задевается при нажатии на мост и соединение, но
        // такой случай нам тоже не подходит
        if (hits.Length == 1)
        {
            if (hits[0].collider.TryGetComponent<InkBlob>(out InkBlob blob)) 
            {
                Debug.Log("Ink blob here!!!!");
                blob.HandleTouch();
            }
        }
    }

    private void EndTouch(InputAction.CallbackContext ctx)
    {
        if (!PathBuilder.Instance.IsFinishedPath())
        {
            PathBuilder.Instance.CancelBuilding();
        }

        _isTouching = false;
    }

    private void Update()
    {
        if (_isTouching)
        {
            RaycastHit2D[] hits = GetCurrentRaycastHits();

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider == null) continue;

                if (hit.collider.TryGetComponent<PathElement>(out PathElement pathElement))
                {
                    pathElement.HandleTouch();
                }
            }
        }
    }
}
