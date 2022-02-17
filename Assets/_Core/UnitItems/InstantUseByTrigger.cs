using UnityEngine;

public class InstantUseByTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            var potion = GetComponent<BasePotion>();

            potion.UsePotionOn(Player.Instance);
        }
    }
}
