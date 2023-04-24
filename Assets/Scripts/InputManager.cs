using UnityEngine;
using UnityEngine.InputSystem;

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
        InitInputs();
    }

    private void InitInputs()
    {
        controls = new PlayerControls();

        groundMovement = controls.GroundMovement;

        //groundMovement.[action].performed += context => do x something (how this event call work)
        //Instead of ctx (context) I use _ when I don't need any information

        //Here is the subscription towards the jump event happens

        groundMovement.Jump.performed += _ => movement.OnJumpPressed();

        //Here is the subscription to the mouse movement

        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();

        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        //Here is the subscription and unsubscription to the Shoot event happens
        groundMovement.Shoot.started += _ => StartFiring();

        groundMovement.Shoot.canceled += _ => StopFiring();
    }

    private void Update()
    {
        movement.ReceiveInput(horizontalInput);

        mouseLook.ReceiveInput(mouseInput);
    }

    private void OnHorizontalMovement(InputValue input)
    {
        horizontalInput = input.Get<Vector2>();
    }

    void StartFiring()
    {
        fireCoroutine = StartCoroutine(gun.ShootCoroutine());
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
