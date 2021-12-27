using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private SpriteLibrary _spriteLibrary;
    [SerializeField] private List<SpriteLibraryAsset> _spriteAssetList;
    [SerializeField] private List<SpriteResolver> _skinPartList;

    private SpriteLibraryAsset currentAsset => _spriteLibrary.spriteLibraryAsset;

    public void ChangeFullSkin(Enum label)
    {
        var activeLabel = label.ToString();

        ChangeFullSkin(activeLabel);
    }

    public void ChangeFullSkin(string label)
    {
        foreach (var asset in _spriteAssetList)
            if (asset.GetCategoryLabelNames(asset.GetCategoryNames().First()).Contains(label))
                if(currentAsset != asset)
                {
                    _spriteLibrary.spriteLibraryAsset = asset;

                    foreach (var skinPart in _skinPartList)
                        skinPart.SetCategoryAndLabel(currentAsset.GetCategoryNames().ToList().Find(s => s == skinPart.name), label);
                }
    }
}
