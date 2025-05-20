using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    private float jumpForce = 6f;
    private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded = true;
    private bool isJumping = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer);

        if(isGrounded && rb.velocity.y <= 0.01f)
        {
            isJumping = false;
        }

    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    public void OnMove(Vector2 direction) { }

    public void OnJump()
    {
        if (isGrounded && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    public void OnInteract() { }
}
