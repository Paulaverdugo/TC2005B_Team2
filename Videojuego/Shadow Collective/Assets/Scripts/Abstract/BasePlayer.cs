/*
    Script to define the base player class 

    it defines movement, animations, shooting, and other default behaviors
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

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
    private bool skippingLevel = false;

    // to pause the game
    [System.NonSerialized] 
    public GameObject pauseMenu;

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
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.O) && !skippingLevel)
        {
            skippingLevel = true;
            Debug.Log("skipping level");
            skipLevel.EndLevel();
        }

        // if esc is presed, pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
        }

        if (isDying || Time.timeScale == 0) return;

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
                // so that if it damages a guard, it knows the player's pos
                bullet.GetComponent<BulletBehaviour>().SetFromPlayer();
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
        if (isDying) return;
        // Reduce the player's health by the amount of damage taken
        // If the player's health is 0, call the GameOver() function
        health -= damage;
        StartCoroutine(DamageVisualCue());

        print(health);

        if (health <= 0)
        {
            isDying = true;
            StartCoroutine(GameOver());
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.SetHealth(health);
    }

    protected IEnumerator DamageVisualCue()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.075f);
        spriteRenderer.color = Color.white;
    }

    virtual public void GetHealed(float healingAmount)
    {
        health += healingAmount;
        healthBar.SetHealth(health);
    }

    protected IEnumerator GameOver() 
    {
        // Add a death to our stats
        StartCoroutine(AddDeath());

        // Leave the player with only one gadget
        while (activeGadgets.Count > 1)
        {
            BaseGadget gadgetToDelete = activeGadgets[Random.Range(0, activeGadgets.Count - 1)];
            StartCoroutine(RemoveGadget(gadgetToDelete));

            activeGadgets.Remove(gadgetToDelete);
        }

        StartCoroutine(ResetLevelAchieved());

        // Play death animation
        animator.SetTrigger("death");

        yield return new WaitForSeconds(1);

        DestroyImmediate(gameObject);
        SceneManager.LoadScene("Death");
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

            ShortGadgetList shortGadgetList = JsonUtility.FromJson<ShortGadgetList>(saveJson);

            // for all of the gadgets stored in playerprefs, look for them in possibleGadgets and add them to activeGadgets
            foreach (ShortGadget gadget in shortGadgetList.gadgets)
            {
                foreach (BaseGadget possibleGadget in possibleGadgets)
                {
                    if (gadget.gadget_id == possibleGadget.gadget_id)
                    {
                        activeGadgets.Add(possibleGadget);
                        possibleGadget.StartGadget();
                    }
                }
            }
        } 
    }

    protected IEnumerator AddDeath()
    {
        Death death = new Death();

        // populate the death object
        death.user_name = PlayerPrefs.GetString("user_name");
        death.player_type = PlayerPrefs.GetInt("player_type_number");

        switch(SceneManager.GetActiveScene().name)
        {
            case "Level1":
                death.level_death = 1;
                break;
            case "Level2":
                death.level_death = 2;
                break;
            case "LevelB":
                death.level_death = 3;
                break;
            default: // we shouldn't reach here
                death.level_death = 0;
                break;
        }

        string jsonDeath = JsonUtility.ToJson(death);

        string ep = ApiConstants.URL + "/event/addDeath";
        // even though the API is a post, we use webrequest's put and later define the method as post
        using (UnityWebRequest www = UnityWebRequest.Put(ep, jsonDeath))
        {
            //UnityWebRequest www = UnityWebRequest.Post(url + getUsersEP, form);
            // Set the method later, and indicate the encoding is JSON
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error creating a death: " + www.error);
            }
        }
    }

    protected IEnumerator RemoveGadget(BaseGadget gadget)
    {
        ChosenGadget chosenGadget = new ChosenGadget();

        chosenGadget.gadget_id = gadget.gadget_id;
        chosenGadget.progress_id = PlayerPrefs.GetInt("id_progress");

        string jsonChosenGadget = JsonUtility.ToJson(chosenGadget);

        string ep = ApiConstants.URL + "/gadget/deleteChosenGadget";
        // even though the API is a post, we use webrequest's put and later define the method as post
        using (UnityWebRequest www = UnityWebRequest.Put(ep, jsonChosenGadget))
        {
            //UnityWebRequest www = UnityWebRequest.Post(url + getUsersEP, form);
            // Set the method later, and indicate the encoding is JSON
            www.method = "DELETE";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error deleting a gadget: " + www.error);
            }
        }
    }

    protected IEnumerator ResetLevelAchieved()
    {
        LevelAchieved levelAchieved = new LevelAchieved();

        // populate the levelAchieved object
        levelAchieved.id_progress = PlayerPrefs.GetInt("id_progress");
        levelAchieved.level_achieved = 1;

        string jsonLevelAchieved = JsonUtility.ToJson(levelAchieved);

        string ep = ApiConstants.URL + "/progress/updateLevel";

        // even though the API is a patch, we use webrequest's put and later define the method as post
        using (UnityWebRequest www = UnityWebRequest.Put(ep, jsonLevelAchieved))
        {
            // Set the method later, and indicate the encoding is JSON
            www.method = "PATCH";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error updating the level achieved: " + www.error);
            }
        }
    }
}
