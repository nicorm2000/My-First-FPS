using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 10f;

    Vector2 horizontalInput;

    [SerializeField] float jumpForce = 5f;
    bool jump;

    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float halfHeight;

    private void Start()
    {
        halfHeight = controller.height * 0.5f;
    }

    private void FixedUpdate()
    {
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;

        rb.AddForce(horizontalVelocity * Time.deltaTime, ForceMode.Acceleration);
    }

    private void Update()
    {        
        var bottomPoint = transform.TransformPoint(controller.center - Vector3.up * halfHeight);
        isGrounded = Physics.CheckSphere(bottomPoint, 0.1f, groundMask);

        //Jump formula -> v = sqrt(-2 * jumpHeight * gravity)
        if (jump)
        {
            if (isGrounded)
            {
                rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            }

            jump = false;
        }
    }

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
}
