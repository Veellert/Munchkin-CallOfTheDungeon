using UnityEngine;

namespace Assets.PickUpSystem
{
    /// <summary>
    /// Компонент отвечающий за подбор сундука
    /// </summary>
    [RequireComponent(typeof(BaseChest))]
    public class PickUpChest : PickUpSystem
    {
        protected override void PickUp()
        {
            foreach (var drop in GetComponent<BaseChest>().GetDrop())
                for (int i = 0; i < drop.Count; i++)
                    Instantiate(drop.Item, Player.Instance.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
