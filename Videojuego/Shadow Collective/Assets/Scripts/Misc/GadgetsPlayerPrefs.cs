/*
    Class for saving the player's gadgets in PlayerPrefs

    All three classes have three unique classes, which will be stored in a list of possible gadgets

    This class will save the active gadgets according to the order in that list
*/

public class GadgetsPlayerPrefs
{
    public bool hasGadgetOne;
    public bool hasGadgetTwo;
    public bool hasGadgetThree;

    public GadgetsPlayerPrefs()
    {
        hasGadgetOne = false;
        hasGadgetTwo = false;
        hasGadgetThree = false;
    }
}
