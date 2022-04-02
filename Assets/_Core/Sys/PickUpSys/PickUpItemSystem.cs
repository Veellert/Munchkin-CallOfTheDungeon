using UnityEngine;

namespace Assets.PickUpSystem
{
    /// <summary>
    /// Компонент родитель отвечающий за подбор премета в инвентарь
    /// </summary>
    [RequireComponent(typeof(Collider2D), typeof(BaseItem))]
    public class PickUpItemSystem : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<PSBRenderer>().SetAlpha(0);
                gameObject.transform.SetParent(Player.Instance.transform);
                AddToInventory(GetComponent<BaseItem>());
            }
        }

        /// <summary>
        /// Добавить предмет в инвентарь
        /// </summary>
        /// <param name="item">Предмет</param>
        private void AddToInventory(BaseItem item)
        {
            if (GetComponent<BaseEquipment>() != null)
                Player.Instance?.EquipmentInventory?.AddEquipmentToInventory((BaseEquipment)item);
            else
                Player.Instance?.Inventory?.AddItemToInventory(item);
        }
    }
}
