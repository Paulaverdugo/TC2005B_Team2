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
    [SerializeField] float health = 5;

    // [SerializeField] float chaseCountDown;

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

    // Value to flip the sprite
    private Rigidbody2D rb;

    // Values for rotation of bullet
    private Vector3 playerPos;

    // Values for the healthbar
    private EnemyHealthBar enemyHealthBar;

    // bool that stores if the guard is going to the target or to the startingPos
    bool goingToPatrolTarget = true;
    bool returningToStart = false;

    // when the guard is alerted, it can look farther away
    private float alertedVisionMultiplier = 2f;

    override protected void Start()
    {   
        base.Start();

        // Get the Healthbar
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();

        // Set the healthbar 
        enemyHealthBar.SetMaxHealth(health);
        
        // make the sprite used to see the gameobject invisible, since we have animations
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // Get the guardAI component
        guardAI = GetComponent<GuardAI>();
        guardAI.enabled = false;
        guardAI.target = patrolTarget;

        startingPos = transform.position;
        timeSinceLastShot = 1;

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    override protected void Update()
    {
        if (isDying) return;


        base.Update();

        if (isHacked) return;

        if (rb.velocity.x <= 0.1f)
        {
            LookLeft();
        }
        else if (rb.velocity.x >= 0.1f)
        {
            LookRight();
        }

        if (isAlerted)
        {
            MoveToPlayer();
            Shoot();
        } 
        else if (returningToStart)
        {
            Vector3 direction = startingPos - transform.position;

            UpdateVisionCone(direction.normalized);
            if ((transform.position - startingPos).magnitude < 0.5f)
            {
                returningToStart = false;
                guardAI.enabled = false;
                rb.velocity = Vector3.zero;
            }
        }
        else
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
            }
            else
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

            UpdateVisionCone(direction.normalized);
            transform.position += movement;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void MoveToPlayer()
    {
        // function that moves the guard to the last known player position
        animator.SetBool("isRunning", true);
        // playerLastPos = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        guardAI.target = playerLastPos;

        Vector3 direction = (playerLastPos - transform.position).normalized;

        UpdateVisionCone(direction);
    }

    void Shoot()
    {
        timeSinceLastShot += Time.deltaTime;

        // check if the player is in sight
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, sightDistance * alertedVisionMultiplier, raycastLayer);
        if(hit.collider != null && GameObject.ReferenceEquals(hit.collider.gameObject, player))
        {
            // checking the visibility has to be done after checking if there is line of sight, since there is a gadget that can hack you when you make this call
            if (playerController.CheckVisibility(gameObject))
            {
                // alert others if you can see the player
                AlertOthers(player.transform.position);

                if (timeSinceLastShot > 1)
                {
                    animator.SetTrigger("shoot");
                    timeSinceLastShot = 0;

                    Vector3 shootingOrigin = gameObject.transform.position - new Vector3(0, 0.5f, 0);

                    Vector3 direction = new Vector3(player.transform.position.x - shootingOrigin.x, player.transform.position.y - shootingOrigin.y, 0).normalized;


                    // Change position of shooting point based on the player position.
                    Vector3 launchPosition = shootingOrigin + direction * 1f;

                    // get the rotation of the bullet gameobject
                    float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    
                    Instantiate(bulletPrefab, launchPosition, Quaternion.Euler(0f, 0f, rotZ));
                    // bullet.GetComponent<BulletBehaviour>().SetDamage(damage); -> to set the damage of the bullet (default 1)
                }
            }
        }

    }

    virtual public void GetDamaged(float damage)
    {
        health -= damage;
        enemyHealthBar.SetHealth(health);

        if (health <= 0)
        {
            Die();
        }
        
        AlertOthers(player.transform.position);
    }

    override public void Alert(Vector3 playerPos)
    {

        base.Alert(playerPos);

        if (!isHacked)
        {
            // so that when the alert mode runs out, the player goes back to it's original spot
            goingToPatrolTarget = false;
            guardAI.enabled = true;
        }
    }

    override protected void UnAlert()
    {
        base.UnAlert();

        guardAI.target = startingPos;
        returningToStart = true;
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
        guardAI.enabled = false;
        
        // hold the guard in place
        rb.velocity = Vector3.zero; 
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

        animator.SetBool("isRunning", false);
    }

    override public void UnHack()
    {
        base.UnHack();

        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

        if (patrols)
        {
            guardAI.enabled = true;

            if (isAlerted)
            {
                guardAI.target = playerLastPos;
            } else
            {
                guardAI.target = startingPos;
                returningToStart = true;
            }
        }
    }

    override public void Die()
    {
        health = 0;
        isDying = true;
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        animator.SetTrigger("die");
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
        Destroy(enemyHealthBar);
    }
}
