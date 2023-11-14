using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private bool frameOnStart = true;

    [SerializeField]
    private bool frameOnUpdate = true;

    [SerializeField]
    private Vector3 _padding = Vector3.zero;

    [SerializeField]
    private Vector3 _centerOffset;

    [SerializeField]
    private float _cameraScale = 0.5f;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Start()
    {
        if (frameOnStart)
            FrameObjects(FindObjectsByType<GameObject>(FindObjectsSortMode.None));
    }

    private void FrameObjects(GameObject[] framedObjects)
    {
        if (framedObjects.Length == 0)
        {
            return;
        }

        Bounds bounds = new Bounds();
        
        foreach (GameObject obj in framedObjects)
        {
            Collider2D collider = obj.GetComponent<Collider2D>();
            if (collider != null && obj.CompareTag("PathElement"))
            {
                bounds.Encapsulate(collider.bounds);
                Debug.Log("/--CAMERA--/\nIncluding " + obj.name + " with coords: " + collider.bounds);
            }
                
        }
        
        bounds.Expand(_padding);

        float vertical = bounds.size.y;
        float horizontal = bounds.size.x * _camera.pixelHeight / _camera.pixelWidth;
        float size = Mathf.Max(horizontal, vertical) * _cameraScale;

        Vector3 center = bounds.center + _centerOffset;
        center.z = _camera.transform.position.z;

        _camera.transform.position = center;
        _camera.orthographicSize = size;
    }

    private void Update()
    {
        if (frameOnUpdate)
            FrameObjects(FindObjectsByType<GameObject>(FindObjectsSortMode.None));
    }

}
