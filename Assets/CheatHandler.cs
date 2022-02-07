using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatHandler : MonoBehaviour
{
    public static List<Vector2> FinishPositions { get; private set; }
    public static void SetFinishPosition(Vector2Int originalPosition)
    {
        FinishPositions = new List<Vector2>();

        Vector2 pos = originalPosition + Vector2Int.up;
        pos.y += new TileHalf();

        FinishPositions.Add(pos);
    }

    [Header("Панель")]
    [SerializeField] private GameObject _cheatPanel;
    [Header("Кнопки")]
    [SerializeField] private Button _teleportBut;
    [SerializeField] private Button _killAllBut;
    [SerializeField] private Button _healBut;
    [SerializeField] private Button _forceBut;
    [SerializeField] private Button _immortalBut;
    [SerializeField] private Button _cheatOffBut;

    private bool _isImmortal;
    private bool _isExtraForce;

    private void OnDestroy()
    {
        FinishPositions = new List<Vector2>();
    }

    private void Start()
    {
        _teleportBut.onClick.AddListener(() => TeleportToFinish());
        _killAllBut.onClick.AddListener(() => KillAllMonsters());
        _healBut.onClick.AddListener(() => InstantHeal());
        _forceBut.onClick.AddListener(() => SetExtraForce());
        _immortalBut.onClick.AddListener(() => SetImmortal());
        _cheatOffBut.onClick.AddListener(() => ResetAllCheats());

        ResetAllCheats();
    }

    public void SetCheatMode(bool isCheatMode)
    {
        _cheatPanel.SetActive(isCheatMode);
    }

    private void HP_OnValueChanged(object sender, System.EventArgs e)
    {
        if (Player.Instance.HP.Value < Player.Instance.HP.MaxValue)
            Player.Instance.HP.FillToMax();
    }

    private void ActivateImmortal()
    {
        if (Player.Instance == null)
            return;

        _isImmortal = true;
        Player.Instance.HP.OnValueChanged += HP_OnValueChanged;
    }

    private void DisableImmortal()
    {
        if (Player.Instance == null)
            return;

        _isImmortal = false;
        Player.Instance.HP.OnValueChanged -= HP_OnValueChanged;
    }

    private void ActivateExtraForce()
    {
        if (Player.Instance == null)
            return;

        _isExtraForce = true;
        Player.Instance.Damage.FillEmpty();
        Player.Instance.Damage += 40;
    }

    private void DisableExtraForce()
    {
        if (Player.Instance == null)
            return;

        _isExtraForce = false;
        Player.Instance.Damage.ResetAttribute();
    }

    public void TeleportToFinish()
    {
        if (Player.Instance == null)
            return;

        if (FinishPositions != null && FinishPositions.Count != 0)
            Player.Instance.transform.position = (Vector2)FinishPositions[0];
    }
    
    public void KillAllMonsters()
    {
        var monsterList = new List<Monster>();
        monsterList.AddRange(Monster.GetMonsters());

        foreach (var monster in monsterList)
            monster.GetDamage(monster.HP);
    }
    
    public void InstantHeal()
    {
        if (Player.Instance == null)
            return;

        Player.Instance.HP.FillToMax();
    }

    public void ResetAllCheats()
    {
        if(_isImmortal)
            DisableImmortal();
        if(_isExtraForce)
            DisableExtraForce();
        SetCheatMode(false);
    }

    public void SetImmortal()
    {
        if (!_isImmortal)
            ActivateImmortal();
        else
            DisableImmortal();
    }

    public void SetExtraForce()
    {
        if (!_isExtraForce)
            ActivateExtraForce();
        else
            DisableExtraForce();
    }
}
