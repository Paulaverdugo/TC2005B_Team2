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
}
