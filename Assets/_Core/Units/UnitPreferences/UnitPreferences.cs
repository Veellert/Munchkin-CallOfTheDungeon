using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки юнита
/// </summary>
public abstract class UnitPreferences : ScriptableObject
{
    [SerializeField] protected string _id = "Unit_ID";
    public string ID => _id;

    [SerializeField] protected string _name = "No Named";
    public string Name => _name;
}
