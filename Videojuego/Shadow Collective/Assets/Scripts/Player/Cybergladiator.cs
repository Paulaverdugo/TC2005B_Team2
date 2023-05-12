/*
    Script that defines the behavior of the palyer Cybergladiator.

    This class inherits from the abstract class BasePlayer.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We are inheriting from abstrcat class BasePlayer

public class Cybergladiator : BasePlayer
{
     //Attributes
    [SerializeField] float health = 1;
    [SerializeField] float speed =1 ;

    //Player states
    private bool isVisible = true;
    private bool canSeeVisionCones = false;

    //Player position
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
