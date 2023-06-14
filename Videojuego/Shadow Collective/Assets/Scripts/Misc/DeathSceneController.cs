/*
    Script to control the player's input in the death scene
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    private bool isCanvasActive = false;

    void Start()
    {
        StartCoroutine(ShowCanvas());
    }

    void Update()
    {
        if (Input.anyKey && isCanvasActive)
        {
            // go to the level one
            SceneManager.LoadScene("Level1");
        }
    }

    private IEnumerator ShowCanvas()
    {
        yield return new WaitForSeconds(2f);
        canvas.SetActive(true);
        isCanvasActive = true;
    }
}
