/*
    Script to control when a level ends
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // load the next level
        SceneManager.LoadScene(nextLevel);
    }
}
