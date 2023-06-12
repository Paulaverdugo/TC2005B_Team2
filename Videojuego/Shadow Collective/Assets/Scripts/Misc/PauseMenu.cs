/*
    Script for the pause menu canvas

    It has the functions that buttons inside use
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameObject normalPause;
    private GameObject confirmResetPause;

    // Start is called before the first frame update
    void Start()
    {
        // the normal pause elements are all within one gameobject in the pause canvas
        normalPause = GameObject.Find("PauseMenu/NormalPause").gameObject;

        // find the reset pause menu
        confirmResetPause = GameObject.Find("PauseMenu/ConfirmResetPause").gameObject;
        confirmResetPause.SetActive(false); // find wouldn't find it if it was inactive
    }

    void OnDisable()
    {
        normalPause.SetActive(true);
        confirmResetPause.SetActive(false);
    }

    public void OnResetClick()
    {
        normalPause.SetActive(false);
        confirmResetPause.SetActive(true);
    }

    public void OnCancelResetClick()
    {
        normalPause.SetActive(true);
        confirmResetPause.SetActive(false);
    }

    public void OnConfirmResetClick()
    {
        // take the player to the selection scene, where another progress will be created 
        SceneManager.LoadScene("Selection");
    }

    public void OnResumeClick()
    {
        Resume();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
