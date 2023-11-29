using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // —корость прокрутки текстуры

    Material material; // —сылка на материал спрайта

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
    }
}
