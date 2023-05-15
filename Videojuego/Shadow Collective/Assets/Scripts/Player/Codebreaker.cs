/*
    Script to define the player Codebreaker
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Codebreaker : BasePlayer
{
    [SerializeField] float hackCooldown = 1;

    // Base player gadgets
    // private List<Gadget> gadgets; TO DO -> uncomment when Gadget exists

    // Start is called before the first frame update
    override protected void Start()
    {
        health = 1;
        maxSpeed = 7;
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

}

    
