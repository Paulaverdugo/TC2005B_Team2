/*
    This script unifies enemies in a script, so that other gameobjects or scripts that need
    to interact with an enemy can do so without knowing if it's a camera or guard or other.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public BaseEnemy enemyScript;

    public void Alert(Vector3 playerPos)
    {
        enemyScript.Alert(playerPos);
    }

    public void AlertOthers(Vector3 playerPos)
    {
        enemyScript.AlertOthers(playerPos);
    }

    public void Hack(float hackDuration_)
    {
        enemyScript.Hack(hackDuration_);
    }

    public void UnHack()
    {
        enemyScript.UnHack();
    }

    public bool IsGuard()
    {
        return enemyScript.GetType() == typeof(Guard);
    }

    public bool IsBoss()
    {
        return enemyScript.GetType() == typeof(Boss);
    }

    public void Die()
    {
        enemyScript.Die();
    }

    public void ShowVisionCone()
    {
        enemyScript.ShowVisionCone();
    }
}
