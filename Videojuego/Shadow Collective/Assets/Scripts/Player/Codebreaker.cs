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

    float cooldownTimer;
    GameObject visualHackTargetAim;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        health = base.maxHealth;
        maxSpeed = 5;
        damage = 1;

        cooldownTimer = hackCooldown;

        visualHackTargetAim = Instantiate(visualHackTarget, Vector3.zero, Quaternion.identity);
        visualHackTargetAim.SetActive(false);

        // to test gadgets
        possibleGadgets = new List<BaseGadget>()
        {
            new ShadowVeil(this),
            new CircuitBreaker(this),
            new PhantomSignal(this)
        };

        PopulateActiveGadgets();

        // for testing
        activeGadgets.Add(possibleGadgets[0]);
        activeGadgets[0].StartGadget();
        activeGadgets.Add(possibleGadgets[1]);
        activeGadgets[1].StartGadget();
        activeGadgets.Add(possibleGadgets[2]);
        activeGadgets[2].StartGadget();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        HackAbilityUpdate();
    }

    private void HackAbilityUpdate()
    {
        if (cooldownTimer >= hackCooldown)
        {
            float closestDistance = float.MaxValue;
            GameObject closest = null;

            // check the distance of all enemies and find the nearest
            foreach (GameObject enemy in enemies)
            {
                if (enemy == null)
                {
                    enemies.Remove(enemy);
                } 
                else
                {
                    float distance = (enemy.transform.position - gameObject.transform.position).magnitude;
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closest = enemy;
                    }
                }
            }

            // if the nearest is within the hackingRadius
            if (closestDistance < hackingRadius)
            {
                // set the visual aid to the closest target's position
                visualHackTargetAim.SetActive(true);
                visualHackTargetAim.transform.position = closest.transform.position;

                // if the player presses space, then hack
                if (Input.GetKey(KeyCode.Space))
                {
                    Hack(closest);
                }
            }
            else
            {
                // nothing near, then there is no visual aid
                visualHackTargetAim.SetActive(false);
            }
        }
        else
        {
            cooldownTimer += Time.deltaTime;
        }
    }

    public void Hack(GameObject enemy)
    {
        visualHackTargetAim.SetActive(false);
        StartCoroutine(HackCoroutine(enemy));
    }

    public IEnumerator HackCoroutine(GameObject enemy)
    {
        EnemyController hackedEnemy = enemy.GetComponent<EnemyController>();

        hackedEnemy.Hack(hackingDuration);
        cooldownTimer = 0f;

        GameObject visualHackTargetHacked = Instantiate(visualHackTarget, Vector3.zero, Quaternion.identity);
        visualHackTargetHacked.SetActive(true);
        visualHackTargetHacked.transform.position = enemy.transform.position;

        yield return new WaitForSeconds(hackingDuration);

        DestroyImmediate(visualHackTargetHacked);
    }

    override public bool CheckVisibility(GameObject enemy)
    {
        // only check if a gadget can save the player from being seen if it is visible
        if (!isVisible) return false;

        foreach (BaseGadget gadget in activeGadgets)
        {
            if (!gadget.CheckVisibility(enemy)) return false;
        }

        return true;
    }
}


