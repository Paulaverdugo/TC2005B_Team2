/*
    This script unifies all player classes in a script, so that other gameobjects or scripts that need
    to interact with the player can do so without knowing it's class
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public BasePlayer playerScript;
    [SerializeField] public GameObject enemyHandler; // to populate the enemies list
    [SerializeField] public Animator animator;
    [SerializeField] public Texture2D cursorTexture;

    [SerializeField] float acceleration;
    [SerializeField] float deceleration;

    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        ChooseCybergladiator();
        // ChooseCodebreaker();
        // ChooseGhostwalker();

        // -7 and -5 were obtained through trail and error
        Vector2 cursorHotspot = new Vector2(cursorTexture.width / 2 - 7, cursorTexture.height / 2 - 5);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

        foreach (Transform child in enemyHandler.transform)
        {
            enemies.Add(child.gameObject);
        }
    }

    virtual public bool CheckVisibility(GameObject enemy)
    {
        return playerScript.CheckVisibility(enemy);
    }

    public void GetDamaged(float damage) 
    {
        playerScript.GetDamaged(damage);
    }

    public BasePlayer GetPlayerScript()
    {
        return playerScript;
    }

    // functions to choose a class 
    public void ChooseCodebreaker()
    {
        gameObject.GetComponent<Cybergladiator>().enabled = false;
        gameObject.GetComponent<Ghostwalker>().enabled = false;

        BasePlayer codebreakerScript = gameObject.GetComponent<Codebreaker>();
        playerScript = codebreakerScript;

        PopulatePlayerScript();
    }

    public void ChooseCybergladiator()
    {
        gameObject.GetComponent<Codebreaker>().enabled = false;
        gameObject.GetComponent<Ghostwalker>().enabled = false;

        BasePlayer cybergladiatorScript = gameObject.GetComponent<Cybergladiator>();
        playerScript = cybergladiatorScript;
        
        PopulatePlayerScript();
    }

    public void ChooseGhostwalker()
    {
        gameObject.GetComponent<Codebreaker>().enabled = false;
        gameObject.GetComponent<Cybergladiator>().enabled = false;

        BasePlayer ghostwalkerScript = gameObject.GetComponent<Ghostwalker>();
        playerScript = ghostwalkerScript;
        
        PopulatePlayerScript();
    }

    void PopulatePlayerScript()
    {
        playerScript.enabled = true;
        playerScript.animator = animator;
        playerScript.acceleration = acceleration;
        playerScript.deceleration = deceleration;
        playerScript.enemies = enemies;
    }
}
