using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������������� ����� ���������� � ������������� � 2D ������������
/// </summary>
public static class Direction2D
{
    /// <summary>
    /// ������ ����������� �����������
    /// </summary>
    /// <returns>
    /// 4 �����������
    /// </returns>
    public static List<Vector2> StandartDirectionsList => new List<Vector2>
    {
        Vector2.up, Vector2.right, Vector2.down, Vector2.left,
    };

    /// <summary>
    /// ������ ���� �����������
    /// </summary>
    /// <returns>
    /// 8 ����������� (������� ������������)
    /// </returns>
    public static List<Vector2> FullDirectionsList => new List<Vector2>
    {
        Vector2.up, new Vector2(1, 1),
        Vector2.right, new Vector2(1, -1),
        Vector2.down, new Vector2(-1, -1),
        Vector2.left, new Vector2(-1, 1),
    };

    /// <summary>
    /// �������� ������� �������
    /// </summary>
    public static Vector3 GetMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    /// <summary>
    /// ���������� �� ������ ������������ ������ �����
    /// </summary>
    /// <param name="allPos">��������� �� ����� ���������� ������� ��� ��������</param>
    /// <param name="center">����� ������ ������� ����� ��������� ������������</param>
    /// <param name="isFullDirList">������������ �� 8 ����������� </param>
    public static bool ExistEmptySpace(IEnumerable<Vector2Int> allPos, Vector2Int center, bool isFullDirList = false)
    {
        bool result;

        // ��������� �� ����� �������
        var posList = new List<Vector2Int>();
        posList.AddRange(allPos);

        // ��������� � ������� ������� ������ ���� �������
        var emptyPos = new List<Vector2Int>();

        if(isFullDirList)
            FullDirectionsList.ForEach(s => emptyPos.Add(Vector2Int.RoundToInt(s) + center));
        else
            StandartDirectionsList.ForEach(s => emptyPos.Add(Vector2Int.RoundToInt(s) + center));

        // ��������� �� ������ ����� �� ������� �����
        result = posList.Contains(emptyPos[0]);
        for (int i = 1; i < emptyPos.Count; i++)
            result &= posList.Contains(emptyPos[i]);

        return result;
    }

    /// <summary>
    /// �������� ����������� �� ����� �� ����
    /// </summary>
    /// <returns>
    /// ����������� � �����������
    /// </returns>
    /// <param name="position">����� �� �������</param>
    /// <param name="targetPosition">����� �� ������� (����)</param>
    public static Vector2 GetDirectionTo(Vector2 position, Vector2 targetPosition)
    {
        float x = 0;
        float y = 0;

        // �������� �� �
        if (position.x > targetPosition.x)
            x = -1;
        else if (position.x < targetPosition.x)
            x = 1;

        // �������� �� �
        if (position.y > targetPosition.y)
            y = -1;
        else if (position.y < targetPosition.y)
            y = 1;

        return new Vector2(x, y);
    }

    /// <summary>
    /// �������� ��������� ����������� �� <see cref="StandartDirectionsList"/>
    /// </summary>
    /// <returns>
    /// ����������� � �����������
    /// </returns>
    public static Vector2 GetRandomStandartDirection() => StandartDirectionsList[Random.Range(0, StandartDirectionsList.Count)];

    /// <summary>
    /// �������� ��������� ����������� �� <see cref="FullDirectionsList"/>
    /// </summary>
    /// <returns>
    /// ����������� � �����������
    /// </returns>
    public static Vector2 GetRandomFullDirection() => FullDirectionsList[Random.Range(0, FullDirectionsList.Count)];
}