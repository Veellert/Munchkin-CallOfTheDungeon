using Assets.DropSystem;
using UnityEngine;

/// <summary>
/// Класс отвечающий за логику зелья "Зелье полиморфа"
/// </summary>
public class PollymorphPotion : BasePotion
{
    [Header("Pollymorph")]
    [SerializeField] private PollymorphMonster _pollymorphModel;

    private PollymorphMonster _pollymorph;

    protected override void Use()
    {
        base.Use();

        if (!_target)
            return;

        PollymorphTarget();
        InitializePollymorph();
    }

    /// <summary>
    /// Назначает дроп для полиморфа
    /// </summary>
    private void InitializePollymorph()
    {
        if (_target.TryGetComponent(out DropSystem drop))
            _pollymorph.InitializePollymorph(drop, _target);
    }

    /// <summary>
    /// Превращает цель в полиморфа
    /// </summary>
    private void PollymorphTarget()
    {
        _pollymorph = Instantiate(_pollymorphModel, _target.gameObject.transform.position, Quaternion.identity);

        _target.gameObject.SetActive(false);
        _target.transform.localScale = new Vector2(0, 0);
        Monster.RemoveMonsterFromStack((Monster)_target);
    }
}