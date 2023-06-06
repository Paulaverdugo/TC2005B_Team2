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

    // gadget id
    public int gadget_id;

    protected BasePlayer player;

    // Enter Key to activate gadget
    protected KeyCode keyBinded;


    //functions every gadget must have to reset in every level
    abstract public void StartGadget();

    abstract public void UpdateGadget(float deltaTime);


    // functions that will work with the gadgets
    virtual public bool CheckVisibility(GameObject enemy) 
    {
        return true;
    }    

    virtual public bool CanBeDamaged()
    {
        return true;
    }

    virtual public float DamageMultiplier()
    {
        return 1;
    }
}