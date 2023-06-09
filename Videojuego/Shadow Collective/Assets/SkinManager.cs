/*
    Script that handles the class selection for the player
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Networking;

public class SkinManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> skins = new List<Sprite>();
    private int selectedSkin = 0;
    public GameObject playerskin;

    public void NextOption()
    {
        selectedSkin += 1;
        if(selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }
        sr.sprite = skins[selectedSkin];
    }

    public void BackOption()
    {
        selectedSkin -= 1;
        if(selectedSkin < 0)
        {
            selectedSkin = skins.Count - 1;
        }
        sr.sprite = skins[selectedSkin];
    }
    
    public void PlayGame()
    {
        PlayerPrefs.SetString("player_type", skins[selectedSkin].name);

        int playerTypeNumber = 0;

        switch(skins[selectedSkin].name)
        {
            case "cybergladiator":
                playerTypeNumber = 1;
                break;
            case "codebreaker":
                playerTypeNumber = 2;
                break;
            case "ghostwalker":
                playerTypeNumber = 3;
                break;
            default: // we shouldn't reach here
                playerTypeNumber = 0;
                break;
        }

        PlayerPrefs.SetInt("player_type_number", playerTypeNumber);
        PlayerPrefs.SetString("level_achieved", "Level1");

        // empty gadgets in player prefs
        ShortGadgetList gadgets = new ShortGadgetList();
        gadgets.gadgets = new List<ShortGadget>();
        string jsonGadgets = JsonUtility.ToJson(gadgets);
        PlayerPrefs.SetString("gadgets", jsonGadgets);

        // HERE IS WHERE WE CREATE A NEW PROGRESS THROUGH THE API
        StartCoroutine(CreateProgress());

        // the create progress coroutine is in charge of loading the tutorial since we have to wait until it's finished
    }

    private IEnumerator CreateProgress()
    {
        ShortProgress progress = new ShortProgress();

        progress.level_achieved = 1;
        progress.user_name = PlayerPrefs.GetString("user_name");
        progress.player_type = PlayerPrefs.GetInt("player_type_number");

        string jsonProgress = JsonUtility.ToJson(progress);

        string ep = ApiConstants.URL + "/progress/newProgress";

        using (UnityWebRequest www = UnityWebRequest.Put(ep, jsonProgress))
        {
            //UnityWebRequest www = UnityWebRequest.Post(url + getUsersEP, form);
            // Set the method later, and indicate the encoding is JSON
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) 
            {
                Debug.Log("error creating a progress: " + www.error);
            } else // get the id_progress from the response
            {
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                PlayerPrefs.SetInt("id_progress", response.data.insertId);
            }
        }
        SceneManager.LoadScene("Tutorial");
    }
}
