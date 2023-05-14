using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Script to define and handle player movement (also includes certain movement animation triggers) 

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    protected Rigidbody2D rigidbody2d;

    protected float currentSpeed;

    [SerializeField]
    protected float deceleration = 1, acceleration = 1;

    protected float maxSpeed = 7;

    protected Vector2 movementDirection;

    // Events necessary for animations
    [field: SerializeField]
    public UnityEvent<float> OnVelocityChange { get; set; }

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbody2d.velocity = currentSpeed * movementDirection.normalized;
        OnVelocityChange?.Invoke(currentSpeed);
    }

    public void Move(Vector2 movementInput)
    {
        movementDirection = movementInput;
        currentSpeed = CalculateSpeed(movementInput);
    }

    private float CalculateSpeed(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }
        return Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }
}