using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Script to control the bullet behavior
    public float bulletSpeed = 10f;
    public float damage = 1f;
    public bool fromPlayer = false;

    private void Update()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deal damage to the player
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerController>().GetDamaged(damage);
        }

        // Deal damage to the guard
        if (collision.gameObject.CompareTag("Enemy")) {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();

            if (enemyController.IsGuard())
            {
                Guard guardScript = (Guard) enemyController.enemyScript;
                guardScript.GetDamaged(damage, fromPlayer);
            }
            else if (enemyController.IsBoss())
            {
                Boss bossScript = (Boss) enemyController.enemyScript;
                bossScript.GetDamaged(damage);
            }
        }

        if (!collision.gameObject.CompareTag("Bullet")) Destroy(gameObject);
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }

    public void SetSpeed(float speed) 
    {
        this.bulletSpeed = speed;
    }

    public void SetFromPlayer()
    {
        this.fromPlayer = true;
    }
}
