namespace Assets.UsingSystem
{
    /// <summary>
    /// Коспонент отвечающий за логику моментального использования зелья
    /// </summary>
    public class InstantUseOnPlayer : UsingPotion
    {
        public override void Use()
        {
            _potion.UsePotionOn(Player.Instance);
        }
    }
}
