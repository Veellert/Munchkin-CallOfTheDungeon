using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    /// <summary>
    /// Компонент отображающий оверлей игрока
    /// </summary>
    public class PlayerOverlayDisplayer : MonoBehaviour
    {
        [Header("Image For Light")]
        [SerializeField] private Image _lightMap;

        [Header("Info Text Labels")]
        [SerializeField] private Text _monsterCounterText;
        [SerializeField] private Text _dropCounterText;
        [SerializeField] private Text _lightLevelText;

        [Header("Minimap Object")]
        [SerializeField] private GameObject _minimap;
        [Header("Menues")]
        [SerializeField] private MenuTemplate _pauseMenu;
        [SerializeField] private CheatHandler _cheatMenu;

        private void Start()
        {
            InputObserver.Instance._minimap.OnButtonUse += OnMinimapActiveChanged;
            InputObserver.Instance._pauseMenu.OnButtonUse += OnPauseClick;
            InputObserver.Instance._cheatPanel.OnButtonUse += OnCheatClick;

            Monster.MonstersCount.OnValueChanged += Count_OnValueChanged;
            Player.Instance.Inventory.DropCrystals.OnValueChanged += Drop_OnValueChanged;

            DisplayOnce();
        }

        private void OnDestroy()
        {
            InputObserver.Instance._minimap.OnButtonUse -= OnMinimapActiveChanged;
            InputObserver.Instance._pauseMenu.OnButtonUse -= OnPauseClick;
            InputObserver.Instance._cheatPanel.OnButtonUse -= OnCheatClick;

            Monster.MonstersCount.OnValueChanged -= Count_OnValueChanged;
            Player.Instance.Inventory.DropCrystals.OnValueChanged -= Drop_OnValueChanged;
        }

        /// <summary>
        /// Отображает меню и ставит игру на паузу
        /// </summary>
        public void SetPause()
        {
            _pauseMenu.InverseActive();

            if (_pauseMenu.gameObject.activeSelf)
                InputObserver.Instance.SetGameState(GameState.PauseState());
            else
                InputObserver.Instance.SetGameState(GameState.PlayState());

            Monster.GetMonsters().ForEach(s => s.SetDisableState(_pauseMenu.gameObject.activeSelf));
        }

        /// <summary>
        /// Уменьшает уровень яркости
        /// </summary>
        public void DecreaseLightLevel()
        {
            float level = PlayerPrefs.GetFloat("LightLevel", 0);

            if (TryChangeLightLevel(level, level != 0, () =>
            {
                level -= 0.1f;
                if (level < 0.1f)
                    level = 0;
            }))
                RenderLightInfo(level);

        }

        /// <summary>
        /// Увеличивает уровень яркости
        /// </summary>
        public void IncreaseLightLevel()
        {
            float level = PlayerPrefs.GetFloat("LightLevel", 0);

            if (TryChangeLightLevel(level, level < 0.7f, () => { level += 0.1f; }))
                RenderLightInfo(level);

        }

        /// <summary>
        /// Отображает всю информацию при загрузке компонента
        /// </summary>
        private void DisplayOnce()
        {
            DisplayMonsterCounterText();
            DisplayDropCounterText();
            RenderLightInfo(PlayerPrefs.GetFloat("LightLevel", 0));
        }

        /// <summary>
        /// Событие при нажатии на паузу
        /// </summary>
        /// <param name="obj">Нажатая кнопка</param>
        private void OnPauseClick(InputButton obj) => SetPause();

        /// <summary>
        /// Событие при нажатии на чит
        /// </summary>
        /// <param name="obj">Нажатая кнопка</param>
        private void OnCheatClick(InputButton obj)
        {
            _cheatMenu.SetCheatMode(!_cheatMenu.gameObject.activeSelf);
        }

        /// <summary>
        /// Событие при изменении видимости миникарты
        /// </summary>
        /// <param name="obj">Нажатая кнопка</param>
        private void OnMinimapActiveChanged(InputButton obj)
        {
            _minimap.SetActive(!_minimap.activeSelf);
        }

        /// <summary>
        /// Событие при изменении кол-ва монстров на карте
        /// </summary>
        /// <param name="obj">Отслеживаемый атрибут</param>
        private void Count_OnValueChanged(NumericAttrib obj)
        {
            DisplayMonsterCounterText();
        }

        /// <summary>
        /// Событие при изменении кол-ва дропа монстров
        /// </summary>
        /// <param name="obj">Отслеживаемый атрибут</param>
        private void Drop_OnValueChanged(NumericAttrib obj)
        {
            DisplayDropCounterText();
        }

        /// <summary>
        /// Отображает текст с кол-вом дропа с моснтров
        /// </summary>
        private void DisplayDropCounterText()
        {
            _dropCounterText.text = ": " + Player.Instance.Inventory.DropCrystals.Value;
        }

        /// <summary>
        /// Отображает текст с кол-вом монстров на карте
        /// </summary>
        private void DisplayMonsterCounterText()
        {
            _monsterCounterText.text = ": " + Monster.MonstersCount.Value;
        }

        /// <returns>
        /// Изменился ли уровень яркости
        /// </returns>
        /// <param name="lightLevel">Уровень яркости</param>
        /// <param name="condition">Условие</param>
        /// <param name="changeLevelAction">Действие изменяющее уровень яркости</param>
        private bool TryChangeLightLevel(float lightLevel, bool condition, Action changeLevelAction)
        {
            if (!condition)
                return false;

            ChangeLightLevel(lightLevel, changeLevelAction);

            return true;
        }

        /// <returns>
        /// Изменяет уровень яркости
        /// </returns>
        /// <param name="lightLevel">Уровень яркости</param>
        /// <param name="changeLevelAction">Действие изменяющее уровень яркости</param>
        private void ChangeLightLevel(float lightLevel, Action changeLevelAction)
        {
            changeLevelAction?.Invoke();

            PlayerPrefs.SetFloat("LightLevel", lightLevel);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Отображает уровень яркости
        /// </summary>
        /// <param name="level">Уровень яркости</param>
        private void RenderLightInfo(float level)
        {
            _lightMap.color = new Color(255, 255, 255, level);
            _lightLevelText.text = "Уровень света: " + level;
        }
    }
}
