using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    protected Weapon weapon;

    private void Awake()
    {
        weapon = GetComponent<Weapon>();
    }

    public void Shoot()
    {
        weapon?.Shoot();
    }

    public void StopShooting()
    {
        weapon?.StopShooting();
    }
}
