/*
    Script that handles the gadget selection for the player
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Networking;

public class SkinManagerGadget : MonoBehaviour
{
    [SerializeField] public SpriteRenderer sr;
    private List<Sprite> skins = new List<Sprite>();
    private List<ShortGadget> gadgetsToChoose;
    private List<ShortGadget> activeGadgets = new List<ShortGadget>();
    private int selectedSkin = 0;

    public void Start()
    {
        // pupulate gadgets to choose depending on the class
        switch (PlayerPrefs.GetInt("player_type_number"))
        {
            case 1: // cybergladiator
                gadgetsToChoose = new List<ShortGadget> {new ShortGadget(1), new ShortGadget(2), new ShortGadget(3)};
                break;

            case 2: // codebreaker
                gadgetsToChoose = new List<ShortGadget> {new ShortGadget(4), new ShortGadget(5), new ShortGadget(6)};
                break;

            case 3: // ghostwalker
                gadgetsToChoose = new List<ShortGadget> {new ShortGadget(7), new ShortGadget(8), new ShortGadget(9)};
                break;

            default: // we shouldn't reach here
                gadgetsToChoose = new List<ShortGadget> {new ShortGadget(1), new ShortGadget(2), new ShortGadget(3)};
                break;
        }

        // get the active gadgets from player prefs
        string jsonActiveGadgets = PlayerPrefs.GetString("gadgets");
        if (jsonActiveGadgets != "")
        {
            ShortGadgetList shortGadgetList = JsonUtility.FromJson<ShortGadgetList>(jsonActiveGadgets);

            activeGadgets = shortGadgetList.gadgets;

            print("Active gadgets count: " + activeGadgets.Count);

            foreach (ShortGadget gadget in activeGadgets)
            {
                gadgetsToChoose.Remove(gadget);
            }
        }

        foreach (ShortGadget gadget in gadgetsToChoose)
        {
            print("gadget id: " + gadget.gadget_id);
        }

    }

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

        // HERE IS WHERE WE CREATE A NEW PROGRESS THROUGH THE API

        // StartCoroutine(CreateProgress());
        SceneManager.LoadScene("Tutorial");
    }
}
