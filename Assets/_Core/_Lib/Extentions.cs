using UnityEngine;

public static class Extentions
{
    /// <summary>
    /// ������������� ������������ ��������� �������
    /// </summary>
    /// <param name="gameObject">������</param>
    /// <param name="alphaValue">�������� ������������</param>
    public static void SetAlpha(this GameObject gameObject, float alphaValue)
    {
        gameObject.GetComponent<Renderer>().material.SetAlpha(alphaValue);
    }
    /// <summary>
    /// ������������� ������������ ���������
    /// </summary>
    /// <param name="material">��������</param>
    /// <param name="alphaValue">�������� ������������</param>
    public static void SetAlpha(this Material material, float alphaValue)
    {
        var oldColor = material.color;
        material.color = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
    }

    /// <summary>
    /// ��������������� ������������ ��������� �������
    /// </summary>
    /// <param name="gameObject">������</param>
    public static void ResetAlpha(this GameObject gameObject)
    {
        gameObject.GetComponent<Renderer>().material.ResetAlpha();
    }
    /// <summary>
    /// ��������������� ������������ ���������
    /// </summary>
    /// <param name="material">��������</param>
    public static void ResetAlpha(this Material material)
    {
        var oldColor = material.color;
        material.color = new Color(oldColor.r, oldColor.g, oldColor.b);
    }
}