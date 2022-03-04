using System;
using UnityEngine;

namespace Assets.UsingSystem
{
    /// <summary>
    /// Компонент отвечающий за логику использования метательного зелья
    /// </summary>
    public class ThrowableUseOnTarget : UsingPotion
    {
        //===>> Inspector <<===\\

        [SerializeField] private float _potionRange = 4;
        [SerializeField] private float _throwSpeed = 2.5f;

        //===>> Components & Fields <<===\\

        /// <summary>
        /// Событие во время броска
        /// </summary>
        private event Action UntilThrow;

        private BaseMonster _target;
        private Vector2 _throwPosition;

        //===>> Unity <<===\\

        private void FixedUpdate()
        {
            UntilThrow?.Invoke();
        }

        //===>> Public Methods <<===\\

        public override void Use()
        {
            base.Use();

            transform.SetParent(Player.Instance.transform);

            UntilThrow += ChasePlayer;
            InputObserver.Instance.OnRightMouseButton += ThrowItem;
        }

        //===>> On Events <<===\\

        /// <summary>
        /// Событие во время броска
        /// </summary>
        private void ThrowItem()
        {
            UntilThrow -= ChasePlayer;
            InputObserver.Instance.OnRightMouseButton -= ThrowItem;

            transform.SetParent(null);
            _throwPosition = InputObserver.GetMousePosition();
            _target = BaseMonster.GetClosest(_throwPosition, _potionRange.TileHalfed());

            UntilThrow += ThrowTo;
        }

        //===>> Private & Protected Methods <<===\\

        /// <summary>
        /// Преследует мышку игрока
        /// </summary>
        private void ChasePlayer()
        {
            transform.position = (Vector2)Player.Instance.transform.position + Direction2D.GetMouseDirection() * new TileHalf();
        }

        /// <summary>
        /// Бросок
        /// </summary>
        private void ThrowTo()
        {
            transform.position = Vector2.MoveTowards(transform.position, _throwPosition, _throwSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _throwPosition) <= new TileHalf() ||
                (_target && Vector2.Distance(transform.position, _target.transform.position) <= new TileHalf()))
                ApproveThrow();
        }

        /// <summary>
        /// Бросок попал в цель
        /// </summary>
        private void ApproveThrow()
        {
            UntilThrow -= ThrowTo;

            _potion.UsePotionOn(_target);
        }
    }
}
