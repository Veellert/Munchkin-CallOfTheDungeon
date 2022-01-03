using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    [SerializeField] private Vector2 _playerPosition;
    [SerializeField] private Player _player;

    private void Start()
    {
        if (Player.Instance && Player.Instance.IsDead)
        {
            Destroy(Player.Instance.gameObject);
            Player.Instance = null;
        }

        if (!Player.Instance)
            Instantiate(_player, _playerPosition, Quaternion.identity);
        else
            Player.Instance.transform.position = _playerPosition;
    }
}
