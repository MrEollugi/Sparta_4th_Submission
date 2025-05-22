using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    private int maxJumpCount = 2;
    private int currentJumpCount = 0;
    private float jumpForce = 6f;

    [SerializeField] private LayerMask groundLayer;
    private float groundCheckDistance = 0.2f;
    private bool isGrounded = true;
    private bool wasGrounded = false;

    private Rigidbody rb;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer)
                     && rb.velocity.y <= 0.01f;

        if (isGrounded && !wasGrounded)
        {
            currentJumpCount = 0;
        }

        wasGrounded = isGrounded;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    public void OnMove(Vector2 direction) { }

    public void OnJump()
    {
        if (currentJumpCount < maxJumpCount)
        {
            Vector3 inputDir = playerMovement.GetInputDirectionWorld();
            Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            bool isReversing = Vector3.Dot(horizontalVelocity.normalized, inputDir) < -0.5f;

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (isReversing)
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
                float bonusHorizontalBoost = Mathf.Clamp(rb.velocity.magnitude * 0.5f + 10f, 10f, 40f);
                rb.AddForce(inputDir * bonusHorizontalBoost, ForceMode.Impulse);
            }

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            currentJumpCount++;
        }
    }


    public void OnInteract() { }
}