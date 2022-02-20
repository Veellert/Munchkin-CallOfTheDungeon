using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику полосы здоровья
/// </summary>
public class HealthBar : MonoBehaviour
{
    //===>> Inspector <<===\\

    [Header("Health Bar Object")]
    [SerializeField] private Transform _indicator;
    [Header("Indicator & It's Color")]
    [SerializeField] private SpriteRenderer _indicatorSprite;
    [SerializeField] private Color _indicatorColor = Color.red;

    //===>> Attributes & Properties <<===\\

    private float HpPercent => _attrib.HP.Value / _attrib.HP.MaxValue;

    //===>> Components & Fields <<===\\

    private Transform _target;
    private IDamageable _attrib;

    //===>> Unity <<===\\

    private void Start()
    {
        ChangeIndicatorColor();

        _target = transform.parent;
        InitializeHPBar();
    }

    //===>> Private & Protected Methods <<===\\

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

    /// <returns>
    /// Обновленный размер индикатора
    /// </returns>
    /// <param name="indicatorScale">Старый размер индикатора</param>
    private Vector2 UpdateIndicatorScale(Vector2 indicatorScale) => new Vector2(HpPercent, indicatorScale.y);

    //===>> On Events <<===\\

    /// <summary>
    /// Обновляет индикатор
    /// </summary>
    /// <param name="hp">Отслеживаемый атрибут</param>
    private void UpdateIndicator(NumericAttrib hp)
    {
        if (!_indicator)
            return;

        _indicator.localScale = UpdateIndicatorScale(_indicator.localScale);

        if (_attrib.HP.IsValueEmpty())
            gameObject.SetActive(false);
        else if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }
}
