/*
    Script for the ghostwalker gadget Ghost Blade

    Ghost Blade is a gadget that once per level, allows the player to one-shot an enemy when stealthing
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlade : BaseGadget
{
    public GhostBlade(BasePlayer player_) : base(player_)
    {
        keyBinded = KeyCode.Q;
        
        hasBeenUsed = false;
        abilityRadius = 1.5f;
    }

    private bool hasBeenUsed;
    private float abilityRadius;

    override public void ResetGadget()
    {

    }

    override public void UpdateGadget(float deltaTime)
    {
        if (!hasBeenUsed && Input.GetKey(keyBinded) && !player.isVisible)
        {
            EnemyController closestGuard = null;
            float closestDistance = Mathf.Infinity;

            // can't use overlapcircleall since the enemies colliders for the vision cone affect it and the
            // ability would be wonky
            foreach (GameObject enemy in player.enemies)
            {
                if (enemy == null)
                {
                    player.enemies.Remove(enemy);
                }
                else
                {
                    EnemyController enemyController = enemy.GetComponent<EnemyController>();
                    if (enemyController.IsGuard())
                    {
                        float distance = Vector3.Distance(player.transform.position, enemy.transform.position);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestGuard = enemyController;
                        }
                    }
                }
            }

            if (closestGuard != null && closestDistance <= abilityRadius)
            {
                hasBeenUsed = true;

                closestGuard.Die();
            }
        }
    }
}
