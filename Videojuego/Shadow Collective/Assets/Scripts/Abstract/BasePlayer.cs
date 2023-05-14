/*
    Script to define the base player class 


*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BasePlayer : MonoBehaviour
{
    // Base attributes
    protected float health;
    protected float speed;

    // Base player states
    protected bool isVisible = true;
    protected bool canSeeVisionCones = false;

    [SerializeField] public Animator animator;

    // to flip the sprites when going left
    private SpriteRenderer spriteRenderer;

    // keeps track of where the guard is looking
    private bool lookingRight = true; 

    
    // Base player gadgets
    // protected List<Gadget> gadgets; TO DO -> uncomment when Gadget exists

    // Start is called before the first frame update
    virtual protected void Start()
    {
        // gadgets = new List<Gadget>(); TO DO -> uncomment when Gadget exists

        // make the sprite used to see the gameobject invisible, since we have animations
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        // Move the player
        Move();

        if (Input.GetKey(KeyCode.P))
        {
            animator.SetTrigger("shoot");
        }

        if (Input.GetKey(KeyCode.O))
        {
            animator.SetTrigger("death");
        }
    }

    protected void Move()
    {
        Vector3 movement = Vector3.zero;
        // Move the player based on the input
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.up;
        } if (Input.GetKey(KeyCode.S))
        {
            movement += Vector3.down;
        } if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
        } if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
        }

        // animate if moving
        if (movement == Vector3.zero)
        {
            animator.SetBool("isRunning", false);
        } else
        {
            animator.SetBool("isRunning", true);
            
            // if we are moving, check if it's to the left or right
            if (movement.x < 0) 
            {
                LookLeft();
            } else if (movement.x > 0)
            {
                LookRight();
            }
        }

        gameObject.transform.position += movement.normalized * speed * Time.deltaTime;
    }   

    public bool CheckVisibility(GameObject obj)
    {
        // Check if the player is visible to the enemy
        // If the player is visible, set isVisible to true
        // If the player is not visible, set isVisible to false
        return isVisible;
    }

    public void GetDamaged(float damage) {
        // Reduce the player's health by the amount of damage taken
        // If the player's health is 0, call the GameOver() function
        health -= damage;
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
}
