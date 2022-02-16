using UnityEngine;

namespace Assets.DropSystem
{
    /// <summary>
    /// Система дропа после смерти
    /// </summary>
    public abstract class DropAfterDeath : DropSystem
    {
        private void Start()
        {
            GetComponent<IDamageable>().HP.OnValueChanged += OnHPChanged;
        }

        /// <summary>
        /// При изменении здоровья цели
        /// </summary>
        /// <param name="hp">Отслеживаемый атрибут</param>
        private void OnHPChanged(NumericAttrib hp)
        {
            if (Player.Instance && hp.IsValueEmpty())
                AddDropToInventory();
        }
    }
}
