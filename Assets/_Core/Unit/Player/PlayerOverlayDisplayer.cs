using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Компонент отображающий оверлей игрока
/// </summary>
public class PlayerOverlayDisplayer : MonoBehaviour
{
    [SerializeField] private Image _lightMap;

    [SerializeField] private Text _monsterCounterText;
    [SerializeField] private Text _dropCounterText;
    [SerializeField] private Text _lightLevelText;

    [SerializeField] private GameObject _minimap;
    [SerializeField] private MenuTemplate _pauseMenu;
    [SerializeField] private CheatHandler _cheatMenu;

    private void Start()
    {
        InputObserver.Instance.minimap.OnButtonEvent += Minimap_OnActiveChanged;
        InputObserver.Instance.pauseMenu.OnButtonEvent += OnPause;
        InputObserver.Instance.cheatPanel.OnButtonEvent += OnCheat;

        Monster.MonstersCount.OnValueChanged += Count_OnValueChanged;
        Player.Instance.DropCrystals.OnValueChanged += Drop_OnValueChanged;

        DisplayMonsterCounterText();
        _dropCounterText.text = ": 0";

        _lightMap.color = new Color(255, 255, 255, PlayerPrefs.GetFloat("LightLevel", 0));
        _lightLevelText.text = "Уровень света: " + PlayerPrefs.GetFloat("LightLevel", 0);
    }

    private void OnDestroy()
    {
        InputObserver.Instance.minimap.OnButtonEvent -= Minimap_OnActiveChanged;
        InputObserver.Instance.pauseMenu.OnButtonEvent -= OnPause;
        InputObserver.Instance.cheatPanel.OnButtonEvent -= OnCheat;

        Monster.MonstersCount.OnValueChanged -= Count_OnValueChanged;
        Player.Instance.DropCrystals.OnValueChanged -= Drop_OnValueChanged;
    }

    private void OnPause(object sender, System.EventArgs e)
    {
        SetPause();
    }

    private void OnCheat(object sender, System.EventArgs e)
    {
        _cheatMenu.SetCheatMode(!_cheatMenu.gameObject.activeSelf);
    }
    
    /// <summary>
    /// Событие при изменении видимости миникарты
    /// </summary>
    private void Minimap_OnActiveChanged(object sender, System.EventArgs e)
    {
        _minimap.SetActive(!_minimap.activeSelf);
    }

    /// <summary>
    /// Событие при изменении кол-ва монстров на карте
    /// </summary>
    private void Count_OnValueChanged(object sender, System.EventArgs e)
    {
        DisplayMonsterCounterText();
    }
    
    /// <summary>
    /// Событие при изменении кол-ва дропа монстров
    /// </summary>
    private void Drop_OnValueChanged(object sender, System.EventArgs e)
    {
        DisplayDropCounterText();
    }

    /// <summary>
    /// Отображает текст с кол-вом дропа с моснтров
    /// </summary>
    private void DisplayDropCounterText()
    {
        _dropCounterText.text = ": " + Player.Instance.DropCrystals.Value;
    }

    /// <summary>
    /// Отображает текст с кол-вом монстров на карте
    /// </summary>
    private void DisplayMonsterCounterText()
    {
        if(_monsterCounterText)
            _monsterCounterText.text = ": " + Monster.MonstersCount.Value;
    }

    public void SetPause()
    {
        _pauseMenu.InverseActive();

        if (_pauseMenu.gameObject.activeSelf)
            InputObserver.Instance.SetGameState(eGameState.Pause);
        else
            InputObserver.Instance.SetGameState(eGameState.Play);

        Player.Instance.SetDisableState(_pauseMenu.gameObject.activeSelf);
        Monster.GetMonsters().ForEach(s => s.SetDisableState(_pauseMenu.gameObject.activeSelf));
    }

    public void DecreaseLightLevel()
    {
        float level = PlayerPrefs.GetFloat("LightLevel", 0);
        if (level == 0)
            return;

        level -= 0.1f;
        if (level < 0.1f)
            level = 0;

        PlayerPrefs.SetFloat("LightLevel", level);
        PlayerPrefs.Save();

        _lightMap.color = new Color(255, 255, 255, level);
        _lightLevelText.text = "Уровень света: " + level;
    }

    public void IncreaseLightLevel()
    {
        float level = PlayerPrefs.GetFloat("LightLevel", 0);

        if (level >= 0.7f)
            return;
        level += 0.1f;
        PlayerPrefs.SetFloat("LightLevel", level);
        PlayerPrefs.Save();

        _lightMap.color = new Color(255, 255, 255, level);
        _lightLevelText.text = "Уровень света: " + level;
    }
}
