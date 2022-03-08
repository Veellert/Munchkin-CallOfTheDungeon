using UnityEngine;

public enum eEffectType
{
    Hitbox = 0,
    Invisibility = 1,
    Pollymorph = 2,
}

/// <summary>
/// Объект хранящий в себе настройки эфекта
/// </summary>
public abstract class BaseEffectPreferences : ScriptableObject
{
    // Для индикатора в оверлее всех эфектов
    public eEffectType Type => GetEffectType();
    
    [SerializeField] [TextArea] protected string _description;
    public string Description => _description;

    [SerializeField] [Min(0)] protected float _duration = 5;
    public float Duration => _duration;

    /// <summary>
    /// Создает эфект для цели
    /// </summary>
    /// <param name="target">Цель</param>
    public abstract void CreateEffectFor(BaseUnit target);
    /// <returns>
    /// Тип эфекта
    /// </returns>
    protected abstract eEffectType GetEffectType();
}
