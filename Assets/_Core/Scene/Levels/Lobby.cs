using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику лобби
/// </summary>
public class Lobby : MonoBehaviour
{
    [SerializeField] private Vector2 _playerPosition;
    [SerializeField] private Player _player;
    [Space]
    [SerializeField] private PlayerCrosshair _crosshair;
    [SerializeField] private TutorialMenu _tutorMenu;

    private bool _isTutorClosed;

    private void FixedUpdate()
    {
        if (_isTutorClosed)
            return;

        if (!_tutorMenu.gameObject.activeSelf)
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

            if (!PlayerCrosshair.Instance)
                PlayerCrosshair.Activate(_crosshair);

            _isTutorClosed = true;
        }
    }
}
