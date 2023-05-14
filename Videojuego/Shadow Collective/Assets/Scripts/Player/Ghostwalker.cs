/*
    Script to define the player Ghostwalker
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghostwalker : BasePlayer
{
    // Start is called before the first frame update
    override protected void Start()
    {
        // Attributes
        health = 1;
        speed = 1;

        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }
}
