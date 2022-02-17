using System;
using UnityEngine;

public class ThrowUseByPlayer : MonoBehaviour
{
    private const float PotionRange = 4;
    private const float Speed = 2.5f;

    private event Action OnPickUp;
    private event Action UntilThrow;
    private event Action OnThrow;

    private Monster _target;
    private Vector2 _throwPosition;

    private void Start()
    {
        OnPickUp += OnItemPickUp;
        OnThrow += OnItemThrow;
    }

    private void OnItemThrow()
    {
        GetComponent<BasePotion>().UsePotionOn(_target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPickUp?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        UntilThrow?.Invoke();
    }

    private void OnItemPickUp()
    {
        OnPickUp -= OnItemPickUp;
        transform.SetParent(Player.Instance.transform);
        UntilThrow += ChasePlayer;
        InputObserver.Instance.OnRightMouseButton += ThrowItem;
    }

    private void ThrowItem()
    {
        UntilThrow -= ChasePlayer;
        InputObserver.Instance.OnRightMouseButton -= ThrowItem;

        transform.SetParent(null);
        _throwPosition = InputObserver.GetMousePosition();
        _target = Monster.GetClosestMonster(_throwPosition, new TileHalf(PotionRange));

        UntilThrow += ThrowTo;
    }

    private void ChasePlayer()
    {
        transform.position = (Vector2)Player.Instance.transform.position + Direction2D.GetMouseDirection() * new TileHalf();
    }

    private void ThrowTo()
    {
        transform.position = Vector2.MoveTowards(transform.position, _throwPosition, Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _throwPosition) <= new TileHalf() ||
            (_target && Vector2.Distance(transform.position, _target.transform.position) <= new TileHalf()))
            Throw();
    }

    private void Throw()
    {
        UntilThrow -= ThrowTo;
        OnThrow?.Invoke();
    }
}
