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

    bool _isLoaded = false;

    private void Start()
    {
        Monster.MonstersCount.OnValueChanged += Count_OnValueChanged;

        _lobbyButton.interactable = false;
        DisplayMonsterCounterText();
    }

    private void OnDestroy()
    {
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
