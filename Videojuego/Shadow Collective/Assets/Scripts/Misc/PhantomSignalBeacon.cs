// script to make the beacon flash

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomSignalBeacon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool goingRed;
    private float flashingDuration;


    // Start is called before the first frame update
    void Start()
    {
        flashingDuration = 0.5f;
        goingRed = true;

        spriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

        InvokeRepeating("Flash", flashingDuration, flashingDuration);
    }

    void Flash()
    {
        if (goingRed)
        {
            spriteRenderer.color = new Color(1, 0, 0, 1);
            goingRed = false;
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            goingRed = true;
        }
    }

    public void WaitAndDestroy(float seconds)
    {
        gameObject.GetComponent<AudioSource>().Play();
        Destroy(gameObject, seconds);
    }
}
