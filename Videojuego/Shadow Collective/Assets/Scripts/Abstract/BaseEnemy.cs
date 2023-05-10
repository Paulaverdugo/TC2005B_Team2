/*
    Script to define an abstract class that will be used by enemy classes

    Using abstract classes normalizes behavior so that all enemies have certain attributes or methods
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEnemy : MonoBehaviour
{
    [SerializeField] GameObject player; 
    
    // radius that defines the max distance to alert another enemy when the player is seen
    [SerializeField] float alertingRadius = 10f; 

    [SerializeField] float alertedTimeLimit = 10f; 
    [SerializeField] float hackedTimeLimit = 5f; 
    
    // attributes related to the state of the enemy being alerted of the enemies position
    private bool isAlerted = false;
    private float alertedTime = 0f;

    // attributes related to the state of the enemy being hacked 
    private bool isHacked = false;
    private float hackedTime = 0f;

    // list that stores the gameobjects that are inside the alerting radius of the enemy
    private List<GameObject> inRadius = new List<GameObject>();

    virtual protected void Start() 
    {
        // the circle collider helps to know which enemies are inside the alerting radius
        gameObject.GetComponent<CircleCollider2D>().radius = alertingRadius;
    }

    virtual protected void Update() 
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

    /* 
        function that checks if the gameobject that entered the vision cone is the player
        if it is, then it asks through the playercontroller if it can be seen 
        if it can be seen, we alert ourselves, aswell as other enemies within the radius
    */ 
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (GameObject.ReferenceEquals(player, collision.gameObject)) 
        {
            // if (player.GetComponent<PlayerController>().playerScript.CanBeSeen(gameObject)) 
            // {
            //     Alert();
            //     AlertOthers();
            // } TO DO -> uncomment when PlayerController exists
        }
    }

    // on trigger enter and exit keep track and update the list with gameobjects that are within the radius
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

    // function to be alerted that the player has been seen
    public void Alert() 
    {
        print("Alerted");
        isAlerted = true;
        alertedTime = 0f;
    }

    public void AlertOthers() 
    {
        for (int i = 0; i < inRadius.Count; i++) 
        {
            // if the gameobject is an enemy, alert it through the enemyController
            // if (inRadius[i].CompareTag("enemy"))
            // {
            //     inRadius[i].GetComponent<EnemyController>().enemyScript.Alert();
            // } TO DO -> uncomment when EnemyController exists
        }
    }

    // function to be hacked by the player or by a gadget
    public void Hack()
    {
        isHacked = true;
        hackedTime = 0f;
    }
}
