/*
    Script to define the player Codebreaker
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Codebreaker : BasePlayer
{
    [SerializeField] float hackCooldown = 7;
    [SerializeField] float hackingRadius = 5;
    [SerializeField] float hackingDuration = 5;
    [SerializeField] GameObject visualHackTarget;

    private float cooldownTimer;
    private EnemyController hackedEnemy;
    private bool unHacked = true; // keeps track if the enemy has been unhacked after hacking it

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        
        health = 1;
        maxSpeed = 5;
        damage = 1;

        cooldownTimer = hackCooldown;

        visualHackTarget = Instantiate(visualHackTarget, Vector3.zero,  Quaternion.identity);
        visualHackTarget.SetActive(false);
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        HackAbility();
    }

    private void HackAbility()
    {
        if (cooldownTimer >= hackCooldown)
        {
            float closestDistance = float.MaxValue;
            GameObject closest = null; 
            
            // check the distance of all enemies and find the nearest
            foreach (GameObject enemy in enemies)
            {
                float distance = (enemy.transform.position - gameObject.transform.position).magnitude;
                if ( distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = enemy;
                }
            }

            // if the nearest is within the hackingRadius
            if (closestDistance < hackingRadius)
            {
                // set the visual aid to the closest target's position
                visualHackTarget.SetActive(true);
                visualHackTarget.transform.position = closest.transform.position;

                // if the player presses space, then hack
                if (Input.GetKey(KeyCode.Space))
                {
                    hackedEnemy = closest.GetComponent<EnemyController>();
                    hackedEnemy.Hack();
                    cooldownTimer = 0f;
                    unHacked = false;
                }
            } else
            {
                // nothing near, then there is no visual aid
                visualHackTarget.SetActive(false);
            }
        } else
        {
            cooldownTimer += Time.deltaTime;

            // if the enemy hasn't been unhacked yet and the time since hacking is 
            // more than the duration then unhack
            if (!unHacked && cooldownTimer >= hackingDuration)
            {
                unHacked = true;
                visualHackTarget.SetActive(false);
                hackedEnemy.UnHack();
            }
        }
    }

}

    
