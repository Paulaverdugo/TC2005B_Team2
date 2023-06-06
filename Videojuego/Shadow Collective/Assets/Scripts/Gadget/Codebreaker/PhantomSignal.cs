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
        gadget_id = 6;
    }


    bool hasBeenUsed;

    float abilityRadius;
    Vector3 abilityPosition;

    float countdownTotal;
    float countdownTimer;

    GameObject beacon;

    override public void StartGadget()
    {
        keyBinded = KeyCode.E;

        hasBeenUsed = false;

        abilityRadius = 7f;

        countdownTotal = 5f;
        countdownTimer = 0f;

        beacon = UnityEngine.Object.Instantiate(Resources.Load("PhantomSignalBeacon") as GameObject);
        beacon.SetActive(false);
    }

    override public void UpdateGadget(float deltaTime)
    {
        if (!hasBeenUsed && Input.GetKey(keyBinded))
        {
            hasBeenUsed = true;
            abilityPosition = player.transform.position;

            beacon.SetActive(true);
            beacon.transform.position = abilityPosition;

            countdownTimer = 0f;
        }

        if (hasBeenUsed && countdownTimer < countdownTotal)
        {
            countdownTimer += deltaTime;

            // alert enemies
            if (countdownTimer >= countdownTotal)
            {
                beacon.GetComponent<PhantomSignalBeacon>().WaitAndDestroy(5f);
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
