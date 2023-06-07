/*
    Class to have all of the API constants, such as the URI and the Port in one place
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ApiConstants
{
    public static string URL = "http://localhost:8000";
}

[System.Serializable]
public class User
{
    public string user_name;
    public string user_password;
    public string email;
    public string age;
    // the User class also haves a timestamp
}

[System.Serializable]
public class ShortUser
{
    public string user_name;
    public string user_password;
}

[System.Serializable]
public class ShortUserList
{
    public List<ShortUser> users;
}

[System.Serializable]
public class ShortProgress
{
    public string user_name;
    public int player_type;
    public int level_achieved;
}

[System.Serializable]
public class Progress
{
    public int id_progress;
    public string user_name;
    public int player_type;
    public int level_achieved;
}

[System.Serializable]
public class ProgressList
{
    public List<Progress> progresses;
}

[System.Serializable]
public class ShortGadget
{
    public int gadget_id;
}

[System.Serializable]
public class ShortGadgetList
{
    public List<ShortGadget> gadgets;
}

[System.Serializable]
public class Win
{
    public string user_name;
    public int player_type;
}