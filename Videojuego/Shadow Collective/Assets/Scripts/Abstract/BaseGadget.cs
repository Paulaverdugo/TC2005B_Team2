/*
    Script to define the Base Gadget class 
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseGadget : MonoBehaviour
{

    private GameObject player;

    // Enter Key to activate gadget
    private KeyCode keyBinded;


    //functions every gadget must have to reset in every level
    public abstract void ResetGadget();

    public abstract void UpdateGadget();


    // functions that will work with the gadgets
    public bool CanBeSeen(GameObject player) 
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