using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f;

    Vector2 horizointalInput;

    [SerializeField] float jumpHeight = 5f;
    bool jump;

    [SerializeField] float gravity = -30f; //-9.81f 
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    private void Update()
    {
        float halfHeight = controller.height * 0.5f;
        var bottomPoint = transform.TransformPoint(controller.center - Vector3.up * halfHeight);

        isGrounded = Physics.CheckSphere(bottomPoint, 0.1f, groundMask);

        if (isGrounded)
        {
            verticalVelocity.y = 0;
        }

        Vector3 horizontalVelocity = (transform.right * horizointalInput.x + transform.forward * horizointalInput.y) * speed;

        controller.Move(horizontalVelocity * Time.deltaTime);

        //Jump formula -> v = sqrt(-2 * jumpHeight * gravity)
        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }

            jump = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 _horizointalInput)
    {
        horizointalInput = _horizointalInput;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
}
