/*
    Script that controlls the gadget bar
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class GadgetBar : MonoBehaviour
{
    private GameObject playerClass;
    private GameObject gadget1;
    private GameObject gadget2; 

    void Start()
    {
        playerClass = gameObject.transform.Find("Class").gameObject;
        PopulateClass();

        gadget1 = gameObject.transform.Find("Gadget1").gameObject;
        gadget2 = gameObject.transform.Find("Gadget2").gameObject;
        PopulateGadgets();

    }

    void PopulateClass()
    {
        string classString = PlayerPrefs.GetString("player_type");

        Image imageComponent = playerClass.GetComponent<Image>();
        imageComponent.sprite = Resources.Load<Sprite>(classString);
        imageComponent.color = new Color(255, 255, 255, 255);
    }

    void PopulateGadgets()
    {
        string jsonGadgets = PlayerPrefs.GetString("gadgets");
        ShortGadgetList shortGadgetList = JsonUtility.FromJson<ShortGadgetList>(jsonGadgets);
        List<ShortGadget> gadgets = shortGadgetList.gadgets;

        print("gadgets: " + gadgets.Count);

        if (gadgets.Count > 0)
        {
            Image imageComponent = gadget1.GetComponent<Image>();
            imageComponent.sprite = Resources.Load<Sprite>("GadgetSprites/" + gadgets[0].gadget_id.ToString());
            imageComponent.color = new Color(255, 255, 255, 255);
        }
        else
        {
            gadget1.SetActive(false);
        }

        if (gadgets.Count > 1)
        {
            Image imageComponent = gadget2.GetComponent<Image>();
            imageComponent.sprite = Resources.Load<Sprite>("GadgetSprites/" + gadgets[1].gadget_id.ToString());
            imageComponent.color = new Color(255, 255, 255, 255);
        }
        else
        {
            gadget2.SetActive(false);
        }
    }
}
