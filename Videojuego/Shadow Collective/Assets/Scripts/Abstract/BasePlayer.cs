/*
    Script to define the base player class 


*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BasePlayer : MonoBehaviour
{
    // Base attributes
    [SerializeField] float health;
    [SerializeField] float speed;

    // Base player states
    private bool isVisible;
    private bool canSeeVisionCones;

    // Base player position
    public Vector3 pos;
    
    // Base player gadgets
    private List<Gadget> gadgets;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        isVisible = false;
        gadgets = new List<Gadget>();
        pos = transform.position;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        // Move the player
        Move();
    }

    private void Move()
    {
        // Move the player based on the input
        if (Input.GetKey(KeyCode.W))
        {
            pos += Vector3.up * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.S))
        {
            pos += Vector3.down * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.A))
        {
            pos += Vector3.left * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.D))
        {
            pos += Vector3.right * speed * Time.deltaTime;
        }

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

}
