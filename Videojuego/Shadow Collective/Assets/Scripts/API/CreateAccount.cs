/*
    Script that belongs to the create account button.

    Controls the connection to the API and the creation of a new account.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateAccount : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField age;

    void Start()
    {
        // print(ApiConstants.URI + ":" + ApiConstants.PORT + "/createAccount");
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        // print(ApiConstants.URI + ":" + ApiConstants.PORT + "/createAccount");
        print(username.text);
    }
}
