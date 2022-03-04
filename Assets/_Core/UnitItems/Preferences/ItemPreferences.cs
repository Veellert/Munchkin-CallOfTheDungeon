using UnityEngine;

/// <summary>
/// Объект хранящий в себе настройки предмета
/// </summary>
public abstract class ItemPreferences : ScriptableObject
{
    [SerializeField] protected string _id = "Item_ID";
    public string ID => _id;

    [SerializeField] [TextArea] protected string _name;
    public string Name => _name;
    
    [SerializeField] [TextArea] protected string _description;
    public string Description => _description;
    
    [SerializeField] protected Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] protected eItemRare _rare;
    public eItemRare Rare => _rare;
    public string RareText
    {
        get
        {
            var result = "Нет";

            switch (_rare)
            {
                case eItemRare.Cheap: result = "Дешманский"; break;
                case eItemRare.Pricy: result = "Ценный"; break;
                case eItemRare.Rich: result = "Дорогущий"; break;
            }

            return result;
        }
    }
}

/// <summary>
/// Перечисление всех типов редкости предметов
/// </summary>
public enum eItemRare
{
    Cheap,
    Pricy,
    Rich,
}
