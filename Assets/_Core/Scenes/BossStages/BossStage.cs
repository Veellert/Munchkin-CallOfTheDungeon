using UnityEngine;

namespace Assets.BossStages
{
    /// <summary>
    /// Компонент родитель отвечающий за логику уровня босса
    /// </summary>
    public abstract class BossStage<TBoss> : MonoBehaviour
        where TBoss : Boss
    {
        [Header("Player")]
        [SerializeField] protected Vector2 _playerPosition;

        [Header("Boss")]
        [SerializeField] protected TBoss _bossTarget;
        [SerializeField] protected Vector2 _bossPosition;

        public TBoss CurrentBoss { get; protected set; }

        protected virtual void Start()
        {
            Player.Instance.transform.position = _playerPosition;
            CurrentBoss = SpawnBoss();
        }

        /// <returns>
        /// Заспавненный босс
        /// </returns>
        protected TBoss SpawnBoss() => Instantiate(_bossTarget, _bossPosition, Quaternion.identity);
    }
}
