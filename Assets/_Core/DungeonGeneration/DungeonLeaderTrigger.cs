using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ���������� �� ������� ����� ��������� �� ��������
/// </summary>
public class DungeonLeaderTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Monster.GetMonsters().Count == 0)
                Player.Instance.GetComponent<PlayerLocationController>().MoveToNextLocation();
        }
    }
}
