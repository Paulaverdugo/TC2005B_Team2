/*
    Script that defines the behavior for the enemy guard

    This class inherits from the abstract class Base Enemy
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : BaseEnemy
{
    // to control how the guard will move when not alerted
    [SerializeField] Vector3 startingPos;
    [SerializeField] Vector3 patrolTarget;
    
    [SerializeField] float speed = 1;
    [SerializeField] float health = 1;

    // bool that stores if the guard is going to the target or to the startingPos
    bool goingToTarget = true;
    bool patrols;

    override protected void Start() 
    {
        patrols = startingPos != patrolTarget;
        base.Start();
    }

    override protected void Update()
    {
        base.Update();
        
        if (isAlerted)
        {
            MoveToPlayer();
            Shoot();
        } else
        {
            MovePatrol();
        }

    }

    void MovePatrol() 
    {
        // function that moves the guard in a patrol
        if (patrols)
        {
            Vector3 direction;

            if (goingToTarget)
            {
                direction = (patrolTarget - transform.position).normalized;
            } else
            {
                direction = (startingPos - transform.position).normalized;
            }

            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void MoveToPlayer()
    {
        // function that moves the guard to the last known player position
        // TO DO -> IMPLEMENT A* PATH FINDING
        Vector3 direction = (playerLastPos - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
    }

    void Shoot()
    {
        // TO DO -> IMPLEMENT THE GUARD SHOOTING THE PLAYER
    }

    void GetDamaged(float damage)
    {
        health -= damage;
    }
}
