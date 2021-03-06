using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Компонент отвечающий за переход между локациями по триггеру
    /// </summary>
    public class FinishTrigger : MonoBehaviour
    {
        private PlayerLocationController PlayerLocation => Player.Instance.GetComponent<PlayerLocationController>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (BaseMonster.MonstersCount.IsValueEmpty() || PlayerLocation.GetCurrentLocation() == eLocations.Lobby)
                {
                    if (PlayerLocation.GetCurrentLocation() == eLocations.Lobby)
                        Player.Instance.Heal(Player.Instance.HP.MaxValue);
                    PlayerLocation.MoveToNextLocation();
                }
            }
        }
    }
}
