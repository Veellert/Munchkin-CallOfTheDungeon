using System;
using System.Collections.Generic;
using System.Linq;
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
    


    // Animator
    public static List<AnimatorClipInfo> GetAllClips(this Animator current, int layerIndex)
    {
        return current.GetCurrentAnimatorClipInfo(layerIndex).ToList();
    }
    public static bool IsExistClip(this Animator current, string clipName, int layerIndex = 0)
    {
        return current.GetAllClips(layerIndex).Exists(s => s.clip.name == clipName);
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



    // Vector2
    /// <returns>
    /// ��������� ���������� �� ������ �����
    /// </returns>
    /// <param name="current">������� ����������</param>
    public static Vector2Int Rounded(this Vector2 current) => Vector2Int.RoundToInt(current);



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