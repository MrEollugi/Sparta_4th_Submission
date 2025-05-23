using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    #region Jump Settings

    private int maxJumpCount = 2;
    private int currentJumpCount = 0;
    private float jumpForce = 6f;

    [SerializeField] private LayerMask groundLayer;
    private float groundCheckDistance = 0.2f;

    private bool isGrounded = true;
    private bool wasGrounded = false;

    #endregion

    #region Components

    private Rigidbody rb;
    private PlayerMovement playerMovement;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // �ٴ�üũ
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer)
                     && rb.velocity.y <= 0.01f;

        // ���� ���� ���� ī��Ʈ �ʱ�ȭ
        if (isGrounded && !wasGrounded)
        {
            currentJumpCount = 0;
        }

        wasGrounded = isGrounded;
    }

    #endregion

    #region Input Callbacks

    public void OnMove(Vector2 direction) { } // �̻��

    public void OnJump()
    {
        if (currentJumpCount < maxJumpCount)
        {
            AudioManager.Instance?.PlayJump();

            Vector3 inputDir = playerMovement.GetInputDirectionWorld();
            Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // ���� ���� ����� �Է� ������ �ݴ��� ���(������ ��)
            bool isReversing = Vector3.Dot(horizontalVelocity.normalized, inputDir) < -0.5f;

            // ���� ���� �ӵ� ����
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (isReversing)
            {
                // ���� �� ���� �ӵ� ���� �� �Է� �������� �߰� ����
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
                float bonusHorizontalBoost = Mathf.Clamp(rb.velocity.magnitude * 0.5f + 10f, 10f, 40f);
                rb.AddForce(inputDir * bonusHorizontalBoost, ForceMode.Impulse);
            }

            // ���� �� ����
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            currentJumpCount++;
        }
    }


    public void OnInteract() { } // �̻��

    #endregion

    #region Ground Check (Optional Util)
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    #endregion
}