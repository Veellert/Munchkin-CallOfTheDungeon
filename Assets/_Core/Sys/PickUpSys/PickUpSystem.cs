using UnityEngine;

namespace Assets.PickUpSystem
{
    /// <summary>
    /// Компонент родитель отвечающий за систему подбора преметов в инвентарь игрока
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public abstract class PickUpSystem : MonoBehaviour
    {
        /// <summary>
        /// При подборе объекта
        /// </summary>
        protected abstract void PickUp();

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                PickUp();
        }

        /// <summary>
        /// Добавить экипировку в инвентарь
        /// </summary>
        /// <param name="equipment">Экипировка</param>
        protected void AddEquipmentToInventory(BaseEquipment equipment)
        {
            Player.Instance?.EquipmentInventory?.AddEquipmentToInventory(equipment);
        }
        /// <summary>
        /// Добавить предмет в инвентарь
        /// </summary>
        /// <param name="equipment">Предмет</param>
        protected void AddItemToInventory(BaseItem item)
        {
            Player.Instance?.Inventory?.AddItemToInventory(item);
        }
    }
}
