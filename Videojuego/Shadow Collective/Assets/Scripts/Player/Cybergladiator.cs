/*
    Script that defines the behavior of the player Cybergladiator.

    This class inherits from the abstract class BasePlayer.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We are inheriting from abstrcat class BasePlayer

public class Cybergladiator : BasePlayer
{
    // Start is called before the first frame update
    override protected void Start()
    {
        //Attributes
        health = 1;
        speed = 3;

        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }
   
}
