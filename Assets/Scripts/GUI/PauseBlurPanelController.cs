using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBlurPanelController : MonoBehaviour
{
    private GameObject mainGamePanel;

    public GameObject test;
    void Start()
    {
        // Получаем текущий индекс объекта в иерархии
        int currentIndex = transform.GetSiblingIndex();

        // Задаем новый индекс с учетом сдвига
        int newIndex = currentIndex + 2; // Например, сдвиг на 2 позиции вниз

        // Перемещаем объект по иерархии на указанное количество позиций
        transform.SetSiblingIndex(newIndex);
    }
}
