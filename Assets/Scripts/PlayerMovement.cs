using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Setup")] 
    [SerializeField] private Rigidbody rigidBody;

    [SerializeField] private Transform feetPivot;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float coyoteTime = 0.2f;

    private Vector2 movementInput;
    private bool isGrounded = true;
    private bool isJumping = false;
    private float lastTimeGrounded = 0f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();    
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(transform.position * movementSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator CoyoteTime()
    {
        yield return new WaitForSeconds(coyoteTime);
        if (!isGrounded && Time.time - lastTimeGrounded <= coyoteTime)
        {
            isJumping = true;
        }
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (isGrounded || Time.time - lastTimeGrounded <= coyoteTime)
        {
            isJumping = true;
            StartCoroutine(CoyoteTime());
        }
    }
}
