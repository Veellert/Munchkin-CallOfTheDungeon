using UnityEngine;

namespace Assets.UsingSystem
{
    /// <summary>
    /// Компонент родитель отвечающий за логику использования зелья
    /// </summary>
    public abstract class UsingPotion : UsingSystem
    {
        protected BasePotion _potion;

        protected void Awake()
        {
            _potion = GetComponent<BasePotion>();
        }

        public override void Use()
        {
            GetComponent<PSBRenderer>().ResetAlpha();
        }
    }
}
