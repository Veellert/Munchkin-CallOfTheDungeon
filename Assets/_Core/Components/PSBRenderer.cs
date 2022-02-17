using UnityEngine;

public class PSBRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _spriteParent;

    private SpriteRenderer[] _modelPartsArray;

    private void Start()
    {
        _modelPartsArray = _spriteParent.GetComponentsInChildren<SpriteRenderer>();
    }

    public void SetAlpha(float alpha)
    {
        foreach (var part in _modelPartsArray)
            part.material.SetAlpha(alpha);
    }

    public void ResetAlpha()
    {
        foreach (var part in _modelPartsArray)
            part.material.ResetAlpha();
    }
}
