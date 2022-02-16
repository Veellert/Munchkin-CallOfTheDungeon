using UnityEngine;

public static class Extentions
{
    /// <summary>
    /// Устанавливает прозрачность материала объекта
    /// </summary>
    /// <param name="gameObject">Объект</param>
    /// <param name="alphaValue">Значение прозрачности</param>
    public static void SetAlpha(this GameObject gameObject, float alphaValue)
    {
        gameObject.GetComponent<Renderer>().material.SetAlpha(alphaValue);
    }
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
    /// Восстанавливает прозрачность материала объекта
    /// </summary>
    /// <param name="gameObject">Объект</param>
    public static void ResetAlpha(this GameObject gameObject)
    {
        gameObject.GetComponent<Renderer>().material.ResetAlpha();
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
}