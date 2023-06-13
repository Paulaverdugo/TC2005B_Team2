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
                        StartCoroutine(GetProgress());
                    } else {
                        errorMessage.SetActive(true);
                        errorMessage.GetComponent<TMP_Text>().text = "Incorrect username or password!";
                    }
                }
                else
                {
                    errorMessage.SetActive(true);
                    errorMessage.GetComponent<TMP_Text>().text = "Incorrect username or password!";
                }
            } else {
                errorMessage.SetActive(true);
                errorMessage.GetComponent<TMP_Text>().text = "Error! Please try again.";
            }
        }
    }

    // after being succesfully logged in, we have to check if there's an active progress
    // if there is we resume that progress, if not, we go to class selection
    // class selection is responsible of creating the new progress
    private IEnumerator GetProgress()
    {
        string ep = ApiConstants.URL + "/progress/user/" + PlayerPrefs.GetString("user_name");

        using (UnityWebRequest www = UnityWebRequest.Get(ep))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) 
            {
                // the response is [] if there is no active progress
                if (www.downloadHandler.text == "[]")
                {
                    SceneManager.LoadScene("Selection");
                }
                // there is an active progress
                else
                {
                    string jsonString = "{\"progresses\":" + www.downloadHandler.text + "}";
                    print(jsonString);
                    ProgressList progressList = JsonUtility.FromJson<ProgressList>(jsonString);
                    Progress progress = progressList.progresses[0];

                    string playerType = "";
                    switch (progress.player_type)
                    {
                        case 1:
                            playerType = "cybergladiator";
                            break;
                        case 2:
                            playerType = "codebreaker";
                            break;
                        case 3:
                            playerType = "ghostwalker";
                            break;
                    }
                    // add info to player prefs
                    PlayerPrefs.SetString("player_type", playerType);
                    PlayerPrefs.SetInt("player_type_number", progress.player_type);
                    PlayerPrefs.SetInt("id_progress", progress.id_progress);

                    yield return StartCoroutine(GetGadgets());

                    print("level achieved: " + progress.level_achieved + " | id_progress: " + progress.id_progress);

                    switch (progress.level_achieved)
                    {
                        case 1:
                            PlayerPrefs.SetString("level_achieved", "Level1");
                            SceneManager.LoadScene("Level1");
                            break;
                        case 2:
                            PlayerPrefs.SetString("level_achieved", "Level2");
                            SceneManager.LoadScene("Level2");
                            break;
                        case 3:
                            PlayerPrefs.SetString("level_achieved", "LevelB");
                            SceneManager.LoadScene("LevelB");
                            break;
                        default:
                            PlayerPrefs.SetString("level_achieved", "Level1");
                            SceneManager.LoadScene("Selection");
                            break;
                    }
                }
            } else {
                Debug.Log("Error: " + www.error);
            }
        }        
    }

    private IEnumerator GetGadgets()
    {
        string ep = ApiConstants.URL + "/progress/gadget/" + PlayerPrefs.GetString("user_name");

        using (UnityWebRequest www = UnityWebRequest.Get(ep))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) 
            {
                // the response is [] if there is are no active gadgets
                if (www.downloadHandler.text != "[]")
                {
                    string jsonGadgets = "{\"gadgets\":" + www.downloadHandler.text + "}";
                    PlayerPrefs.SetString("gadgets", jsonGadgets); 
                }
            } else {
                Debug.Log("Error: " + www.error);
            }
        }
    }
}
