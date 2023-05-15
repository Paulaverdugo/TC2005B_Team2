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
    [SerializeField] GameObject visualHackTarget;

    private float cooldownTimer;
    private EnemyController hackedEnemy;


    // Base player gadgets
    // private List<Gadget> gadgets; TO DO -> uncomment when Gadget exists

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        
        health = 1;
        maxSpeed = 5;

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

            foreach (GameObject enemy in enemies)
            {
                float distance = (enemy.transform.position - gameObject.transform.position).magnitude;
                if ( distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = enemy;
                }
            }

            if (closestDistance < hackingRadius)
            {
                visualHackTarget.SetActive(true);
                visualHackTarget.transform.position = closest.transform.position;

                if (Input.GetKey(KeyCode.Space))
                {
                    hackedEnemy = closest.GetComponent<EnemyController>();
                    hackedEnemy.Hack();
                    cooldownTimer = 0f;
                }
            } else
            {
                visualHackTarget.SetActive(false);
            }
        } else
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= hackCooldown)
            {
                hackedEnemy.UnHack();
            }
        }
    }

}

    
