/*
    Script for the Ghostwalker gadget Ghost Vision

    Ghost vision allows the player to see vision cones
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostVision : BaseGadget
{
    public GhostVision(BasePlayer player_) : base(player_)
    {
        hasBeenActivated = false;
    }

    private bool hasBeenActivated;

    override public void ResetGadget()
    {

    }

    override public void UpdateGadget(float deltaTime)
    {
        if (!hasBeenActivated)
        {
            hasBeenActivated = true;
            foreach (GameObject enemy in player.enemies)
            {
                enemy.GetComponent<EnemyController>().ShowVisionCone();
            }
        }
    }
}
