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
    [SerializeField] LayerMask raycastLayer;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();

        foreach (Transform child in transform)
        {
            EnemyController enemyController = child.gameObject.GetComponent<EnemyController>();
            enemyController.enemyScript.player = player;
            enemyController.enemyScript.playerController = playerController;
            enemyController.enemyScript.raycastLayer = raycastLayer;
            child.gameObject.tag = "Enemy";
            child.gameObject.layer = LayerMask.NameToLayer("Enemy");
        }

    }
}
