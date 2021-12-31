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

    public static bool ExistEmptySpace(IEnumerable<Vector2Int> allPos, Vector2Int center, bool isFullDirList = false)
    {
        bool result;

        var posList = new List<Vector2Int>();
        posList.AddRange(allPos);

        var emptyPos = new List<Vector2Int>();

        if(isFullDirList)
            FullDirectionsList.ForEach(s => emptyPos.Add(Vector2Int.RoundToInt(s) + center));
        else
            StandartDirectionsList.ForEach(s => emptyPos.Add(Vector2Int.RoundToInt(s) + center));

        result = posList.Contains(emptyPos[0]);
        for (int i = 1; i < emptyPos.Count; i++)
            result &= posList.Contains(emptyPos[i]);

        return result;
    }
    
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