using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ���������� �� ������ ������� � 1 ������ 1 ������
/// </summary>
public class SmileyFaceBossStage : MonoBehaviour
{
    [SerializeField] private Vector2 _playerPosition;
    [SerializeField] private Vector2 _bossPosition;
    [SerializeField] private SmileyFaceBoss _bossTarget;

    private void Start()
    {
        Player.Instance.transform.position = _playerPosition;

        _bossTarget = Instantiate(_bossTarget, _bossPosition, Quaternion.identity);
    }
}
