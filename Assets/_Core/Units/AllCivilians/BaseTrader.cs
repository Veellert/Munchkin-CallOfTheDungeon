using System.Collections.Generic;
using UnityEngine;

public class BaseTrader : MonoBehaviour
{
    public static BaseTrader ActiveTrader { get; set; }

    [SerializeField] private List<CostItem> _itemList = new List<CostItem>();
    [SerializeField] private SpriteRenderer _model;

    private void Update()
    {
        if (!Player.Instance)
            return;

        if (Player.Instance.transform.position.x > transform.position.x)
            _model.flipX = true;
        else if (Player.Instance.transform.position.x < transform.position.x)
            _model.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ActiveTrader = this;
            InputObserver.Instance.CollisionTrader();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActiveTrader = null;
            InputObserver.Instance.CollisionTrader();
        }
    }

    public List<CostItem> GetItemList() => _itemList;
}