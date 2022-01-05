using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Вспомогательный класс работающий с направлениями в 2D пространстве
/// </summary>
public static class Direction2D
{
    /// <summary>
    /// Список стандартных направлений
    /// </summary>
    /// <returns>
    /// 4 направления
    /// </returns>
    public static List<Vector2> StandartDirectionsList => new List<Vector2>
    {
        Vector2.up, Vector2.right, Vector2.down, Vector2.left,
    };

    /// <summary>
    /// Список всех направлений
    /// </summary>
    /// <returns>
    /// 8 направлений (включая диагональные)
    /// </returns>
    public static List<Vector2> FullDirectionsList => new List<Vector2>
    {
        Vector2.up, new Vector2(1, 1),
        Vector2.right, new Vector2(1, -1),
        Vector2.down, new Vector2(-1, -1),
        Vector2.left, new Vector2(-1, 1),
    };

    /// <summary>
    /// Получает позицию курсора
    /// </summary>
    public static Vector3 GetMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    /// <summary>
    /// Существует ли пустое пространство вокруг точки
    /// </summary>
    /// <param name="allPos">Коллекция со всеми доступными точками для проверки</param>
    /// <param name="center">Точка вокруг которой нужно проверить пространство</param>
    /// <param name="isFullDirList">Использовать ли 8 направлений </param>
    public static bool ExistEmptySpace(IEnumerable<Vector2Int> allPos, Vector2Int center, bool isFullDirList = false)
    {
        bool result;

        // Коллекция со всеми точками
        var posList = new List<Vector2Int>();
        posList.AddRange(allPos);

        // Коллекция с точками которые должны быть пустыми
        var emptyPos = new List<Vector2Int>();

        if(isFullDirList)
            FullDirectionsList.ForEach(s => emptyPos.Add(Vector2Int.RoundToInt(s) + center));
        else
            StandartDirectionsList.ForEach(s => emptyPos.Add(Vector2Int.RoundToInt(s) + center));

        // Совпадают ли пустые точки со списком точек
        result = posList.Contains(emptyPos[0]);
        for (int i = 1; i < emptyPos.Count; i++)
            result &= posList.Contains(emptyPos[i]);

        return result;
    }

    /// <summary>
    /// Получает направление от точки до цели
    /// </summary>
    /// <returns>
    /// Направление в координатах
    /// </returns>
    /// <param name="position">Точка от которой</param>
    /// <param name="targetPosition">Точка до которой (цель)</param>
    public static Vector2 GetDirectionTo(Vector2 position, Vector2 targetPosition)
    {
        float x = 0;
        float y = 0;

        // Проверка по Х
        if (position.x > targetPosition.x)
            x = -1;
        else if (position.x < targetPosition.x)
            x = 1;

        // Проверка по У
        if (position.y > targetPosition.y)
            y = -1;
        else if (position.y < targetPosition.y)
            y = 1;

        return new Vector2(x, y);
    }

    /// <summary>
    /// Получает рандомное направление из <see cref="StandartDirectionsList"/>
    /// </summary>
    /// <returns>
    /// Направление в координатах
    /// </returns>
    public static Vector2 GetRandomStandartDirection() => StandartDirectionsList[Random.Range(0, StandartDirectionsList.Count)];

    /// <summary>
    /// Получает рандомное направление из <see cref="FullDirectionsList"/>
    /// </summary>
    /// <returns>
    /// Направление в координатах
    /// </returns>
    public static Vector2 GetRandomFullDirection() => FullDirectionsList[Random.Range(0, FullDirectionsList.Count)];
}