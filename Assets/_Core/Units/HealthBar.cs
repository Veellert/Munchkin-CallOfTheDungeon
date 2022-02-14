using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику полосы здоровья
/// </summary>
public class HealthBar : MonoBehaviour
{
    [Header("Health Bar Object")]
    [SerializeField] private Transform _indicator;
    [Header("Indicator & Its Color")]
    [SerializeField] private SpriteRenderer _indicatorSprite;
    [SerializeField] private Color _indicatorColor = Color.red;

    private Transform _target;
    private IDamageable _attrib;
    private float HpPercent => _attrib.HP.Value / _attrib.HP.MaxValue;

    private void Awake()
    {
        ChangeIndicatorColor();

        _target = gameObject.transform.parent;
        InitializeHPBar();
    }

    /// <summary>
    /// Инициализирует индикатор здоровья
    /// </summary>
    private void InitializeHPBar()
    {
        _attrib = _target.GetComponent<IDamageable>();
        _attrib.HP.OnValueChanged += UpdateIndicator;
    }

    /// <summary>
    /// Изменяет цвет индикатора
    /// </summary>
    private void ChangeIndicatorColor()
    {
        if (_indicatorSprite)
            _indicatorSprite.color = _indicatorColor;
    }

    /// <summary>
    /// Обновляет индикатор
    /// </summary>
    /// <param name="hp">Отслеживаемый атрибут</param>
    private void UpdateIndicator(NumericAttrib hp)
    {
        if (!_indicator)
            return;

        _indicator.localScale = UpdateIndicatorScale(_indicator.localScale);

        if (_attrib.IsDead)
            gameObject.SetActive(false);
        else if(!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    /// <returns>
    /// Обновленный размер индикатора
    /// </returns>
    /// <param name="indicatorScale">Старый размер индикатора</param>
    private Vector2 UpdateIndicatorScale(Vector2 indicatorScale) => new Vector2(HpPercent, indicatorScale.y);
}
