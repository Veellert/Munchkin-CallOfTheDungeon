using UnityEngine;

/// <summary>
/// Состояние атаки игрока по умолчанию
/// </summary>
public class PlayerAttackState : IUnitState
{
    //===>> Properties <<===\\

    protected Player CurrentPlayer => Player.Instance;
    protected Transform Transform => CurrentPlayer.transform;
    protected NumericAttrib DodgeCooldown => CurrentPlayer.DodgeCooldown;
    protected NumericAttrib AttackCooldown => CurrentPlayer.AttackCooldown;
    protected NumericAttrib DodgeImpulse => CurrentPlayer.DodgeImpulse;
    protected NumericAttrib AttackRange => CurrentPlayer.AttackRange;
    protected NumericAttrib AttackDistance => CurrentPlayer.AttackDistance.TileHalfed();
    protected NumericAttrib Damage => CurrentPlayer.Damage;
    protected bool IsDodging => CurrentPlayer.IsDodging;

    //===>> Components & Fields <<===\\

    protected AnimationCaller _animation;
    protected PlayerStateMachine _stateMachine;

    //===>> Constructor <<===\\

    public PlayerAttackState(PlayerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _animation = CurrentPlayer.GetComponent<AnimationCaller>();
    }

    //===>> Interfaces Methods <<===\\

    public virtual void OnEnter()
    {
        InputObserver.Instance._dodge.OnButtonUse += CurrentPlayer.DodgeInput;

        CurrentPlayer.ResetVelocity();

        var mouseDirection = Direction2D.GetMouseDirection();
        CurrentPlayer.SetDirection(mouseDirection);

        // position + direction * дальность атаки
        CurrentPlayer.SetTempAttackPosition((Vector2)Transform.position +
            mouseDirection * AttackDistance);

        _animation.ATTACK(() =>
        {
            _stateMachine.TransitToDefault();
        });
    }

    public virtual void OnExecute()
    {
        DodgeCooldown.DecreaseOnDeltaTime();
        CurrentPlayer.SetDirection(InputObserver.Instance.GetInputDirection(), false, false);
    }

    public virtual void OnExit()
    {
        InputObserver.Instance._dodge.OnButtonUse -= CurrentPlayer.DodgeInput;

        if (!IsDodging)
            CurrentPlayer.AttackTo(BaseMonster.GetClosest(CurrentPlayer.GetTempAttackPosition(), AttackRange), Damage);

        AttackCooldown.FillToMax();
    }
}
