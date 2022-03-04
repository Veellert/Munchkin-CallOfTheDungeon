using UnityEngine;

namespace Assets.PickUpSystem
{
    /// <summary>
    /// Коспонент отвечающий за подбор зелья
    /// </summary>
    [RequireComponent(typeof(BasePotion))]
    public class PickUpPotion : PickUpItemSystem
    {
        public override void PickUp()
        {
            var a = GetComponent<BasePotion>();
            AddToInventory(a);
        }
    }
}
