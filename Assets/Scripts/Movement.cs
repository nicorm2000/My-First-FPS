using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f;

    Vector2 horizointalInput;

    [SerializeField] float gravity = -30f; //-9.81f 
    Vector3 verticalVelocity = Vector3.zero;

    private void Update()
    {
        Vector3 horizontalVelocity = (transform.right * horizointalInput.x + transform.forward * horizointalInput.y) * speed;

        controller.Move(horizontalVelocity * Time.deltaTime);

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 _horizointalInput)
    {
        horizointalInput = _horizointalInput;
    }
}
