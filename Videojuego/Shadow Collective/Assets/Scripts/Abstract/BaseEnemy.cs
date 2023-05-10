/*
    Script to define an abstract class that will be used by enemy classes

    Using abstract classes normalizes behavior so that all enemies have certain attributes or methods
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEnemy : MonoBehaviour
{
    // radius that defines the max distance to alert another enemy when the player is seen
    [SerializeField] GameObject player; 
    [SerializeField] float alertingRadius; 
    [SerializeField] float alertedTimeLimit; 
    [SerializeField] float hackedTimeLimit; 
    
    private bool isAlerted = false;
    private float alertedTime = 0f;

    private bool isHacked = false;
    private float hackedTime = 0f;

    private List<GameObject> inRadius = new List<GameObject>();

    virtual protected void start() 
    {
        gameObject.GetComponent<CircleCollider2D>().radius = alertingRadius;
    }

    virtual protected void update() 
    {
        if (isAlerted) 
        {
            alertedTime += Time.deltaTime;

            if (alertedTime > alertedTimeLimit) isAlerted = false;            
        }

        if (isHacked) 
        {
            hackedTime += Time.deltaTime;

            if (hackedTime > hackedTimeLimit) isHacked = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (GameObject.ReferenceEquals(player, collision.gameObject)) 
        {
            if (player.GetComponent<PlayerController>().playerInstance.CanBeSeen(gameObject)) 
            {
                Alert();
                AlertOthers();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (!inRadius.Contains(col.gameObject)) 
        {
            inRadius.Add(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col) 
    {
        if (inRadius.Contains(col.gameObject)) 
        {
            inRadius.Remove(col.gameObject);
        }
    }

    private void Alert() 
    {
        isAlerted = true;
        alertedTime = 0f;
    }

    private void AlertOthers() 
    {
        for (int i = 0; i < inRadius.Count; i++) 
        {
            if (inRadius[i].CompareTag("enemy"))
            {
                inRadius[i].GetComponent<EnemyController>().enemyInstance.Alert();
            }
        }
    }

    private void Hack()
    {
        isHacked = true;
        hackedTime = 0f;
    }
}
