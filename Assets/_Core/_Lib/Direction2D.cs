using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction2D
{
    public static List<Vector2> StandartDirectionsList => new List<Vector2>
    {
        Vector2.up, Vector2.right, Vector2.down, Vector2.left,
    };

    public static List<Vector2> FullDirectionsList => new List<Vector2>
    {
        Vector2.up, new Vector2(1, 1),
        Vector2.right, new Vector2(1, -1),
        Vector2.down, new Vector2(-1, -1),
        Vector2.left, new Vector2(-1, 1),
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

    public static Vector2 GetRandomStandartDirection() => StandartDirectionsList[Random.Range(0, StandartDirectionsList.Count)];
}