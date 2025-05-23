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
        // 바닥체크
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer)
                     && rb.velocity.y <= 0.01f;

        // 착지 직후 점프 카운트 초기화
        if (isGrounded && !wasGrounded)
        {
            currentJumpCount = 0;
        }

        wasGrounded = isGrounded;
    }

    #endregion

    #region Input Callbacks

    public void OnMove(Vector2 direction) { } // 미사용

    public void OnJump()
    {
        if (currentJumpCount < maxJumpCount)
        {
            AudioManager.Instance?.PlayJump();

            Vector3 inputDir = playerMovement.GetInputDirectionWorld();
            Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // 현재 진행 방향과 입력 방향이 반대일 경우(급정지 용)
            bool isReversing = Vector3.Dot(horizontalVelocity.normalized, inputDir) < -0.5f;

            // 기존 수직 속도 제거
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (isReversing)
            {
                // 점프 시 수평 속도 정지 후 입력 방향으로 추가 보정
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
                float bonusHorizontalBoost = Mathf.Clamp(rb.velocity.magnitude * 0.5f + 10f, 10f, 40f);
                rb.AddForce(inputDir * bonusHorizontalBoost, ForceMode.Impulse);
            }

            // 점프 힘 적용
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            currentJumpCount++;
        }
    }


    public void OnInteract() { } // 미사용

    #endregion

    #region Ground Check (Optional Util)
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    #endregion
}