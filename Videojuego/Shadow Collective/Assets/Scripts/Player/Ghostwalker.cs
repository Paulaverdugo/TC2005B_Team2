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
        base.Start();

        // Attributes
        health = base.maxHealth;
        maxSpeed = 5;
        damage = 1;

        cooldownTimer = invisibilityCooldown;

        possibleGadgets = new List<BaseGadget>()
        {
            new PhantomStep(this),
            new GhostBlade(this),
            new GhostVision(this)
        };

        PopulateActiveGadgets();

        // for testing
        // activeGadgets.Add(possibleGadgets[0]);
        // activeGadgets[0].StartGadget();
        // activeGadgets.Add(possibleGadgets[1]);
        // activeGadgets[1].StartGadget();
        // activeGadgets.Add(possibleGadgets[2]);
        // activeGadgets[2].StartGadget();
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
            spriteRenderer.color = new Color(1, 1, 1, .5f);
            isVisible = false;
            invisibilityTimer = 0;
        }

        else if (!isVisible)
        {
            invisibilityTimer += Time.deltaTime;

            // ability ran out
            if (invisibilityTimer > invisibilityDuration)
            {
                spriteRenderer.color = new Color(1, 1, 1, 1);
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
