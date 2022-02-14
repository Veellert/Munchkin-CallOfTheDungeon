using UnityEngine;

/// <summary>
/// Список уровней
/// </summary>
public enum eLocations
{
    Lobby = 0,
    Level1,
    L1Boss1,
}

/// <summary>
/// Компонент отвечающий за передвижение игрока по локациям
/// </summary>
public class PlayerLocationController : MonoBehaviour
{
    [Header("Current Player Level")]
    [SerializeField] private eLocations _currentLocation;

    /// <returns>
    /// Текущая локация
    /// </returns>
    public eLocations GetCurrentLocation() => _currentLocation;

    /// <summary>
    /// Запускает выбранную локацию
    /// </summary>
    public void SetCurrentLocation()
    {
        SetLocation((int)_currentLocation);
    }
    
    /// <summary>
    /// Запускает следующую по списку локацию
    /// </summary>
    public void MoveToNextLocation()
    {
        int nextLoc = (int)_currentLocation + 1;

        if (nextLoc < System.Enum.GetValues(typeof(eLocations)).Length)
            SetLocation(nextLoc);
        else
            SetLocation(0);

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
            case eLocations.L1Boss1: SceneLoader.L1FirstBoss(); break;
            default: SceneLoader.Lobby(); break;
        }
    }
}
