namespace Assets.DropSystem
{
    /// <summary>
    /// Дроп кристаллов после смерти
    /// </summary>
    public class CrystalDrop : DropAfterDeath
    {
        public override void AddDropToInventory()
        {
            Player.Instance.Inventory.AddDropCrystals(GetDrop());
        }
    }
}
