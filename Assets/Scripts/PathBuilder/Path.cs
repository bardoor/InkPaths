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
    /// Первый элемент пути (null, если путь пустой).
    /// </summary>
    public PathElement First { get => Count > 0 ? _pathElements[0] : null; }

    /// <summary>
    /// Последний элемент пути (null, если путь пустой).
    /// </summary>
    public PathElement Last { get => Count > 0 ? _pathElements[^1] : null; }

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
    public void AddElement(PathElement element)
    {
        if (_pathElements.Contains(element))
        {
            return;
        }
        _pathElements.Add(element);
    }

    /// <summary>
    /// Проверяет, принадлежит ли элемент пути.
    /// </summary>
    /// <param name="element">Элемент, принадлежность которого проверяется</param>
    /// <returns>Признак, указывающий, принадлежит ли элемент пути</returns>
    public bool BelongsToPath(PathElement element) => _pathElements.Contains(element);

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
