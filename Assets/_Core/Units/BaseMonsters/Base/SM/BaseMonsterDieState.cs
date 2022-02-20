using UnityEngine;

/// <summary>
/// Состояние смерти монстра по умолчанию
/// </summary>
public class BaseMonsterDieState : IUnitState
{
    //===>> Components & Fields <<===\\

    protected BaseMonster _monster;
    protected AnimationCaller _animation;
    protected float _destroyAfterDeathDelay;

    //===>> Constructor <<===\\

    public BaseMonsterDieState(BaseMonster monster, float destroyAfterDeathDelay)
    {
        _monster = monster;
        _animation = _monster.GetComponent<AnimationCaller>();
        _destroyAfterDeathDelay = destroyAfterDeathDelay;
    }

    //===>> Interfaces Methods <<===\\

    public virtual void OnEnter()
    {
        _monster.GetComponent<Collider2D>().isTrigger = true;
        _animation.DIE(() =>
        {
            BaseMonster.RemoveMonsterFromStack(_monster);
            _monster.Invoke(nameof(_monster.DestroySelf), _destroyAfterDeathDelay);
        });
    }

    public virtual void OnExecute()
    {

    }

    public virtual void OnExit()
    {

    }
}
