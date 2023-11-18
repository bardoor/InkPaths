using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithScreenResolution : MonoBehaviour
{
    private Canvas canvas;

    void Start()
    {
        // Получаем компонент Canvas
        canvas = GetComponent<Canvas>();

        // Подписываемся на событие изменения разрешения
        if (canvas != null)
        {
            canvas.scaleFactor = CalculateScaleFactor();
            UpdateScale();
        }
    }

    void Update()
    {
        // Проверяем, изменилось ли разрешение
        if (canvas != null && canvas.scaleFactor != CalculateScaleFactor())
        {
            canvas.scaleFactor = CalculateScaleFactor();
            UpdateScale();
        }
    }

    float CalculateScaleFactor()
    {
        // Рассчитываем новый scaleFactor, например, исходя из ширины экрана
        return Screen.width / 1920f; // 1920f - ширина, при которой объекты имеют оригинальный масштаб
    }

    void UpdateScale()
    {
        // Применяем масштаб ко всем дочерним объектам Canvas
        foreach (Transform child in canvas.transform)
        {
            child.localScale = Vector3.one;
        }
    }
}
