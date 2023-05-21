/*
    Script for the enemy handler

    Used to put all of the enemies in a list for the player class
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<EnemyController>().enemyScript.player = player;
            child.gameObject.tag = "Enemy";
            child.gameObject.layer = LayerMask.NameToLayer("Enemy");
        }

    }
}
