using UnityEngine;

/// <summary>
/// Обычное состояние игрока по умолчанию
/// </summary>
public class PlayerDefaultState : IUnitState
{
    //===>> Properties <<===\\

    protected Player CurrentPlayer => Player.Instance;
    protected NumericAttrib DodgeCooldown => CurrentPlayer.DodgeCooldown;
    protected NumericAttrib AttackCooldown => CurrentPlayer.AttackCooldown;
    protected NumericAttrib Speed => CurrentPlayer.Speed;

    //===>> Components & Fields <<===\\

    protected AnimationCaller _animation;

    //===>> Constructor <<===\\

    public PlayerDefaultState()
    {
        _animation = CurrentPlayer.GetComponent<AnimationCaller>();
    }

    //===>> Interfaces Methods <<===\\

    public virtual void OnEnter()
    {
        InputObserver.Instance.OnLeftMouseButton += CurrentPlayer.AttackInput;
        InputObserver.Instance._dodge.OnButtonUse += CurrentPlayer.DodgeInput;
    }

    public virtual void OnExecute()
    {
        AttackCooldown.DecreaseOnDeltaTime();
        DodgeCooldown.DecreaseOnDeltaTime();

        CurrentPlayer.SetDirection(InputObserver.Instance.GetInputDirection(),
            needRendering: false);

        CurrentPlayer.Move(CurrentPlayer.GetMovementDirection() * Speed);
        _animation.RUNNING(CurrentPlayer.GetMovementDirection());
    }

    public virtual void OnExit()
    {
        InputObserver.Instance.OnLeftMouseButton -= CurrentPlayer.AttackInput;
        InputObserver.Instance._dodge.OnButtonUse -= CurrentPlayer.DodgeInput;
    }
}
