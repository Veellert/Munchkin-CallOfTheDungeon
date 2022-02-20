using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UI
{
    /// <summary>
    /// Компонент отвечающий за отрисовку миникарты
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class MinimapRenderer : MonoBehaviour
    {
        public static MinimapRenderer Instance { get; set; }

        //===>> Inspector <<===\\

        [Header("Indicator Objects For Minimap")]
        [SerializeField] private List<MinimapIndicatorObject> _indicatorList = new List<MinimapIndicatorObject>();

        //===>> Attributes & Properties <<===\\

        public Camera Self { get; private set; }

        //===>> Components & Fields <<===\\

        private bool _isActive = true;

        //===>> Unity <<===\\

        private void Start()
        {
            Instance = this;
            Instance.Self = GetComponent<Camera>();

            InputObserver.Instance._minimap.OnButtonUse += OnMinimap;
            InputObserver.Instance._minimapIncrease.OnButtonUse += OnMinimapIncrease;
            InputObserver.Instance._minimapDecrease.OnButtonUse += OnMinimapDecrease;
        }

        private void OnDestroy()
        {
            InputObserver.Instance._minimap.OnButtonUse -= OnMinimap;
            InputObserver.Instance._minimapIncrease.OnButtonUse -= OnMinimapIncrease;
            InputObserver.Instance._minimapDecrease.OnButtonUse -= OnMinimapDecrease;
        }

        //===>> Public Methods <<===\\

        /// <summary>
        /// Устанавливает индикатор на миникарту
        /// </summary>
        /// <param name="indicator">Индикатор</param>
        public void SetIndicator(MinimapIndicator indicator)
        {
            if (_indicatorList.Count >= Enum.GetNames(typeof(eMinimapIndicator)).Length)
                SetIndicator(indicator, GetIndicatorObject(indicator.Indicator));
        }
        /// <summary>
        /// Устанавливает индикатор на миникарту
        /// </summary>
        /// <param name="indicator">Индикатор</param>
        /// <param name="indicatorObject">Объект индикатора</param>
        public void SetIndicator(MinimapIndicator indicator, MinimapIndicatorObject indicatorObject)
        {
            Instantiate(indicatorObject, indicator.Position, Quaternion.identity);
        }

        //===>> Private & Protected Methods <<===\\

        /// <returns>
        /// Объект индикатора из коллекции
        /// </returns>
        /// <param name="indicator">Тип индикатора</param>
        private MinimapIndicatorObject GetIndicatorObject(eMinimapIndicator indicator) => _indicatorList.Find(s => s.Indicator == indicator);

        //===>> On Events <<===\\

        /// <summary>
        /// Инверсия визуализации миникарты
        /// </summary>
        /// <param name="obj">Нажатая кнопка</param>
        private void OnMinimap(InputButton obj)
        {
            _isActive = !_isActive;
        }

        /// <summary>
        /// Увеличение масштаба миникарты
        /// </summary>
        /// <param name="obj">Нажатая кнопка</param>
        private void OnMinimapIncrease(InputButton obj)
        {
            if (_isActive && Instance.Self.orthographicSize < 15)
                Instance.Self.orthographicSize += 0.5f;
        }

        /// <summary>
        /// Уменьшение масштаба миникарты
        /// </summary>
        /// <param name="obj">Нажатая кнопка</param>
        private void OnMinimapDecrease(InputButton obj)
        {
            if (_isActive && Instance.Self.orthographicSize > 5)
                Instance.Self.orthographicSize -= 0.5f;
        }
    }
}
