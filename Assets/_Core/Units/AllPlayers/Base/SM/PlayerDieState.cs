using UnityEngine;

/// <summary>
/// Состояние смерти игрока по умолчанию
/// </summary>
public class PlayerDieState : IUnitState
{
    //===>> Properties <<===\\

    protected Player CurrentPlayer => Player.Instance;

    //===>> Components & Fields <<===\\

    protected AnimationCaller _animation;

    //===>> Constructor <<===\\

    public PlayerDieState()
    {
        _animation = CurrentPlayer.GetComponent<AnimationCaller>();
    }

    //===>> Interfaces Methods <<===\\

    public virtual void OnEnter()
    {
        CurrentPlayer.ResetVelocity();
        _animation.DIE();

        CurrentPlayer.GetComponent<Collider2D>().isTrigger = true;
        CurrentPlayer.GetComponent<DirectionModelRenderer>().UnsubscribeChangingForPlayer();
    }

    public virtual void OnExecute()
    {

    }

    public virtual void OnExit()
    {
        CurrentPlayer.GetComponent<Collider2D>().isTrigger = false;
        CurrentPlayer.GetComponent<DirectionModelRenderer>().SubscribeChangingForPlayer();
    }
}
