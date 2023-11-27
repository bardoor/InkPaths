using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackround : MonoBehaviour
{

    /*[SerializeField] private float _speed;
    [SerializeField] private MeshRenderer _meshRenderer;

    private Vector2 _meshOffset;

    void Start()
    {
        _meshOffset = _meshRenderer.sharedMaterial.mainTextureOffset;
    }
    private void OnDisable()
    {
        _meshRenderer.sharedMaterial.mainTextureOffset = _meshOffset;
    }

    void Update()
    {
        var x = Mathf.Repeat(Time.time * _speed, 1);
        var offset = new Vector2(x, _meshOffset.y);
        _meshRenderer.sharedMaterial.mainTextureOffset = offset;
    }*/
}
