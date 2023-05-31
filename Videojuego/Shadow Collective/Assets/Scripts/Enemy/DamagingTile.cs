using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingTile : MonoBehaviour
{
    // Script to create a tile that damages on entry.

    // The amount of damage to deal.
    public int damage = 1;

    public int damageTimer = 10;
    
    // Create a function that will be called when the player enters the tile.
    void OnTriggerStay2D(Collider2D other)
    {
        // Check if the other object is the player.
        if (other.CompareTag("Player"))
        {
            // If it is, start the damage coroutine.
            StartCoroutine(Damage(other.gameObject));
        }
        if (other.CompareTag("Enemy"))
        {
            // If it is, start the damage coroutine.
            StartCoroutine(Damage(other.gameObject));
        }
    }

    // Define the coroutine that will deal damage.
    private IEnumerator Damage(GameObject player) {
       // Wait for half a second.
       yield return new WaitForSeconds(0.5f);
        
        damageTimer -= 1;

        if(player.CompareTag("Player")) {
            if (damageTimer == 0) {
                // Deal damage to the player.
                player.GetComponent<BasePlayer>().GetDamaged(damage);
                damageTimer = 10;
                yield return new WaitForSeconds(0.5f);
            } 
        } else if(player.CompareTag("Enemy")){
            if (damageTimer == 0) {
                // Deal damage to the enemy.
                player.GetComponent<Guard>().GetDamaged(damage);
               damageTimer = 10;
               yield return new WaitForSeconds(0.5f);
            } 
        }
    }
}
