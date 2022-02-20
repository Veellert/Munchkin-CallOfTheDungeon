using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику курсора
/// </summary>
public class PlayerCrosshair : MonoBehaviour
{
    //===>> Unity <<===\\

    private void Start()
    {
        Activate();
        Player.Instance.HP.OnValueChanged += OnHPChanged;
    }

    private void FixedUpdate()
    {
        transform.position = Input.mousePosition;
    }

    private void OnDestroy()
    {
        Disable();
        Player.Instance.HP.OnValueChanged -= OnHPChanged;
    }

    //===>> Public Methods <<===\\

    /// <summary>
    /// Отключает игровой курсор
    /// </summary>
    public void Disable()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Включает игровой курсор
    /// </summary>
    public void Activate()
    {
        Cursor.visible = false;
        gameObject.SetActive(true);
    }

    //===>> On Events <<===\\

    /// <summary>
    /// Событие при изменении здоровья
    /// </summary>
    /// <param name="hp">Отслеживаемый атрибут</param>
    private void OnHPChanged(NumericAttrib hp)
    {
        if (hp.IsValueEmpty())
            Disable();
    }
}
