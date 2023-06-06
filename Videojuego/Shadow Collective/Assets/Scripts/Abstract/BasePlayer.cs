/*
    Script to define the base player class 

    it defines movement, animations, shooting, and other default behaviors
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BasePlayer : MonoBehaviour
{
    // Base attributes
    public float health;
    public float maxHealth;
    public float maxSpeed;
    protected float damage;

    [System.NonSerialized]
    public float acceleration, deceleration;

    // used for movement
    public float currentSpeed;
    protected Rigidbody2D rigidbody2d;

    // used for shooting
    protected bool shootButtonPressed;

    // Base player states
    [System.NonSerialized]
    public bool isVisible = true;
    protected bool isDying = false;

    [System.NonSerialized]
    public Animator animator;

    // to flip the sprites when going left
    public SpriteRenderer spriteRenderer;

    // keeps track of where the guard is looking
    protected Camera mainCamera;
    protected bool lookingRight = true;

    // some gadgets and classes need to know where nearby enemies are
    public List<GameObject> enemies = new List<GameObject>();

    // HealthBar 
    public HealthBar healthBar;

    // Get the bullet prefab
    [System.NonSerialized]
    public GameObject bulletPrefab;
    protected float shootCooldown = 0.5f;
    protected float timeSinceLastShot;

    // Base player gadgets
    protected List<BaseGadget> activeGadgets;
    protected List<BaseGadget> possibleGadgets;

    // to show which class is active
    [SerializeField] public Sprite activeClassSkin;

    // to skip the level for testing and showcasing -> added by player controller
    [System.NonSerialized]
    public LevelEnd skipLevel;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        activeGadgets = new List<BaseGadget>();

        // make the sprite used to see the gameobject invisible, since we have animations
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        healthBar.SetMaxHealth(health);
        timeSinceLastShot = shootCooldown;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        // skip current level for testing and showcasing
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.O))
        {
            print("skip level");
            skipLevel.EndLevel();
        }

        if (isDying) return;

        // Move the player
        Move();
        Shoot();
        FaceMouse();

        foreach (BaseGadget gadget in activeGadgets)
        {
            gadget.UpdateGadget(Time.deltaTime);
        }
    }

    protected void Move()
    {
        // get player's input
        Vector3 movementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        // with the player's input, calculate the speed
        if (movementDirection.magnitude > 0) // if we are moving
        {
            animator.SetBool("isRunning", true);
            currentSpeed += acceleration * Time.deltaTime;
        }
        else // not moving
        {
            animator.SetBool("isRunning", false);
            currentSpeed -= deceleration * Time.deltaTime;
        }
        // speed can't be lower than zero or bigger than the max
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        rigidbody2d.velocity = currentSpeed * movementDirection.normalized;
    }

    protected void Shoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetAxisRaw("Fire1") > 0 && timeSinceLastShot >= shootCooldown)
        {
            if (!shootButtonPressed)
            {
                timeSinceLastShot = 0;
                animator.SetTrigger("shoot");
                shootButtonPressed = true;

                Vector3 shootingOrigin = gameObject.transform.position - new Vector3(0, 0.5f, 0);

                Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                Vector3 direction = new Vector3(mousePos.x - shootingOrigin.x, mousePos.y - shootingOrigin.y, 0).normalized;

                // Make launchposition change based on the rotation of the player
                float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // we add the direction * offset to not hit own player's hitbox
                Vector3 launchPosition = shootingOrigin + direction * 0.9f;


                float tmpDamage = damage;

                foreach (BaseGadget gadget in activeGadgets)
                {
                    tmpDamage *= gadget.DamageMultiplier();
                }

                // Create the bullet
                GameObject bullet = Instantiate(bulletPrefab, launchPosition, Quaternion.Euler(0f, 0f, rotZ));
                bullet.GetComponent<BulletBehaviour>().SetDamage(tmpDamage);
            }
        }
        else
        {
            if (shootButtonPressed)
            {
                shootButtonPressed = false;
            }
        }
    }

    virtual public bool CheckVisibility(GameObject enemy)
    {
        // Check if the player is visible to the enemy
        // If the player is visible, set isVisible to true
        // If the player is not visible, set isVisible to false
        return isVisible;
    }

    virtual public void GetDamaged(float damage)
    {
        // Reduce the player's health by the amount of damage taken
        // If the player's health is 0, call the GameOver() function
        health -= damage;

        print(health);

        if (health <= 0)
        {
            StartCoroutine(GameOver());
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.SetHealth(health);
    }

    virtual public void GetHealed(float healingAmount)
    {
        health += healingAmount;
        healthBar.SetHealth(health);
    }

    protected IEnumerator GameOver() {
        // Play death animation
        animator.SetTrigger("death");
        isDying = true;

        yield return new WaitForSeconds(1);

        DestroyImmediate(gameObject);
        // spriteRenderer.enabled = true;
        // // Reset the player's health
        // health = maxHealth;
        // healthBar.SetHealth(health);
        // // Reset the player's position
        // transform.position = new Vector3(0, 0, 0);

    }

    private void FaceMouse()
    {
        Vector3 mousePosition = (Vector3)mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // if mouse is to the right of us
        if ((mousePosition - transform.position).x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    protected void PopulateActiveGadgets()
    {
        if (PlayerPrefs.HasKey("gadgets"))
        {
            string saveJson = PlayerPrefs.GetString("gadgets");
            Debug.Log(saveJson);

            ShortGadgetList shortGadgetList = JsonUtility.FromJson<ShortGadgetList>(saveJson);

            Debug.Log("Gadget id in shortGadgetList: " + shortGadgetList.gadgets[0].gadget_id);

            // for all of the gadgets stored in playerprefs, look for them in possibleGadgets and add them to activeGadgets
            foreach (ShortGadget gadget in shortGadgetList.gadgets)
            {
                foreach (BaseGadget possibleGadget in possibleGadgets)
                {
                    if (gadget.gadget_id == possibleGadget.gadget_id)
                    {
                        Debug.Log("Added gadget to active gadgets");
                        activeGadgets.Add(possibleGadget);
                        possibleGadget.StartGadget();
                    }
                }
            }
        } 
    }
}
