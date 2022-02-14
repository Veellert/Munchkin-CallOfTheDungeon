namespace Assets.DropSystem
{
    /// <summary>
    /// Система дропа после смерти
    /// </summary>
    public abstract class DropAfterDeath : DropSystem
    {
        protected virtual void OnDestroy()
        {
            if (Player.Instance)
                AddDropToInventory();
        }
    }
}
