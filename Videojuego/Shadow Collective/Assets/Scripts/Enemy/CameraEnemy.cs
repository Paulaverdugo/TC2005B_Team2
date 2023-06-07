/*
    Script that defines the behavior for the enemy camera

    This class inherits from the abstract class Base Enemy
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnemy : BaseEnemy
{    
    float flipTimer = 3f;
    private bool flips = false;

    override protected void Start()
    {
        spriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        base.Start();

        // if it's looking to a side, we don't want the camera to flip around
        if (startingVisionConeDirection == new Vector3(0, -1, 0))
        {
            StartFlipping();
            flips = true;
        } else if (startingVisionConeDirection == new Vector3(1, 0, 0))
        {
            // just flip once
            Flip();
        }
        
    }

    private void StartFlipping()
    {
        InvokeRepeating("Flip", Random.Range(0f, flipTimer), flipTimer);
    }

    private void Flip()
    {
        // flip the camera to make it look like it's looking around
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    override public void Hack(float hackDuration_)
    {
        base.Hack(hackDuration_);
        CancelInvoke();
    }

    override public void UnHack()
    {
        base.UnHack();
        if (flips) StartFlipping();
    }
}
