/*
    Class for the cybergladiator gadget Bio-Stim

    Bio-Stim allows the player to heal once 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioStim : BaseGadget
{
    public BioStim(BasePlayer player_) : base(player_)
    {
        keyBinded = KeyCode.E;

        hasBeenUsed = false;
        healingAmount = 5f;
        maxHealth = player.health;
        animationDuration = 1f;
        animationTimer = 0f;
    }

    private bool hasBeenUsed;
    private float healingAmount; 
    private float maxHealth;
    private float animationDuration;
    private float animationTimer;

    public override void ResetGadget()
    {
        
    }

    public override void UpdateGadget(float deltaTime)
    {
        if (!hasBeenUsed && Input.GetKey(keyBinded))
        {
            hasBeenUsed = true;
            // player can't have more hps than the hp amount they start with
            player.health = Mathf.Min(player.health + healingAmount, maxHealth);

            // green flash to make it look like it's healing
            player.spriteRenderer.color = new Color(0, 1, 0, 1);
        } 

        else if (hasBeenUsed && animationTimer < animationDuration)
        {
            animationTimer += deltaTime;
            
            if (animationTimer >= animationDuration)
            {
                player.spriteRenderer.color = new Color(1, 1, 1, 1);
            }
            else
            {
                player.spriteRenderer.color = new Color(
                    Mathf.Lerp(0,1, animationTimer / animationDuration), 
                    1, 
                    Mathf.Lerp(0, 1, animationTimer / animationDuration), 
                    1
                );
            }
        }
    }
}
