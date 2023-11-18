using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Чернильный путь.
/// </summary>
public class InkPath
{
    /// <summary>
    /// Список элементов, из которых состоит данный путь.
    /// </summary>
    private List<PathElement> _pathElements;

    /// <summary>
    /// Элементы в пути.
    /// </summary>
    public List<PathElement> PathElements { get => new List<PathElement>(_pathElements); }

    /// <summary>
    /// Число элементов в пути.
    /// </summary>
    public int Count { get => _pathElements.Count; }

    /// <summary>
    /// Конструктор, создающий пустой путь.
    /// </summary>
    public InkPath() => _pathElements = new List<PathElement>();
    
    /// <summary>
    /// Конструктор, создающий путь на основе списка элементов пути.
    /// </summary>
    /// <param name="elements">Список элементов пути.</param>
    public InkPath(List<PathElement> elements) => _pathElements = new List<PathElement>(elements);

    /// <summary>
    /// Добавляет новый элемент в путь.
    /// </summary>
    /// <param name="element">Новый элемент.</param>
    public void AddElement(PathElement element) => _pathElements.Add(element);

    /// <summary>
    /// Удаляет все элементы из пути.
    /// </summary>
    public void Clear() => _pathElements.Clear();

    /// <summary>
    /// Копирует путь.
    /// </summary>
    /// <returns>Копия пути.</returns>
    public InkPath Copy() => new InkPath(_pathElements);

    /// <summary>
    /// Проверяет, что путь является завершенным: начало и конец пути это чернильные точки.
    /// </summary>
    /// <returns>Признак, завершен ли путь.</returns>
    public bool IsFinishedPath()
    {
        if (this.Count < 3)
        {
            return false;
        }
        
        return (_pathElements[0] is InkBlob) && (_pathElements[^1] is InkBlob);
    }
}
