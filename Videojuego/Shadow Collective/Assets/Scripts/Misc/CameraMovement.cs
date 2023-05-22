/*
    Script for the camera's movement, so that it follows the player
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;

    // the player can move freely without the camera moving, outside of that, the player moves
    [SerializeField] float horizontalPlayerLimit;
    [SerializeField] float verticalPlayerLimit;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float cameraX = gameObject.transform.position.x;
        float cameraY = gameObject.transform.position.y;

        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;

        float newCamX = cameraX;
        float newCamY = cameraY;


        // check x bounds
        if (playerX > cameraX + horizontalPlayerLimit)
        {
            newCamX = playerX - horizontalPlayerLimit;
        }
        else if (playerX < cameraX - horizontalPlayerLimit)
        {
            newCamX = playerX + horizontalPlayerLimit;
        }

        // check y bounds
        if (playerY > cameraY + verticalPlayerLimit)
        {
            newCamY = playerY - verticalPlayerLimit;
        }
        else if (playerY < cameraY - verticalPlayerLimit)
        {
            newCamY = playerY + verticalPlayerLimit;
        }

        transform.position = new Vector3(newCamX, newCamY, -10);
    }
}
