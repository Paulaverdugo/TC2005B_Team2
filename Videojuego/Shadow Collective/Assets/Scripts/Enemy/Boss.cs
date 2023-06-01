/*
    Script that defines behavior for the boss enemy.

    It borrows a lot from the guard enemy, but it inherits from base enemy since it's more principled and clean that way.

    It needs to be a child from BaseEnemy since many of the player's gadgets depend on this class to work.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss : BaseEnemy
{
    [SerializeField] float maxSpeed = 1.5f;
    [SerializeField] float maxHealth = 25;
    private float health;

    // to control the animations
    [SerializeField] Animator animator;

    // keeps track of where the guard is looking
    private bool lookingRight = true;

    // to not do anything if the guard is dying
    private bool isDying = false;

    private float timeSinceLastShot = 0;

    //Bullet values
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletDamage = 2;
    private float shootOffset = 1.75f;

    // Value to flip the sprite
    private Rigidbody2D rb;

    // Values for the healthbar
    private EnemyHealthBar enemyHealthBar;

    // to control the boss' movement
    private AIPath aiPath;

    // to control the map
    [SerializeField] GameObject toxicHalf;
    [SerializeField] GameObject toxicFull;
    [SerializeField] GameObject toxicBridge;

    override protected void Start()
    {
        // make the sprite used to see the gameobject invisible, since we have animations
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        // Get the Healthbar
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();

        // Set the healthbar 
        enemyHealthBar.SetMaxHealth(maxHealth);

        // Get the rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();

        // Get the spriteRenderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // Get the AIPath
        aiPath = gameObject.GetComponent<AIPath>();

        aiPath.maxSpeed = maxSpeed;

        health = maxHealth;

        InvokeRepeating("StartBulletHell", 5f, 10f);
    }

    override protected void Update()
    {
        if (player.transform.position.x - gameObject.transform.position.x >= 0)
        {
            LookRight();
        }
        else
        {
            LookLeft();
        }

        // 3 is the distance at which the boss starts running
        animator.SetBool("isRunning", aiPath.desiredVelocity.magnitude > 0);

        Shoot();
    }
    
    void Shoot()
    {
        timeSinceLastShot += Time.deltaTime;

        // check if the player is in sight
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, Mathf.Infinity, raycastLayer);
        if(hit.collider != null && GameObject.ReferenceEquals(hit.collider.gameObject, player))
        {
            // checking the visibility has to be done after checking if there is line of sight, since there is a gadget that can hack you when you make this call
            if (playerController.CheckVisibility(gameObject))
            {
                if (timeSinceLastShot > 0.75f)
                {
                    animator.SetTrigger("shoot2");
                    timeSinceLastShot = 0;

                    Vector3 shootingOrigin = gameObject.transform.position - new Vector3(0, 0.5f, 0);

                    Vector3 direction = new Vector3(player.transform.position.x - shootingOrigin.x, player.transform.position.y - shootingOrigin.y, 0).normalized;


                    // Change position of shooting point based on the player position.
                    Vector3 launchPosition = shootingOrigin + direction * shootOffset;

                    // get the rotation of the bullet gameobject
                    float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    
                    GameObject bullet = Instantiate(bulletPrefab, launchPosition, Quaternion.Euler(0f, 0f, rotZ));
                    bullet.GetComponent<BulletBehaviour>().SetDamage(bulletDamage);
                }
            }
        }
    }

    private void StartBulletHell()
    {
        StartCoroutine(BulletHell());
    }

    private IEnumerator BulletHell()
    {
        aiPath.maxSpeed = 0;
        animator.SetTrigger("shoot1");
        animator.SetBool("bulletHell", true);

        Vector3 shootingOrigin = gameObject.transform.position - new Vector3(0, 0.5f, 0);
        
        for (int angle = 0; angle < 720; angle += 15)
        {
            Vector3 direction = new Vector3(Mathf.Cos(angle / Mathf.Rad2Deg), Mathf.Sin(angle / Mathf.Rad2Deg), 0).normalized;

            // Change position of shooting point based on the player position.
            Vector3 launchPosition = shootingOrigin + direction * shootOffset;
            
            GameObject bullet = Instantiate(bulletPrefab, launchPosition, Quaternion.Euler(0f, 0f, angle));
            bullet.GetComponent<BulletBehaviour>().SetDamage(bulletDamage); 

            yield return new WaitForSeconds(0.05f);
        }

        animator.SetBool("bulletHell", false);
        aiPath.maxSpeed = maxSpeed;
    }

    public void GetDamaged(float damage)
    {
        health -= damage;
        enemyHealthBar.SetHealth(health);

        if (health <= 0)
        {
            aiPath.maxSpeed = 0;
            animator.SetTrigger("death");
            Die();
        } 
        // phase
        else if (health == Mathf.Ceil(maxHealth / 2))
        {
            toxicHalf.SetActive(false);
            toxicFull.SetActive(true);
        }
    }

    override public void Die()
    {
        toxicBridge.SetActive(false);
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(1f);
        DestroyImmediate(gameObject);
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

    // override functions the boss doesn't use from base enemy
    override protected void UpdateVisionCone(Vector3 direction3) {}
    override public void Alert(Vector3 playerPos) {}
    override protected void UnAlert() {}
    override public void AlertOthers(Vector3 playerPos) {}
    override public void ShowVisionCone() {}
}
