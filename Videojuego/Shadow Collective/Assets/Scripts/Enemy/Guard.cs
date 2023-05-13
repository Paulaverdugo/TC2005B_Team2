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
    Vector3 startingPos;
    [SerializeField] Vector3 patrolTarget;
    [SerializeField] bool patrols;
    
    [SerializeField] float speed = 1;
    [SerializeField] float health = 1;

    // to control the animations
    [SerializeField] Animator animator;

    // keeps track of where the guard is looking
    private bool lookingRight = true; 

    // bool that stores if the guard is going to the target or to the startingPos
    bool goingToTarget = true;

    override protected void Start() 
    {
        base.Start();

        // make the sprite used to see the gameobject invisible, since we have animations
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        startingPos = transform.position;
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
            animator.SetBool("isRunning", true);
            Vector3 direction, movement;

            if (goingToTarget)
            {
                direction = patrolTarget - transform.position;
                movement = direction.normalized * speed * Time.deltaTime;


                // check if we are going to pass the target this tick
                if (direction.magnitude < (movement).magnitude)
                {
                    transform.position = patrolTarget;
                    direction = Vector3.zero;

                    goingToTarget = !goingToTarget;
                }

            } else
            {
                direction = startingPos - transform.position;
                movement = direction.normalized * speed * Time.deltaTime;

                // check if we are going to pass the startingPos this tick
                if (direction.magnitude < (movement).magnitude)
                {
                    transform.position = startingPos;
                    direction = Vector3.zero;

                    goingToTarget = !goingToTarget;
                }
            }

            if (direction.x < 0)
            {
                lookLeft();
            } else
            {
                lookRight();
            }

            transform.position += movement;
        } else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void MoveToPlayer()
    {
        // function that moves the guard to the last known player position
        // TO DO -> IMPLEMENT A* PATH FINDING
        animator.SetBool("isRunning", true);

        Vector3 direction = (playerLastPos - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
    }

    void Shoot()
    {
        // TO DO -> IMPLEMENT THE GUARD SHOOTING THE PLAYER
        animator.SetTrigger("shoot");
    }

    void GetDamaged(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            animator.SetTrigger("death");
        }
    }

    private void lookRight()
    {
        if (!lookingRight)
        {
            lookingRight = true;
            transform.Rotate(new Vector3(0f, 180f, 0f));
        }
    }

    private void lookLeft()
    {
        if (lookingRight)
        {
            lookingRight = false;
            transform.Rotate(new Vector3(0f, 180f, 0f));
        }
    }
}
