using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    // Main camera.
    private Camera mainCamera;

    // Shooting input.
    private bool shootButtonPressed = false;

    // Shooting Events
    [field: SerializeField]
    public UnityEvent OnShootButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnShootButtonReleased { get; set; }

    // Events that aid in getting player input.
    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChange { get; set; }

    private void Awake()
    {
        // Calls the main camera by finding the camera that has the tag "Main"
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetShootingInput();
    }

    private void GetMovementInput()
    {
        OnMovementKeyPressed?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    private void GetPointerInput()
    {
        var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        OnPointerPositionChange?.Invoke(mousePos);
    }

    private void GetShootingInput()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (!shootButtonPressed)
            {
                shootButtonPressed = true;
                OnShootButtonPressed?.Invoke();
            }
        }
        else
        {
            if (shootButtonPressed)
            {
                shootButtonPressed = false;
                OnShootButtonReleased?.Invoke();
            }
        }
    }

}
