using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBlurPanelController : MonoBehaviour
{
    private GameObject mainGamePanel;

    public GameObject test;
    void Start()
    {
        // �������� ������� ������ ������� � ��������
        int currentIndex = transform.GetSiblingIndex();

        // ������ ����� ������ � ������ ������
        int newIndex = currentIndex + 2; // ��������, ����� �� 2 ������� ����

        // ���������� ������ �� �������� �� ��������� ���������� �������
        transform.SetSiblingIndex(newIndex);
    }
}
