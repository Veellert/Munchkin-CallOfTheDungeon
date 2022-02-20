using UnityEngine;

/// <summary>
/// Состояние рывка игрока по умолчанию
/// </summary>
public class PlayerDodgeState : IUnitState
{
    //===>> Properties <<===\\

    protected Player CurrentPlayer => Player.Instance;
    protected NumericAttrib DodgeCooldown => CurrentPlayer.DodgeCooldown;
    protected NumericAttrib AttackCooldown => CurrentPlayer.AttackCooldown;
    protected NumericAttrib DodgeImpulse => CurrentPlayer.DodgeImpulse;

    //===>> Components & Fields <<===\\

    protected AnimationCaller _animation;
    protected PlayerStateMachine _stateMachine;

    //===>> Constructor <<===\\

    public PlayerDodgeState(PlayerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _animation = CurrentPlayer.GetComponent<AnimationCaller>();
    }

    //===>> Interfaces Methods <<===\\

    public virtual void OnEnter()
    {
        DodgeImpulse.FillToMax();

        _animation.DODGE(() =>
        {
            _stateMachine.TransitToDefault();
        });
    }

    public virtual void OnExecute()
    {
        AttackCooldown.DecreaseOnDeltaTime();

        CurrentPlayer.Move(CurrentPlayer.GetLastMovementDirection() * DodgeImpulse);
        DodgeImpulse.DecreaseValue(DodgeImpulse.MaxValue * Time.deltaTime);
    }

    public virtual void OnExit()
    {
        DodgeCooldown.FillToMax();
        DodgeImpulse.FillEmpty();
    }
}
