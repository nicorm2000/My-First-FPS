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

    private void Update()
    {   
        //This lets the player move
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed + Vector3.up * rb.velocity.y;

        rb.velocity = horizontalVelocity;

        var bottomPoint = transform.TransformPoint(controller.center - Vector3.up * halfHeight);
        
        isGrounded = Physics.CheckSphere(bottomPoint, 0.1f, groundMask);

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
