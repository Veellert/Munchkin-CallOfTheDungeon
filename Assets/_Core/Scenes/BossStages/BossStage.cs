using UnityEngine;

namespace Assets.BossStages
{
    /// <summary>
    /// Компонент родитель отвечающий за логику уровня босса
    /// </summary>
    public abstract class BossStage<T> : MonoBehaviour where T : Boss
    {
        [Header("Player")]
        [SerializeField] protected Vector2 _playerPosition;

        [Header("Boss")]
        [SerializeField] protected T _bossTarget;
        [SerializeField] protected Vector2 _bossPosition;

        public T CurrentBoss { get; protected set; }

        protected virtual void Start()
        {
            Player.Instance.transform.position = _playerPosition;
            CurrentBoss = SpawnBoss();
        }

        /// <returns>
        /// Заспавненный босс
        /// </returns>
        protected T SpawnBoss() => (T)Instantiate(_bossTarget, _bossPosition, Quaternion.identity);
    }
}
