using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Script to control the bullet behavior
    public float bulletSpeed = 10f;

    private void Update() {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }

}
