/*
    Script that handles the class selection for the player
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

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

        // HERE IS WHERE WE CREATE A NEW PROGRESS THROUGH THE API

        // SceneManager.LoadScene("Level1");
    }
}
