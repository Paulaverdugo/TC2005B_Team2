/*
    Script for the Codebreaker's gadget Phantom Signal

    Phantom Signal is a gadget that when activated, it leaves a beacon that waits n seconds and
    sends a fake signal alerting enemies towards it
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomSignal : BaseGadget
{
    public PhantomSignal(BasePlayer player_) : base(player_)
    {
        keyBinded = KeyCode.E;

        hasBeenUsed = false;

        abilityRadius = 7f;

        countdownTotal = 5f;
        countdownTimer = 0f;
    }
    private bool hasBeenUsed;

    private float abilityRadius;
    private Vector3 abilityPosition;

    private float countdownTotal;
    private float countdownTimer;


    override public void ResetGadget()
    {
        
    }

    override public void UpdateGadget(float deltaTime)
    {
        if (!hasBeenUsed && Input.GetKey(keyBinded))
        {
            hasBeenUsed = true;
            abilityPosition = player.transform.position;
            countdownTimer = 0f;
        }

        if (hasBeenUsed && countdownTimer < countdownTotal)
        {
            countdownTimer += deltaTime;

            if (countdownTimer >= countdownTotal)
            {
                // could do it iterating through player's list of enemies
                // dont know which is faster
                Collider2D[] enemies = Physics2D.OverlapCircleAll(abilityPosition, abilityRadius, LayerMask.GetMask("Enemy"));
                foreach (Collider2D enemy in enemies)
                {
                    enemy.gameObject.GetComponent<EnemyController>().Alert(abilityPosition);
                }
            }
        }
    }
}
