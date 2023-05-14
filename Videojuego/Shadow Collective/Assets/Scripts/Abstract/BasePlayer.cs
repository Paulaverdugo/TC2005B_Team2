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

    // Base player states
    private bool isVisible;
    private bool canSeeVisionCones;

    // Base player position
    [SerializeField] Vector3 pos;

    // Base player gadgets
    // private List<Gadget> gadgets; TO DO -> uncomment when Gadget exists

    // Start is called before the first frame update
    virtual protected void Start()
    {
        isVisible = true;
        // gadgets = new List<Gadget>(); TO DO -> uncomment when Gadget exists
    }

    // Update is called once per frame
    virtual protected void Update()
    {
    }

    public bool CheckVisibility(GameObject obj)
    {
        // Check if the player is visible to the enemy
        // If the player is visible, set isVisible to true
        // If the player is not visible, set isVisible to false
        return isVisible;
    }

    public void GetDamaged(float damage)
    {
        // Reduce the player's health by the amount of damage taken
        // If the player's health is 0, call the GameOver() function
        health -= damage;
    }

}
