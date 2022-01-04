using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за передвижение игрока по локациям
/// </summary>
public class PlayerLocationController : MonoBehaviour
{
    [SerializeField] private eLocations _currentLocation;

    /// <summary>
    /// Запускает следующую по списку локацию
    /// </summary>
    public void MoveToNextLocation()
    {
        int nextLoc = (int)_currentLocation + 1;

        if (nextLoc < System.Enum.GetValues(typeof(eLocations)).Length)
            SetLocation(nextLoc);
    }

    /// <summary>
    /// Устанавливает локацию по номеру
    /// </summary>
    /// <param name="location">Номер локации</param>
    private void SetLocation(int location)
    {
        _currentLocation = (eLocations)location;

        switch (_currentLocation)
        {
            case eLocations.Lobby: SceneLoader.Lobby(); break;
            case eLocations.Level1: SceneLoader.FirstLevel(); break;
            case eLocations.Boss1: SceneLoader.FirstBoss(); break;
            default: SceneLoader.Lobby(); break;
        }
    }
}

/// <summary>
/// Список уровней
/// </summary>
public enum eLocations
{
    Lobby,
    Level1,
    Boss1,
}