/*
    Script to control when a level ends
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] string nextLevel;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EndLevel();
        }
    }

    public void EndLevel()
    {
        // if it's the boss level, add a win to the player's stats
        if (SceneManager.GetActiveScene().name == "LevelB")
        {
            StartCoroutine(AddWin());
        }

        StartCoroutine(UpdateLevelAchieved());
        
        // load the next level
        SceneManager.LoadScene(nextLevel);
    }

    private IEnumerator AddWin()
    {
        Win win = new Win();

        // populate the win object
        win.user_name = PlayerPrefs.GetString("user_name");
        win.player_type = PlayerPrefs.GetInt("player_type_number");
        
        string jsonWin = JsonUtility.ToJson(win);

        string ep = ApiConstants.URL + "/event/addWin";
        // even though the API is a post, we use webrequest's put and later define the method as post
        using (UnityWebRequest www = UnityWebRequest.Put(ep, jsonWin))
        {
            //UnityWebRequest www = UnityWebRequest.Post(url + getUsersEP, form);
            // Set the method later, and indicate the encoding is JSON
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error creating a win: " + www.error);
            }
        }
    }

    private IEnumerator UpdateLevelAchieved()
    {
        LevelAchieved levelAchieved = new LevelAchieved();

        // populate the levelAchieved object
        levelAchieved.id_progress = PlayerPrefs.GetInt("id_progress");
        switch (nextLevel)
        {
            case "Level1":
                levelAchieved.level_achieved = 1;
                break;
            case "Level2":
                levelAchieved.level_achieved = 2;
                break;
            case "LevelB":
                levelAchieved.level_achieved = 3;
                break;
            // case "scene after boss":
            //     levelAchieved.level_achieved = 4;
            //     break;
            default: // should not reach here
                levelAchieved.level_achieved = 1;
                break;
        }

        string jsonLevelAchieved = JsonUtility.ToJson(levelAchieved);

        string ep = ApiConstants.URL + "/progress/updateLevel";

        // even though the API is a patch, we use webrequest's put and later define the method as post
        using (UnityWebRequest www = UnityWebRequest.Put(ep, jsonLevelAchieved))
        {
            // Set the method later, and indicate the encoding is JSON
            www.method = "PATCH";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error updating the level achieved: " + www.error);
            }
        }
        
    }
}
