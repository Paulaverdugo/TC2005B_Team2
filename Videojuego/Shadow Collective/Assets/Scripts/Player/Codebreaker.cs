/*
    Script to define the player Codebreaker
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Codebreaker : BasePlayer
{
    // Attributes
    [SerializeField] float health = 1;
    [SerializeField] float speed = 1;
    [SerializeField] float hackCooldown = 1;

    // Player States
    private bool isVisible = true;
    private bool canSeeVisionCones = false;

    // Player Position
    [SerializeField] Vector3 pos;
    
    // Base player gadgets
    // private List<Gadget> gadgets; TO DO -> uncomment when Gadget exists

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

}

    
