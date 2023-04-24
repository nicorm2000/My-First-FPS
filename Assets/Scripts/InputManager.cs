using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] Gun gun;

    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    Coroutine fireCoroutine;

    private void Awake()
    {
        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;

        //groundMovement.[action].performed += context => do x something (how this event call work)
        //Instead of ctx I use _ when I don't need any information

        groundMovement.Jump.performed += _ => movement.OnJumpPressed();

        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        //Script for Single fire shooting
        //groundMovement.Shoott.performed += _ => gun.Shoot();
        
        groundMovement.Shoott.started += _ => StartFiring();
        groundMovement.Shoott.canceled += _ => StopFiring();
    }

    private void Update()
    {
        horizontalInput = groundMovement.HorizontalMovement.ReadValue<Vector2>(); //Reads constantly the player input
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
    }

    void StartFiring()
    {
        fireCoroutine = StartCoroutine(gun.RapidFire());
    }

    void StopFiring()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
