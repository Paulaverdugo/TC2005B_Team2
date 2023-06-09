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
        gadget_id = 7;
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

            List<GameObject> enemiesToDelete = new List<GameObject>();

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

            // delete from enemies list the enemies that were destroyed
            foreach (GameObject enemy in enemiesToDelete)
            {
                player.enemies.Remove(enemy);
            }
        }
    }
}
