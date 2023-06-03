using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GadgetSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenScene(){
        SceneManager.LoadScene("Level2");
        //Communicate with player and SQL to update gadget
    }
}
