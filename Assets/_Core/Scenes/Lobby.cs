using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику лобби
/// </summary>
public class Lobby : MonoBehaviour
{
    [Header("Input Handler")]
    [SerializeField] private InputObserver _inputInstance;

    [Header("Player")]
    [SerializeField] private Player _playerInstance;
    [SerializeField] private Vector2 _playerStartPosition;
    
    private void Start()
    {
        if (Player.Instance && Player.Instance.IsDead)
        {
            Destroy(Player.Instance.gameObject);
            Player.Instance = null;
        }

        if (!InputObserver.Instance)
            Instantiate(_inputInstance, Vector2.zero, Quaternion.identity);

        if (!Player.Instance)
            Instantiate(_playerInstance, _playerStartPosition, Quaternion.identity);
        else
            Player.Instance.transform.position = _playerStartPosition;
    }
}
