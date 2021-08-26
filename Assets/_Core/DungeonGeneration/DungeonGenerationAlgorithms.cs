using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DungeonGenerationAlgorithms
{
    public static HashSet<Vector2Int> GetRandomFloorPositions(Vector2Int startPosition, int pathLength)
    {
        var roomPosotions = new HashSet<Vector2Int>();
        roomPosotions.Add(startPosition);

        var prevPosition = startPosition;

        for (int i = 0; i < pathLength; i++)
        {
            var randomPosition = prevPosition + Direction2D.GetRandomStandartDirection();
            roomPosotions.Add(randomPosition);
            prevPosition = randomPosition;
        }

        return roomPosotions;
    }

    public static List<Vector2Int> GetRandomCorridorPositions(Vector2Int startPosition, int corridorLength)
    {
        var corridorPositions = new List<Vector2Int>();

        var direction = Direction2D.GetRandomStandartDirection();
        var currentPosition = startPosition;
        corridorPositions.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridorPositions.Add(currentPosition);
        }

        return corridorPositions;
    }
    
    public static List<BoundsInt> GetSpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        var roomsQueue = new Queue<BoundsInt>();
        var roomsList = new List<BoundsInt>();

        roomsQueue.Enqueue(spaceToSplit);

        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y > minHeight && room.size.x > minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                        SplitHorizontaly(minHeight, roomsQueue, room);
                    else if (room.size.x >= minWidth * 2)
                        SplitVerticaly(minWidth, roomsQueue, room);
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                        roomsList.Add(room);
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                        SplitVerticaly(minWidth, roomsQueue, room);
                    else if(room.size.y >= minHeight * 2)
                        SplitHorizontaly(minHeight, roomsQueue, room);
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                        roomsList.Add(room);
                }
            }
        }

        return roomsList;
    }

    private static void SplitVerticaly(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        var room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        var room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontaly(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        var room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        var room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}