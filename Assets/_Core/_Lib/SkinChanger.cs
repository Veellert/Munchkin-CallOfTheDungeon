using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

/// <summary>
///  омпонент отвечающий за смену скинов персонажей
/// </summary>
public class SkinChanger : MonoBehaviour
{
    [SerializeField] private SpriteLibrary _spriteLibrary;
    [SerializeField] private List<SpriteLibraryAsset> _spriteAssetList;
    [SerializeField] private List<SpriteResolver> _skinPartList;

    private SpriteLibraryAsset CurrentAsset => _spriteLibrary.spriteLibraryAsset;

    /// <summary>
    /// ћен€ет весь скин по его названию
    /// </summary>
    /// <param name="label">Ќазвание из перечислени€ скинов которое превратитс€ в строку</param>
    public void ChangeFullSkin(Enum label)
    {
        var activeLabel = label.ToString();

        ChangeFullSkin(activeLabel);
    }

    /// <summary>
    /// ћен€ет весь скин по его названию
    /// </summary>
    /// <param name="label">Ќазвание скина</param>
    public void ChangeFullSkin(string label)
    {
        // ѕеребираем список ассетов
        foreach (var asset in _spriteAssetList)
            // Ќаходим нужный ассет у которого присутсвует название скина из параметра
            if (asset.GetCategoryLabelNames(asset.GetCategoryNames().First()).Contains(label))
                if(CurrentAsset != asset)
                {
                    _spriteLibrary.spriteLibraryAsset = asset;

                    // ћен€ем скин всех частей модели на нужный скин
                    foreach (var skinPart in _skinPartList)
                        skinPart.SetCategoryAndLabel(CurrentAsset.GetCategoryNames().ToList().Find(s => s == skinPart.name), label);
                }
    }
}
