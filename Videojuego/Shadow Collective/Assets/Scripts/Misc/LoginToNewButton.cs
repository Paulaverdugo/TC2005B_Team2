/*
    Button in the login screen that takes you to the create account scene
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginToNewButton : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {   
        SceneManager.LoadScene("NewAccount");
    }
}
