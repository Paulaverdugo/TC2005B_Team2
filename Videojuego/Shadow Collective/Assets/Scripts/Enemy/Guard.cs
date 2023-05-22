/*
    Script that defines the behavior for the enemy guard

    This class inherits from the abstract class Base Enemy
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
    
    // to not do anything if the guard is dying
    private bool isDying = false;

    private float timeSinceLastShot = 0;

    private GuardAI guardAI;

    //Bullet values
    [SerializeField] GameObject bulletPrefab;
    public Transform firePoint;

    // bool that stores if the guard is going to the target or to the startingPos
    bool goingToPatrolTarget = true;

    override protected void Start() 
    {
        base.Start();

        // make the sprite used to see the gameobject invisible, since we have animations
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        // Get the guardAI component
        guardAI = GetComponent<GuardAI>();
        guardAI.enabled = false;

        startingPos = transform.position;
        timeSinceLastShot = 0;
    }

    override protected void Update()
    {
        if (isDying) return;

        base.Update();
        
        if (isHacked) return;

        
        if (isAlerted)
        {
            MoveToPlayer();
            if (timeSinceLastShot > 1)
            {
                Shoot();
                timeSinceLastShot = 0;
            }
        } else
        {
            MovePatrol();
        }
    }

    void MovePatrol() 
    {
        //function that moves the guard in a patrol
        if (patrols)
        {
            animator.SetBool("isRunning", true);
            Vector3 direction, movement;

            if (goingToPatrolTarget)
            {
                direction = patrolTarget - transform.position;
                movement = direction.normalized * speed * Time.deltaTime;


                // check if we are going to pass the target this tick
                if (direction.magnitude < (movement).magnitude)
                {
                    transform.position = patrolTarget;
                    direction = Vector3.zero;

                    goingToPatrolTarget = !goingToPatrolTarget;
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

                    goingToPatrolTarget = !goingToPatrolTarget;
                }
            }

            if (direction.x < 0)
            {
                LookLeft();
            } else
            {
                LookRight();
            }
            UpdateVisionCone(direction.normalized);
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

        guardAI.enabled = true;
        // Vector3 direction = (playerLastPos - transform.position).normalized;

        // UpdateVisionCone(direction);

        // if (direction.x < 0)
        // {
        //     LookLeft();
        // } else
        // {
        //     LookRight();
        // }

        // transform.position += direction * speed * Time.deltaTime;
    }

    void Shoot()
    {
        // TO DO -> IMPLEMENT THE GUARD SHOOTING THE PLAYER
        animator.SetTrigger("shoot");
        timeSinceLastShot += Time.deltaTime;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void GetDamaged(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            Die();
        }
    }

    override public void Alert(Vector3 playerPos)
    {
        base.Alert(playerPos);

        // so that when the alert mode runs out, the player goes back to it's original spot
        goingToPatrolTarget = false;
    }

    private void LookRight()
    {
        if (!lookingRight)
        {
            lookingRight = true;
            spriteRenderer.flipX = false;
        }
    }

    private void LookLeft()
    {
        if (lookingRight)
        {
            lookingRight = false;
            spriteRenderer.flipX = true;
        }
    }

    override public void Hack(float hackDuration_)
    {
        base.Hack(hackDuration_);
        animator.SetBool("isRunning", false);
    }

    override public void Die()
    {
        isDying = true;
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        animator.SetTrigger("die");
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
}
