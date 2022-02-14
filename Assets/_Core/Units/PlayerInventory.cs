using UnityEngine;

/// <summary>
/// Компонент отвечающий за инвентарь игрока
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    [Header("Count of Dropped Crystals")]
    [SerializeField] private NumericAttrib _dropCrystals = new NumericAttrib(0, false);
    public NumericAttrib DropCrystals { get => _dropCrystals; private set => _dropCrystals = value; }

    /// <summary>
    /// Добавляет в инвентарь кристаллы
    /// </summary>
    /// <param name="crystalsCount">Кол-во кристаллов которые нужно добавить</param>
    public void AddDropCrystals(NumericAttrib crystalsCount)
    {
        DropCrystals += crystalsCount;
    }
}