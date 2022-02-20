using System;
using UnityEngine;

/// <summary>
/// Компонент отвечающий за логику использования предмета с помощью броска
/// </summary>
public class ThrowUseByPlayer : MonoBehaviour
{
    //===>> Constants <<===\\

    private const float PotionRange = 4;
    private const float Speed = 2.5f;

    //===>> Components & Fields <<===\\

    /// <summary>
    /// Событие при взятии в руку
    /// </summary>
    private event Action OnPickUp;
    /// <summary>
    /// Событие во время броска
    /// </summary>
    private event Action UntilThrow;
    /// <summary>
    /// Событие при конце броска
    /// </summary>
    private event Action OnThrowed;

    private BaseMonster _target;
    private Vector2 _throwPosition;

    //===>> Unity <<===\\

    private void Start()
    {
        OnPickUp += OnItemPickUp;
        OnThrowed += OnItemThrowed;
    }

    private void FixedUpdate()
    {
        UntilThrow?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPickUp?.Invoke();
        }
    }

    //===>> On Events <<===\\

    /// <summary>
    /// Событие при коце броска
    /// </summary>
    private void OnItemThrowed()
    {
        GetComponent<BasePotion>().UsePotionOn(_target);
    }

    /// <summary>
    /// Событие при взятии в руку
    /// </summary>
    private void OnItemPickUp()
    {
        OnPickUp -= OnItemPickUp;
        transform.SetParent(Player.Instance.transform);
        UntilThrow += ChasePlayer;
        InputObserver.Instance.OnRightMouseButton += ThrowItem;
    }

    /// <summary>
    /// Событие во время броска
    /// </summary>
    private void ThrowItem()
    {
        UntilThrow -= ChasePlayer;
        InputObserver.Instance.OnRightMouseButton -= ThrowItem;

        transform.SetParent(null);
        _throwPosition = InputObserver.GetMousePosition();
        _target = BaseMonster.GetClosest(_throwPosition, PotionRange.TileHalfed());

        UntilThrow += ThrowTo;
    }

    //===>> Private & Protected Methods <<===\\

    /// <summary>
    /// Преследует мышку игрока
    /// </summary>
    private void ChasePlayer()
    {
        transform.position = (Vector2)Player.Instance.transform.position + Direction2D.GetMouseDirection() * new TileHalf();
    }

    /// <summary>
    /// Бросок
    /// </summary>
    private void ThrowTo()
    {
        transform.position = Vector2.MoveTowards(transform.position, _throwPosition, Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _throwPosition) <= new TileHalf() ||
            (_target && Vector2.Distance(transform.position, _target.transform.position) <= new TileHalf()))
            ApproveThrow();
    }

    /// <summary>
    /// Бросок попал в цель
    /// </summary>
    private void ApproveThrow()
    {
        UntilThrow -= ThrowTo;
        OnThrowed?.Invoke();
    }
}
