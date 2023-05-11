/*
    Script that defines the behavior for the enemy camera

    This class inherits from the abstract class Base Enemy
*/


// from Assets.Scripts.Abstract import BaseEnemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : BaseEnemy
{
    float flipTimer = 3f;

    override protected void Start()
    {
        base.Start();

        InvokeRepeating("Flip", Random.Range(0f, flipTimer), flipTimer);
    }

    private void Flip()
    {
        // flip the camera to make it look like it's looking around
        transform.Rotate(new Vector3(0f, 180f, 0f));
    }
}
