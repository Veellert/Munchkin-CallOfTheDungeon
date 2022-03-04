using UnityEngine;

namespace Assets.PickUpSystem
{
    /// <summary>
    /// Компонент родитель отвечающий за подбор премета в инвентарь
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public abstract class PickUpItemSystem : MonoBehaviour
    {
        /// <summary>
        /// Подобрать предмет
        /// </summary>
        public abstract void PickUp();

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PickUp();
                GetComponent<Collider2D>().enabled = false;
                GetComponent<PSBRenderer>().SetAlpha(0);
            }
        }

        /// <summary>
        /// Добавить предмет в инвентарь
        /// </summary>
        /// <param name="item">Предмет</param>
        protected void AddToInventory(BaseItem item)
        {
            Player.Instance?.Inventory?.AddItemToInventory(item);
        }
    }
}
