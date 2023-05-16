/*
    Script to define the Base Gadget class 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseGadget
{
    public BaseGadget(BasePlayer player_)
    {
        player = player_;
    }

    protected BasePlayer player;

    // Enter Key to activate gadget
    protected KeyCode keyBinded;


    //functions every gadget must have to reset in every level
    abstract public void ResetGadget();

    abstract public void UpdateGadget(float deltaTime);


    // functions that will work with the gadgets
    public bool CanBeSeen(BaseEnemy enemy) 
    {
        return true;
    }    

    public bool CanBeDamaged()
    {
        return true;
    }

    public float DamageMultiplier()
    {
        return 1;
    }
}