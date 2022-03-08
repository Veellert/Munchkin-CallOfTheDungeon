using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику эфектов
/// </summary>
public class EffectSystem : MonoBehaviour
{
    private List<BaseEffect> _effectList = new List<BaseEffect>();

    /// <summary>
    /// Добавляет эфект и запускает его
    /// </summary>
    /// <param name="effect">Эфект</param>
    public void AddEffect(BaseEffect effect)
    {
        _effectList.Add(effect);
        effect.StartEffect();

        StartCoroutine(RemoveEffect(effect));
    }
    /// <summary>
    /// Удаляет эфект и останавливает его
    /// </summary>
    /// <param name="effect">Эфект</param>
    public IEnumerator RemoveEffect(BaseEffect effect)
    {
        yield return new WaitForSeconds(effect.GetPreferences().Duration);

        effect.StopEffect();
        _effectList.Remove(effect);
    }
    /// <returns>
    /// Список активных эфектов
    /// </returns>
    public List<BaseEffect> GetEffects() => _effectList;
}