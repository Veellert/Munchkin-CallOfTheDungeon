using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction2D
{
    public static List<Vector2Int> StandartDirectionsList => new List<Vector2Int>
    {
        Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left,
    };

    public static List<Vector2Int> FullDirectionsList => new List<Vector2Int>
    {
        Vector2Int.up, new Vector2Int(1, 1),
        Vector2Int.right, new Vector2Int(1, -1),
        Vector2Int.down, new Vector2Int(-1, -1),
        Vector2Int.left, new Vector2Int(-1, 1),
    };

    public static Vector2 GetDirectionTo(Vector2 position, Vector2 targetPosition)
    {
        float x = 0;
        float y = 0;

        if (position.x > targetPosition.x)
            x = -1;
        else if (position.x < targetPosition.x)
            x = 1;

        if (position.y > targetPosition.y)
            y = -1;
        else if (position.y < targetPosition.y)
            y = 1;

        return new Vector2(x, y);
    }

    public static Vector2Int GetRandomStandartDirection() => StandartDirectionsList[Random.Range(0, StandartDirectionsList.Count)];
}