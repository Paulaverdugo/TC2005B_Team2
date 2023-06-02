/*
    Script that belongs to the create account button.

    Controls the connection to the API and the creation of a new account.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CreateAccount : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField age;
    [SerializeField] private GameObject errorMessage;

    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        StartCoroutine(AddUser());
    }

    private IEnumerator AddUser()
    {
        User newUser = new User();

        newUser.user_name = username.text;
        newUser.user_password = password.text;
        newUser.email = email.text;
        newUser.age = age.text;
        
        string jsonUser = JsonUtility.ToJson(newUser);

        string ep = ApiConstants.URL + "/users/createUser";

        // even though the API is a post, we use webrequest's put and later define the method as post
        using (UnityWebRequest www = UnityWebRequest.Put(ep, jsonUser))
        {
            //UnityWebRequest www = UnityWebRequest.Post(url + getUsersEP, form);
            // Set the method later, and indicate the encoding is JSON
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) 
            {
                // consider after creating user taking the user to the game instead of the log in screen
                // in that case ADD THE USER_NAME TO PLAYER PREFS
                PlayerPrefs.SetString("user_name", newUser.user_name);
                SceneManager.LoadScene("Intro");
            } else {
                // Useful to debug:
                // EditorUtility.DisplayDialog("Error", "Error: " + www.error, "Ok");

                errorMessage.SetActive(true);
            }
        }
    }
}
