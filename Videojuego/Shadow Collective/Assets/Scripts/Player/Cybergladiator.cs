/*
    Script that defines the behavior of the player Cybergladiator.

    This class inherits from the abstract class BasePlayer.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We are inheriting from abstrcat class BasePlayer

public class Cybergladiator : BasePlayer
{
    // how long the shield class ability last
    [SerializeField] float shieldDuration = 7;

    // how long before player can shield again
    [SerializeField] float shieldCooldown = 7;

    // how long the player has been shielded
    float shieldTimer = 0;
    float cooldownTimer;
    bool shielding = false;

    // for the shield animation
    GameObject shieldAnimation;


    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        //Attributes
        health = base.maxHealth;
        maxSpeed = 5;
        damage = 1;

        cooldownTimer = shieldCooldown;

        shieldAnimation = gameObject.transform.Find("Shield").gameObject;

        // to test gadgets
        possibleGadgets = new List<BaseGadget>()
        {
            new CyberDash(this),
            new Overcharge(this),
            new BioStim(this)
        };

        PopulateActiveGadgets();

        // for testing
        activeGadgets.Add(possibleGadgets[0]);
        activeGadgets[0].StartGadget();
        activeGadgets.Add(possibleGadgets[1]);
        activeGadgets[1].StartGadget();
        activeGadgets.Add(possibleGadgets[2]);
        activeGadgets[2].StartGadget();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        ActivateShield();
    }

    void ActivateShield()
    {
        // if player can be invisible again and they pressed space
        if (Input.GetKey(KeyCode.Space) && !shielding && cooldownTimer >= shieldCooldown)
        {
            shieldAnimation.SetActive(true);

            shielding = true;
            shieldTimer = 0;
        }

        else if (shielding)
        {
            shieldTimer += Time.deltaTime;

            // ability ran out
            if (shieldTimer > shieldDuration)
            {
                shieldAnimation.SetActive(false);
                shielding = false;
                cooldownTimer = 0;
            }
        }

        else
        {
            if (cooldownTimer < shieldCooldown)
            {
                cooldownTimer += Time.deltaTime;
            }
        }
    }


    override public void GetDamaged(float damage)
    {
        print(health);

        if (!shielding)
        {
            health -= damage;
        }

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
}
