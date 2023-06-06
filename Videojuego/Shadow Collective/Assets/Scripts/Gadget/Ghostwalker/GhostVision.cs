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
        id_gadget = 7;
    }

    private bool hasBeenActivated;

    override public void StartGadget()
    {
        hasBeenActivated = false;
    }

    override public void UpdateGadget(float deltaTime)
    {
        // has to be activated here since the enemies list is populated in the player's Start()
        if (!hasBeenActivated)
        {
            hasBeenActivated = true;
            foreach (GameObject enemy in player.enemies)
            {
                if (enemy == null)
                {
                    player.enemies.Remove(enemy);
                }
                else
                {
                    enemy.GetComponent<EnemyController>().ShowVisionCone();
                }
            }
        }
    }
}
