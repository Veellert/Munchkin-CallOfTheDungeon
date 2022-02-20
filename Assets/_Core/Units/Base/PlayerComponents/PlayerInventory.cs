using UnityEngine;

/// <summary>
/// Компонент отвечающий за инвентарь игрока
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    //===>> Inspector <<===\\

    [Header("Count of Dropped Crystals")]
    [SerializeField] private NumericAttrib _dropCrystals = new NumericAttrib(0, false);

    //===>> Attributes & Properties <<===\\

    public NumericAttrib DropCrystals { get => _dropCrystals; private set => _dropCrystals = value; }

    //===>> Public Methods <<===\\

    /// <summary>
    /// Добавляет в инвентарь кристаллы
    /// </summary>
    /// <param name="crystalsCount">Кол-во кристаллов которые нужно добавить</param>
    public void AddDropCrystals(NumericAttrib crystalsCount)
    {
        DropCrystals += crystalsCount;
    }
}