/*
    Class that inherits to basegadget to define the cybergladiator's cyber dash gadget

    this gadget allows the player to dash, enhancing movement
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberDash : BaseGadget
{
    public CyberDash(BasePlayer player_) : base(player_)
    {

    }

    private float dashCooldown; // how much time the player has to wait between dashes
    private float dashDuration; // how long the dash lasts
    private float dashSpeed; // how fast does the player go when dashing
    private float regularSpeed; // player's usual speed
    private float cooldownTimer; // time elapsed since dashing
    private bool dashing;

    public override void StartGadget()
    {
        keyBinded = KeyCode.LeftShift;
        
        dashCooldown = 2f;
        dashDuration = 0.1f;
        dashSpeed = 30f;
        cooldownTimer = dashCooldown;
        regularSpeed = player.maxSpeed;
        dashing = false;
    }

    public override void UpdateGadget(float deltaTime)
    {
        // if cooldown is over and player is moving
        if (!dashing && cooldownTimer >= dashCooldown && player.currentSpeed != 0)
        {
            if (Input.GetKey(keyBinded))
            {
                cooldownTimer = 0f;
                player.maxSpeed = dashSpeed;
                player.currentSpeed = dashSpeed;
                dashing = true;
            }
        } 
        else 
        {
            cooldownTimer += deltaTime;

            if (dashing && cooldownTimer > dashDuration)
            {
                dashing = false;
                player.maxSpeed = regularSpeed;
                player.currentSpeed = regularSpeed;
            }
        }
    }
}
