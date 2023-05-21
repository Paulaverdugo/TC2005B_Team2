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

    // angle in radians, measured from the direction, that defines the vision cone's aperture
    [SerializeField] float fov;

    // to control the animations
    [SerializeField] Animator animator;

    // keeps track of where the guard is looking
    private bool lookingRight = true; 
    
    // to not do anything if the guard is dying
    private bool isDying = false;


    // bool that stores if the guard is going to the target or to the startingPos
    bool goingToPatrolTarget = true;

    override protected void Start() 
    {
        base.Start();

        // make the sprite used to see the gameobject invisible, since we have animations
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        startingPos = transform.position;
    }

    override protected void Update()
    {
        if (isDying) return;

        base.Update();
        
        if (isHacked) return;

        
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

    private void UpdateVisionCone(Vector3 direction)
    {
        direction = new Vector2(direction.x, direction.y).normalized;
        float directionAngle = Mathf.Acos(Vector2.Dot(direction, Vector2.right));

        if (direction.y < 0) directionAngle = 2 * Mathf.PI - directionAngle;

        PolygonCollider2D col = gameObject.GetComponent<PolygonCollider2D>();

        col.enabled = false;

        col.points = new[]
        {
            Vector2.zero,
            sightDistance * new Vector2(Mathf.Cos(directionAngle + fov), Mathf.Sin(directionAngle + fov)),
            sightDistance * new Vector2(Mathf.Cos(directionAngle - fov), Mathf.Sin(directionAngle - fov))
        };

        col.enabled = true;
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
        animator.SetTrigger("death");
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
}
