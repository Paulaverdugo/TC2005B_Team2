/*
    Script to define the player Ghostwalker
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghostwalker : BasePlayer
{
    // how long the invisibility class ability last
    [SerializeField] float invisibilityDuration;

    // how long before player can be invisible again
    [SerializeField] float invisibilityCooldown;

    // how long the player has been invisible
    float invisibilityTimer = 0;
    float cooldownTimer;

    // Start is called before the first frame update
    override protected void Start()
    {
        // Attributes
        health = 1;
        speed = 3;

        cooldownTimer = invisibilityCooldown;

        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        GoInvisible();
    }

    void GoInvisible()
    {
        // if player can be invisible again and they pressed space
        if (Input.GetKey(KeyCode.Space) && isVisible && cooldownTimer >= invisibilityCooldown)
        {
            // make sprite transparent
            spriteRenderer.color = new Color(1,1,1,.5f);
            isVisible = false;
            invisibilityTimer = 0;
        }

        else if (!isVisible)
        {
            invisibilityTimer += Time.deltaTime;

            // ability ran out
            if (invisibilityTimer > invisibilityDuration)
            {
                spriteRenderer.color = new Color(1,1,1,1);
                isVisible = true;
                cooldownTimer = 0;
            }
        }

        else
        {
            if (cooldownTimer < invisibilityCooldown)
            {
                cooldownTimer += Time.deltaTime;
            }
        }
    }
}
