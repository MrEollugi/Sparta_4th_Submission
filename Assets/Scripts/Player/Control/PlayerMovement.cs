using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    private float moveSpeed = 5f;
    private float currentMoveSpeed;
    private float dashMultiplier = 2f;
    private float dashDuration = 0.5f;
    private float dashDecayTime = 0.5f;
    private float dashStaminaCost = 10f;

    private Coroutine speedBoostCoroutine;

    private Vector2 inputDirection;
    private Rigidbody rb;

    private bool isDashing = false;
    private float dashTimeRemaining = 0f;
    private float dashDecayTimer = 0f;
    //private bool isGrounded = false;
    //private bool isOnIce = false;

    private PlayerStats playerStats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentMoveSpeed = moveSpeed;
        playerStats = GetComponent<PlayerStats>();
    }

    public void OnMove(Vector2 direction)
    {
        inputDirection = direction;
    }

    private void FixedUpdate()
    {
        HandleDashDecay();

        if(inputDirection.sqrMagnitude > 0.01f)
        {
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = camForward * inputDirection.y + camRight * inputDirection.x;

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);

            rb.velocity = new Vector3(moveDir.x * currentMoveSpeed, rb.velocity.y, moveDir.z * currentMoveSpeed);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    public void TryDash()
    {
        if (playerStats.GetCurrentStamina() < dashStaminaCost) return;

        if (isDashing && dashTimeRemaining > 0f) return;

        playerStats.UseStamina(dashStaminaCost);

        isDashing = true;
        dashTimeRemaining = dashDuration;
        dashDecayTimer = dashDecayTime;
        currentMoveSpeed = moveSpeed * dashMultiplier;
    }

    private void HandleDashDecay()
    {
        if (!isDashing) return;

        if(dashTimeRemaining > 0f)
        {
            dashTimeRemaining -= Time.deltaTime;
        }
        else
        {
            dashDecayTimer -= Time.deltaTime;

            float t = 1f - (dashDecayTimer / dashDecayTime);
            currentMoveSpeed = Mathf.Lerp(moveSpeed * dashMultiplier, moveSpeed, t);

            if(dashDecayTimer <= 0f)
            {
                currentMoveSpeed = moveSpeed;
                isDashing = false;
            }
        }
    }

    public IEnumerator ApplySpeedBoost(float boostAmount, float duration)
    {
        if (speedBoostCoroutine != null)
            StopCoroutine(speedBoostCoroutine);

        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(boostAmount, duration));
        yield return speedBoostCoroutine;
    }

    private IEnumerator SpeedBoostRoutine(float boostAmount, float duration)
    {
        currentMoveSpeed += boostAmount;
        yield return new WaitForSeconds(duration);
        currentMoveSpeed = moveSpeed;
    }

    //private void UpdateGroundCheck()
    //{
    //    Ray ray = new Ray(transform.position, Vector3.down);
    //    if(Physics.Raycast(ray, out RaycastHit hit, 1.1f))
    //    {
    //        isGrounded = true;
    //        isOnIce = hit.collider.CompareTag("Ice");
    //    }
    //    else
    //    {
    //        isGrounded= false;
    //        isOnIce= false;
    //    }
    //}
}
