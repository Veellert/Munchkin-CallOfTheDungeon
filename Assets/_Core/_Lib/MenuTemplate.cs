using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ������ ��� ������ ����������� ����
/// </summary>
public class MenuTemplate : MonoBehaviour
{
    [SerializeField] protected Button[] _activeButtons;
    [SerializeField] protected bool _activeOnLoad;

    protected virtual void Start()
    {
        foreach (var button in _activeButtons)
            button.onClick.AddListener(InverseActive);

        if(!_activeOnLoad)
            InverseActive();
    }

    /// <summary>
    /// ������ ��������� ���� �� ���������������
    /// </summary>
    public void InverseActive() => gameObject.SetActive(!gameObject.activeSelf);
}