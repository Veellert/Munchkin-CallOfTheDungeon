using Assets.DropSystem;
using UnityEngine;

/// <summary>
/// Класс отвечающий за логику зелья "Зелье полиморфа"
/// </summary>
public class PollymorphPotion : BasePotion
{
    //===>> Inspector <<===\\

    [Header("Pollymorph")]
    [SerializeField] private PollymorphMonster _pollymorphModel;

    //===>> Components & Fields <<===\\

    private PollymorphMonster _pollymorph;

    //===>> Important Methods <<===\\

    protected override void Use()
    {
        base.Use();

        if (!_target)
            return;

        PollymorphTarget();
        InitializePollymorph();

        Destroy(gameObject);
    }

    //===>> Private & Protected Methods <<===\\

    /// <summary>
    /// Назначает дроп для полиморфа
    /// </summary>
    private void InitializePollymorph()
    {
        _pollymorph.InitializePollymorph(_target.GetComponent<DropSystem>(),
            _target.GetComponent<BaseMonster>());
    }

    /// <summary>
    /// Превращает цель в полиморфа
    /// </summary>
    private void PollymorphTarget()
    {
        _pollymorph = Instantiate(_pollymorphModel, _target.gameObject.transform.position, Quaternion.identity);
    }
}