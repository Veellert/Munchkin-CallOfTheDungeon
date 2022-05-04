using UnityEngine;

/// <summary>
/// Компонент родитель отвечающий за логику агрессивного монстра
/// </summary>
public partial class AgressiveMonster : BaseMonster, IDamager
{
    //===>> Attributes & Properties <<===\\

    public NumericAttrib Damage { get; protected set; }
    public NumericAttrib AttackRange { get; protected set; }
    public NumericAttrib AttackCooldown { get; protected set; }
    public bool CanAttack => AttackCooldown.IsValueEmpty();

    protected new AgressiveMonsterPreferences MonsterPreferences => (AgressiveMonsterPreferences)_preferences;

    //===>> Components & Fields <<===\\

    protected Vector2 _tempAttackPosition;

    //===>> Important Methods <<===\\

    protected override void InitializeAttributes()
    {
        base.InitializeAttributes();

        Damage = MonsterPreferences.Damage;
        AttackRange = MonsterPreferences.AttackRange;
        AttackCooldown = MonsterPreferences.AttackCooldown;

        _tempAttackPosition = transform.position + Vector3.right;
    }
    protected override void InitializeStateMachine()
    {
        base.InitializeStateMachine();

        _stateMachine = new AgressiveMonsterStateMachine((MonsterStateMachine)_stateMachine);

        ((AgressiveMonsterStateMachine)_stateMachine).TransitToDefault();
    }

    //===>> Interfaces Methods <<===\\

    public void AttackTo(IDamageable target, float damage) => target?.ReceiveDamage(damage);

    //===>> Public Methods <<===\\

    /// <returns>
    /// Временную позицию атаки
    /// </returns>
    public Vector2 GetTempAttackPosition() => _tempAttackPosition;

    /// <summary>
    /// Устанавливает временную позицию атаки
    /// </summary>
    /// <param name="position">Позиция атаки</param>
    public void SetTempAttackPosition(Vector2 position) => _tempAttackPosition = position;

    //===>> Gizmos <<===\\

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.white;
        #region Attack

        if (AttackRange != null)
            Gizmos.DrawWireSphere(_tempAttackPosition, AttackRange);
        else
            Gizmos.DrawWireSphere(transform.position + (BtwTargetDistance.TileHalfed() * Vector3.right), MonsterPreferences.AttackRange);

        #endregion
    }
}
