using UnityEngine;

namespace Assets.UsingSystem
{
    /// <summary>
    /// Компонент родитель отвечающий за логику использования предмета
    /// </summary>
    public abstract class UsingSystem : MonoBehaviour
    {
        /// <summary>
        /// Использовать предмет
        /// </summary>
        public abstract void Use();
    }
}
