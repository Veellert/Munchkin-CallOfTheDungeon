using System;
using UnityEngine;

public static class Extentions
{
    // Material
    /// <summary>
    /// Устанавливает прозрачность материала
    /// </summary>
    /// <param name="material">Материал</param>
    /// <param name="alphaValue">Значение прозрачности</param>
    public static void SetAlpha(this Material material, float alphaValue)
    {
        var oldColor = material.color;
        material.color = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
    }
    /// <summary>
    /// Восстанавливает прозрачность материала
    /// </summary>
    /// <param name="material">Материал</param>
    public static void ResetAlpha(this Material material)
    {
        var oldColor = material.color;
        material.color = new Color(oldColor.r, oldColor.g, oldColor.b);
    }



    // Transform
    public static float DistanceTo(this Transform current, Transform target)
    {
        return Vector2.Distance(current.position, target.position);
    }
    public static float DistanceToPlayer(this Transform current)
    {
        return Vector2.Distance(current.position, Player.Instance.transform.position);
    }


    
    // Float => NumericAttrib
    public static float TileHalfed(this float value)
    {
        return new TileHalf(value);
    }
    public static float TileHalfed(this NumericAttrib value)
    {
        return new TileHalf(value);
    }
    public static void DecreaseOnDeltaTime(this NumericAttrib value)
    {
        value.DecreaseValue(Time.deltaTime);
    }



    // MonoBehaviour
    /// <summary>
    /// Выполняет метод проверяя не выполняется ли он уже
    /// </summary>
    /// <param name="methodName">Название метода</param>
    /// <param name="delay">Задержка перед выполнением</param>
    public static void LoopProtectedInvoke(this MonoBehaviour caller, string methodName, float delay = 0)
    {
        caller.LoopProtectedInvoke(methodName, () => caller.Invoke(methodName, delay));
    }
    /// <summary>
    /// Выполняет действие проверяя не выполняется ли какой-то метод
    /// </summary>
    /// <param name="methodName">Название метода</param>
    /// <param name="action">Действие</param>
    public static void LoopProtectedInvoke(this MonoBehaviour caller, string methodName, Action action = null)
    {
        if (action != null && !caller.IsInvoking(methodName))
            action.Invoke();
    }
}