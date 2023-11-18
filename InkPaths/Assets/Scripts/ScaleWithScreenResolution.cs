using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithScreenResolution : MonoBehaviour
{
    private Canvas canvas;

    void Start()
    {
        // �������� ��������� Canvas
        canvas = GetComponent<Canvas>();

        // ������������� �� ������� ��������� ����������
        if (canvas != null)
        {
            canvas.scaleFactor = CalculateScaleFactor();
            UpdateScale();
        }
    }

    void Update()
    {
        // ���������, ���������� �� ����������
        if (canvas != null && canvas.scaleFactor != CalculateScaleFactor())
        {
            canvas.scaleFactor = CalculateScaleFactor();
            UpdateScale();
        }
    }

    float CalculateScaleFactor()
    {
        // ������������ ����� scaleFactor, ��������, ������ �� ������ ������
        return Screen.width / 1920f; // 1920f - ������, ��� ������� ������� ����� ������������ �������
    }

    void UpdateScale()
    {
        // ��������� ������� �� ���� �������� �������� Canvas
        foreach (Transform child in canvas.transform)
        {
            child.localScale = Vector3.one;
        }
    }
}
