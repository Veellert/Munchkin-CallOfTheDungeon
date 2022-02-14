using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    /// <summary>
    /// Шаблон логики для разных всплывающих меню
    /// </summary>
    public class MenuTemplate : MonoBehaviour
    {
        [Header("Show Menu When Scene Loaded")]
        [SerializeField] protected bool _activeOnLoad;
        [Header("Open/Close Menu Buttons")]
        [SerializeField] protected Button[] _activeButtons;

        protected virtual void Start()
        {
            InitializeActiveButtons();

            if (!_activeOnLoad)
                InverseActive();
        }

        /// <summary>
        /// Меняет видимость меню на противоположную
        /// </summary>
        public void InverseActive() => gameObject.SetActive(!gameObject.activeSelf);

        /// <summary>
        /// Инициализирует кнопки которые меняют видимость меню
        /// </summary>
        protected void InitializeActiveButtons()
        {
            foreach (var button in _activeButtons)
                button.onClick.AddListener(InverseActive);
        }
    }
}