using UnityEngine;

/// <summary>
/// Список уровней
/// </summary>
public enum eLocations
{
    Lobby = 0,
    Level1 = 1,
    L1Boss1 = 2,
}

/// <summary>
/// Компонент отвечающий за перемещение игрока по локациям
/// </summary>
public class PlayerLocationController : MonoBehaviour
{
    //===>> Inspector <<===\\

    [Header("Current Player Level")]
    [SerializeField] private eLocations _currentLocation;

    //===>> Editor Methods <<===\\

    /// <summary>
    /// Перемещает в текущую локацию
    /// </summary>
    public void SetCurrentLocation() => SetLocation((int)_currentLocation);

    //===>> Public Methods <<===\\

    /// <returns>
    /// Текущая локация
    /// </returns>
    public eLocations GetCurrentLocation() => _currentLocation;

    /// <summary>
    /// Запускает следующую по списку локацию
    /// </summary>
    public void MoveToNextLocation()
    {
        int nextLoc = (int)_currentLocation + 1;

        if (nextLoc < System.Enum.GetValues(typeof(eLocations)).Length)
            SetLocation(nextLoc);
        else
            SetLocation(1);
    }
    
    /// <summary>
    /// Запускает лобби
    /// </summary>
    public void SetLobby()
    {
        SetLocation(0);
    }

    //===>> Private & Protected Methods <<===\\

    /// <summary>
    /// Устанавливает локацию по номеру
    /// </summary>
    /// <param name="location">Номер локации</param>
    public void SetLocation(int location)
    {
        _currentLocation = (eLocations)location;

        switch (_currentLocation)
        {
            case eLocations.Lobby: SceneLoader.Lobby();
                Player.Instance.Inventory.DropCrystals.FillEmpty();
                Player.Instance.HP.FillToMax();
                Player.Instance.Inventory.RemoveAllFromInventory();
                Player.Instance.EquipmentInventory.RemoveAllFromCurrentEquipment();
                Player.Instance.EquipmentInventory.RemoveAllFromEquipment();
                break;
            case eLocations.Level1: SceneLoader.FirstLevel(); break;
            case eLocations.L1Boss1: SceneLoader.L1FirstBoss(); break;
            default: SceneLoader.Lobby(); break;
        }
    }
}
