/*
    Class that inherits to basegadget to define the cybergladiator's cyber dash gadget

    this gadget allows the player to dash, enhancing movement
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberDash : BaseGadget
{
    public CyberDash(BasePlayer player_, KeyCode keyBinded_, float dashCooldown_) : base(player_, keyBinded_)
    {
        dashCooldown = dashCooldown_;

        dashDuration = 0.12f;
        dashSpeed = 30f;
        cooldownTimer = dashCooldown;
        regularSpeed = player.maxSpeed;
        dashing = false;
    }

    private float dashCooldown; // how much time the player has to wait between dashes
    private float dashDuration; // how long the dash lasts
    private float dashSpeed; // how fast does the player go when dashing
    private float regularSpeed; // player's usual speed
    private float cooldownTimer; // time elapsed since dashing
    private bool dashing;

    public override void ResetGadget()
    {
        
    }

    public override void UpdateGadget(float deltaTime)
    {
        // if cooldown is over
        if (!dashing && cooldownTimer >= dashCooldown)
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
