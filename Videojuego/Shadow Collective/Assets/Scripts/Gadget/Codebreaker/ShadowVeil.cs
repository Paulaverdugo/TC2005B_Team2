/*
    Class for the codebreaker's gadget shadow veil

    Shadow veil hacks the enemy, when they ask if they can see the player
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowVeil : BaseGadget
{
    public ShadowVeil(Codebreaker player_) : base(player_)
    {
        hasBeenUsed = false;
    }

    private bool hasBeenUsed;

    override public void ResetGadget()
    {
        
    }

    override public void UpdateGadget(float deltaTime)
    {

    }

    override public bool CheckVisibility(GameObject enemy)
    {
        if (!hasBeenUsed)
        {   
            hasBeenUsed = true;

            Codebreaker codebreaker = (Codebreaker) player;
            codebreaker.Hack(enemy);
            
            
            // the enemy won't see the player 
            return false;
        }
        else return true;
    }
}
