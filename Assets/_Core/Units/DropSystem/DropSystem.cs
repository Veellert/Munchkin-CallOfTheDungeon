using UnityEngine;

namespace Assets.DropSystem
{
    /// <summary>
    /// Компонент родитель отвечающий за систему дропа
    /// </summary>
    public abstract class DropSystem : MonoBehaviour
    {
        [Header("Drop Count")]
        [SerializeField] protected NumericAttrib _dropCount = new NumericAttrib(5, 20);
        public NumericAttrib DropCount { get => _dropCount; protected set => _dropCount = value; }

        /// <summary>
        /// Добавляет дроп в инвентарь
        /// </summary>
        public abstract void AddDropToInventory();

        /// <returns>
        /// Количество дропа
        /// </returns>
        public virtual NumericAttrib GetDrop() => (int)Random.Range(DropCount.Value, DropCount.MaxValue + 1);
    }
}
