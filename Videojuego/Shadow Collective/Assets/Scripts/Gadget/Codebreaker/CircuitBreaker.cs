/*
    Script for the Codebreaker's gadget circuit breaker

    Circuit breaker is an EMP that disables (by hacking) all enemies in a radius

    Comments: 
    cues for the player:
        - add a sound effect when the gadget is used

    ideas to balance the gadget:
        - the gadget can only be used once per level
        - when the player presses the button, the gadget has to 'charge' x seconds before launching the EMT
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBreaker : BaseGadget 
{
    public CircuitBreaker(BasePlayer player_) : base(player_)
    {
        keyBinded = KeyCode.Q;

        abilityCooldown = 10f;
        abilityRadius = 7f;
        cooldownTimer = abilityCooldown;

        soundPlayer = UnityEngine.Object.Instantiate(Resources.Load("CircuitBreakerPlayer") as GameObject).GetComponent<CircuitBreakerPlayer>();
    }
    
    float abilityCooldown;
    float abilityRadius;
    float cooldownTimer;
    
    CircuitBreakerPlayer soundPlayer;

    override public void ResetGadget()
    {
        
    }

    override public void UpdateGadget(float deltaTime)
    {
        if (cooldownTimer >= abilityCooldown)
        {
            if (Input.GetKey(keyBinded))
            {
                soundPlayer.Play();
                cooldownTimer = 0f;

                Collider2D[] enemies = Physics2D.OverlapCircleAll(player.transform.position, abilityRadius, LayerMask.GetMask("Enemy"));
                foreach (Collider2D enemy in enemies)
                {
                    Codebreaker codebreaker = (Codebreaker) player;
                    codebreaker.Hack(enemy.gameObject);
                }
            }
        } else
        {
            cooldownTimer += deltaTime;
        }
    }
}
