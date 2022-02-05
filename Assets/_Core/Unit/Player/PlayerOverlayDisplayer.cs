using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��������� ������������ ������� ������
/// </summary>
public class PlayerOverlayDisplayer : MonoBehaviour
{
    [SerializeField] private Text _monsterCounterText;
    [SerializeField] private Button _lobbyButton;
    [SerializeField] private GameObject _minimap;

    bool _isLoaded = false;

    private void Start()
    {
        MinimapController.Instance.OnActiveChanged += Minimap_OnActiveChanged;
        Monster.MonstersCount.OnValueChanged += Count_OnValueChanged;

        _lobbyButton.interactable = false;
        DisplayMonsterCounterText();
    }

    private void OnDestroy()
    {
        MinimapController.Instance.OnActiveChanged -= Minimap_OnActiveChanged;
        Player.Instance.HP.OnValueChanged -= HP_OnValueChanged;
        Monster.MonstersCount.OnValueChanged -= Count_OnValueChanged;
    }

    private void FixedUpdate()
    {
        if(!_isLoaded)
        {
            if(Player.Instance != null)
            {
                Player.Instance.HP.OnValueChanged += HP_OnValueChanged;
                _isLoaded = true;
            }
        }
    }

    /// <summary>
    /// ������� ��� ��������� ��������� ���������
    /// </summary>
    private void Minimap_OnActiveChanged(object sender, System.EventArgs e)
    {
        _minimap.SetActive(!_minimap.activeSelf);
    }

    /// <summary>
    /// ������� ��� ��������� �������� ������
    /// </summary>
    private void HP_OnValueChanged(object sender, System.EventArgs e)
    {
        if (Player.Instance.IsDead)
            _lobbyButton.interactable = true;
    }

    /// <summary>
    /// ������� ��� ��������� ���-�� �������� �� �����
    /// </summary>
    private void Count_OnValueChanged(object sender, System.EventArgs e)
    {
        DisplayMonsterCounterText();
    }

    /// <summary>
    /// ���������� ����� � ���-��� �������� �� �����
    /// </summary>
    private void DisplayMonsterCounterText()
    {
        _monsterCounterText.text = "�������� �������� �� �����: " + Monster.MonstersCount.Value;
    }
}
