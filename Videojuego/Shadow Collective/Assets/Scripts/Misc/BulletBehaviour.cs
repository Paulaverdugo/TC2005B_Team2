using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Script to control the bullet behavior
    public float bulletSpeed = 10f;
    public float damage = 1f;

    private void Update()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deal damage to the player
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<BasePlayer>().GetDamaged(damage);
        }

        // Deal damage to the guard
        if (collision.gameObject.CompareTag("Enemy")) {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();

            if (enemyController.IsGuard())
            {
                Guard guardScript = (Guard) enemyController.enemyScript;
                guardScript.GetDamaged(damage);
            }
        }

        Destroy(gameObject);
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }
}
