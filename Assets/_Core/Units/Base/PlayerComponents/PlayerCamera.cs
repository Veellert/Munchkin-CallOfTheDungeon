using Assets.UI;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за камеру игрока
/// </summary>
[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    //===>> Inspector <<===\\

    [Header("Minimap Object")]
    [SerializeField] private MinimapRenderer _minimapCamera;

    //===>> Attributes & Properties <<===\\

    private float PlayerX => Player.Instance.transform.position.x;
    private float PlayerY => Player.Instance.transform.position.y;

    //===>> Unity <<===\\

    private void Update()
    {
        if (Player.Instance)
            transform.position = new Vector3(PlayerX, PlayerY, transform.position.z);
    }

    //===>> Public Methods <<===\\

    /// <returns>
    /// Текущая миникарта
    /// </returns>
    public MinimapRenderer GetCurrentMinimap() => _minimapCamera;
}
