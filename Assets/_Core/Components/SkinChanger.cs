using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

/// <summary>
/// Компонент отвечающий за смену скинов персонажей
/// </summary>
public class SkinChanger : MonoBehaviour
{
    [Header("Model Skin Manager")]
    [SerializeField] private SpriteLibrary _spriteLibrary;
    [Header("Skins Collection")]
    [SerializeField] private List<SpriteLibraryAsset> _spriteAssetList;
    [Header("Model Parts Collection")]
    [SerializeField] private List<SpriteResolver> _skinPartList;

    private SpriteLibraryAsset CurrentAsset => _spriteLibrary.spriteLibraryAsset;

    /// <summary>
    /// Меняет текущий скин на новый
    /// </summary>
    /// <param name="skinAsset">Cкин</param>
    public void ChangeFullSkin(SpriteLibraryAsset skinAsset)
    {
        string label = GetAssetLabel(skinAsset);
        // Находим нужный ассет у которого присутсвует название скина из параметра
        var requireAsset = _spriteAssetList.Find(asset => HasLabel(asset, label));

        if (CurrentAsset == requireAsset)
            return;

        _spriteLibrary.spriteLibraryAsset = requireAsset;
        // Меняем скин всех частей модели на нужный скин
        _skinPartList.ForEach(skinPart =>
            skinPart.SetCategoryAndLabel(CurrentAsset.GetCategoryNames().ToList().Find(s => s == skinPart.name), label));

        List<string> GetAssetLabels(SpriteLibraryAsset asset)
            => asset.GetCategoryLabelNames(asset.GetCategoryNames().First()).ToList();

        string GetAssetLabel(SpriteLibraryAsset asset)
            => asset.GetCategoryLabelNames(asset.GetCategoryNames().First()).First();

        bool HasLabel(SpriteLibraryAsset asset, string label)
            => GetAssetLabels(asset).Contains(label);
    }
}
