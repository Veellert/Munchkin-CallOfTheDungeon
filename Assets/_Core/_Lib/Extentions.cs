using System;
using UnityEngine;

public static class Extentions
{
    // Material
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
    /// ��������������� ������������ ���������
    /// </summary>
    /// <param name="material">��������</param>
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
    /// ��������� ����� �������� �� ����������� �� �� ���
    /// </summary>
    /// <param name="methodName">�������� ������</param>
    /// <param name="delay">�������� ����� �����������</param>
    public static void LoopProtectedInvoke(this MonoBehaviour caller, string methodName, float delay = 0)
    {
        caller.LoopProtectedInvoke(methodName, () => caller.Invoke(methodName, delay));
    }
    /// <summary>
    /// ��������� �������� �������� �� ����������� �� �����-�� �����
    /// </summary>
    /// <param name="methodName">�������� ������</param>
    /// <param name="action">��������</param>
    public static void LoopProtectedInvoke(this MonoBehaviour caller, string methodName, Action action = null)
    {
        if (action != null && !caller.IsInvoking(methodName))
            action.Invoke();
    }
}