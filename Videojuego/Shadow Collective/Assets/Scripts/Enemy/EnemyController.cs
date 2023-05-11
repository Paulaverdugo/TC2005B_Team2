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

    public void Alert()
    {
        enemyScript.Alert();
    }

    public void AlertOthers()
    {
        enemyScript.AlertOthers();
    }

    public void Hack()
    {
        enemyScript.Hack();
    }
}
