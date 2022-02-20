using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Вспомогательный класс работающий с направлениями и 2D пространством
/// </summary>
public static class Direction2D
{
    /// <summary>
    /// Список стандартных направлений
    /// </summary>
    /// <returns>
    /// 4 направления
    /// </returns>
    public static List<Vector2> StandartDirectionList => new List<Vector2>
    {
        Vector2.up, Vector2.right, Vector2.down, Vector2.left,
    };
    /// <returns>
    /// Рандомное направление из<see cref="StandartDirectionList"/>
    /// </returns>
    public static Vector2 GetRandomStandartDirection() => StandartDirectionList[Random.Range(0, StandartDirectionList.Count)];

    /// <summary>
    /// Список всех направлений
    /// </summary>
    /// <returns>
    /// 8 направлений (включая диагонали)
    /// </returns>
    public static List<Vector2> FullDirectionList => new List<Vector2>
    {
        Vector2.up, new Vector2(1, 1),
        Vector2.right, new Vector2(1, -1),
        Vector2.down, new Vector2(-1, -1),
        Vector2.left, new Vector2(-1, 1),
    };
    /// <returns>
    /// Рандомное направление из <see cref="FullDirectionList"/>
    /// </returns>
    public static Vector2 GetRandomFullDirection() => FullDirectionList[Random.Range(0, FullDirectionList.Count)];

    /// <returns>
    /// Максимальная точка из списка координат
    /// </returns>
    /// <param name="positions">Список координат</param>
    public static Vector2Int CompareMax(List<Vector2Int> positions)
    {
        var currentMax = new Vector2Int(int.MinValue, int.MinValue);

        positions.ForEach(max =>
        {
            currentMax.x = CompareValue(max.x, currentMax.x);
            currentMax.y = CompareValue(max.y, currentMax.y);
        });

        return currentMax;

        static int CompareValue(int newValue, int currentValue)
        {
            if (newValue > currentValue)
                return newValue;

            return currentValue;
        }
    }
    /// <returns>
    /// Минимальная точка из списка координат
    /// </returns>
    /// <param name="positions">Список координат</param>
    public static Vector2Int CompareMin(List<Vector2Int> positions)
    {
        var currentMin = new Vector2Int(int.MaxValue, int.MaxValue);

        positions.ForEach(min =>
        {
            currentMin.x = CompareValue(min.x, currentMin.x);
            currentMin.y = CompareValue(min.y, currentMin.y);
        });

        return currentMin;

        static int CompareValue(int newValue, int currentValue)
        {
            if (newValue < currentValue)
                return newValue;

            return currentValue;
        }
    }

    /// <summary>
    /// Существует ли пустое пространство вокруг точки
    /// </summary>
    /// <param name="allPos">Коллекция со всеми доступными точками для проверки</param>
    /// <param name="center">Точка вокруг которой нужно проверить пространство</param>
    /// <param name="isFullDirList">Использовать ли 8 направлений </param>
    public static bool ExistEmptySpace(IEnumerable<Vector2Int> allPos, Vector2Int center, bool isFullDirList = false)
    {
        bool result = true;

        var posList = new List<Vector2Int>(allPos);
        var emptyPos = GetDirectionsByCenter(isFullDirList, center);
        
        // Совпадают ли пустые точки со списком точек
        for (int i = 0; i < emptyPos.Count; i++)
            result &= posList.Contains(emptyPos[i]);

        return result;

        static List<Vector2Int> GetDirectionsByCenter(bool isFullDirectionList, Vector2Int center)
        {
            var result = new List<Vector2Int>();

            if (isFullDirectionList)
                FullDirectionList.ForEach(s => result.Add(Vector2Int.RoundToInt(s) + center));
            else
                StandartDirectionList.ForEach(s => result.Add(Vector2Int.RoundToInt(s) + center));

            return result;
        }
    }

    /// <returns>
    /// Направление от точки до цели
    /// </returns>
    /// <param name="position">Точка от которой</param>
    /// <param name="targetPosition">Точка до которой (цель)</param>
    public static Vector2 GetDirectionTo(Vector2 position, Vector2 targetPosition)
    {
        return new Vector2(CompareValue(position.x, targetPosition.x), CompareValue(position.y, targetPosition.y));

        static float CompareValue(float val1, float val2)
        {
            if (val1 > val2)
                return -1;
            else if (val1 < val2)
                return 1;
            return 0;
        }
    }

    /// <returns>
    /// Направление от игрока до курсора
    /// </returns>
    public static Vector2 GetMouseDirection()
    {
        return GetDirectionToPlayer(InputObserver.GetMousePosition());
    }

    /// <returns>
    /// Направление от точки до игрока
    /// </returns>
    /// <param name="position">Точка от которой</param>
    public static Vector2 GetDirectionToPlayer(Vector2 position)
    {
        return (position - (Vector2)Player.Instance.transform.position).normalized;
    }
    /// <returns>
    /// Направление от игрока до точки
    /// </returns>
    /// <param name="position">Точка до которой</param>
    public static Vector2 GetPlayerDirectionFrom(Vector2 position)
    {
        return ((Vector2)Player.Instance.transform.position - position).normalized;
    }
}