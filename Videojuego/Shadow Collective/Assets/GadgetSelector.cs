using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GadgetSelector : MonoBehaviour
{

    public int gadgetID;
    //GadgetIDS: circuitbreaker 1, phantom 2, shadowveil 3
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenScene(){
        SceneManager.LoadScene("Level 2");
        //Communicate with player and SQL to update gadget
    }

    //If gadget < 0 go to level 1
    //If gadget >= 1 go to level 2
}
