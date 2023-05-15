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
    protected float health;
    protected float maxSpeed;

    [System.NonSerialized]
    public float acceleration, deceleration;

    // used for movement
    protected float currentSpeed;
    protected Rigidbody2D rigidbody2d;

    // used for shooting
    protected bool shootButtonPressed;

    // Base player states
    protected bool isVisible = true;
    protected bool canSeeVisionCones = false;

    [System.NonSerialized] 
    public Animator animator;

    // to flip the sprites when going left
    protected SpriteRenderer spriteRenderer;

    // keeps track of where the guard is looking
    protected Camera mainCamera;
    protected bool lookingRight = true; 

    // some gadgets and classes need to know where nearby enemies are
    public List<GameObject> enemies = new List<GameObject>();

    
    // Base player gadgets
    // protected List<Gadget> gadgets; TO DO -> uncomment when Gadget exists

    // Start is called before the first frame update
    virtual protected void Start()
    {
        // gadgets = new List<Gadget>(); TO DO -> uncomment when Gadget exists

        // make the sprite used to see the gameobject invisible, since we have animations
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        // Move the player
        Move();
        Shoot();
        FaceMouse();
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
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (!shootButtonPressed)
            {
                shootButtonPressed = true;
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

    public bool CheckVisibility(GameObject obj)
    {
        // Check if the player is visible to the enemy
        // If the player is visible, set isVisible to true
        // If the player is not visible, set isVisible to false
        return isVisible;
    }

    virtual public void GetDamaged(float damage) {
        // Reduce the player's health by the amount of damage taken
        // If the player's health is 0, call the GameOver() function
        health -= damage;
    }

    private void FaceMouse()
    {
        Vector3 mousePosition = (Vector3) mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // if mouse is to the right of us
        if ((mousePosition - transform.position).x > 0)
        {
            spriteRenderer.flipX = false;
        } else
        {
            spriteRenderer.flipX = true;
        }
    }
}
