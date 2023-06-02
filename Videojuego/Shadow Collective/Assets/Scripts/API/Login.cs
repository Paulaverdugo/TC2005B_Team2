/*
    Script that belongs to the log in button

    Takes in a username and password and checks if it exists 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private GameObject errorMessage;


    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        StartCoroutine(LogIn());
    }

    private IEnumerator LogIn()
    {
        string ep = ApiConstants.URL + "/users/" + username.text;

        using (UnityWebRequest www = UnityWebRequest.Get(ep))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) 
            {
                // the response is a list of json short users
                if (www.downloadHandler.text != "[]")
                {
                    string jsonString = "{\"users\":" + www.downloadHandler.text + "}";
                    ShortUserList shortUserList = JsonUtility.FromJson<ShortUserList>(jsonString);
                    ShortUser shortUser = shortUserList.users[0];

                    // if the password returns matches the password inputed, we are good to go
                    if (shortUser.user_password == password.text) {
                        PlayerPrefs.SetString("user_name", shortUser.user_name);
                        SceneManager.LoadScene("Intro");
                    } else {
                        errorMessage.SetActive(true);
                        errorMessage.GetComponent<TMP_Text>().text = "Incorrect username or password!";
                    }
                }
                else
                {
                    errorMessage.SetActive(true);
                    errorMessage.GetComponent<TMP_Text>().text = "User does not exist!";
                }
            } else {
                errorMessage.SetActive(true);
                errorMessage.GetComponent<TMP_Text>().text = "Error! Please try again.";
            }
        }
    }
}
