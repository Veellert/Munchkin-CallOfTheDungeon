using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки юнита
/// </summary>
[CreateAssetMenu(fileName = "NewUnit", menuName = "Units/Create Unit", order = 0)]
public class UnitPreferences : ScriptableObject, IDamageableAttrib
{
    [Header("Basic Information")]
    [SerializeField] protected string _id = "Unit_ID";
    public string ID => _id;

    [SerializeField] [TextArea] protected string _name = "";
    public string Name => _name;
    [SerializeField] [TextArea] protected string _description = "";
    public string Description => _description;

    [Header("Basic Attributes")]
    [SerializeField] [Min(0.1f)] protected float _hitboxRange = 0.5f;
    public float HitboxRange => _hitboxRange;

    [SerializeField] [Min(0)] protected float _speed = 1;
    public float Speed => _speed;
    [SerializeField] [Min(1)] protected float _hp = 50;
    public float HP => _hp;
}
