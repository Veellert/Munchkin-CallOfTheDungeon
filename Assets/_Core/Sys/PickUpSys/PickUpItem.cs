using UnityEngine;

namespace Assets.PickUpSystem
{
    /// <summary>
    /// Компонент отвечающий за подбор предмета
    /// </summary>
    [RequireComponent(typeof(BaseItem))]
    public class PickUpItem : PickUpSystem
    {
        protected override void PickUp()
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<PSBRenderer>().SetAlpha(0);
            gameObject.transform.SetParent(Player.Instance.transform);

            if (GetComponent<BaseEquipment>() != null)
                AddEquipmentToInventory(GetComponent<BaseEquipment>());
            else
                AddItemToInventory(GetComponent<BaseItem>());
        }
    }
}
