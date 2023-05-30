using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{

    public float moveSpeed = 5f;

    void Update()
    {
        // Get input axes for horizontal and vertical movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement vector based on input axes
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        // Normalize the movement vector to prevent faster diagonal movement
        movement.Normalize();

        // Move the player based on the movement vector and speed
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

}
