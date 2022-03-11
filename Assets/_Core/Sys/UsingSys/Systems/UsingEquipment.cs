namespace Assets.UsingSystem
{
    /// <summary>
    /// Коспонент отвечающий за логику использования экипировки
    /// </summary>
    public class UsingEquipment : UsingSystem
    {
        private BaseEquipment _equipment;

        private void Awake()
        {
            _equipment = GetComponent<BaseEquipment>();
        }

        public override void Use()
        {
            _equipment.UnEquipCurrent();
            _equipment.EquipOn();
        }
    }
}
