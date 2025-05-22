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
    private float maxSpeed = 60f;
    private float acceleration = 15f;
    private float deceleration = 30f;

    [Header("Dash Settings")]
    private float dashDuration = 2f;
    private float dashStaminaCost = 50f;

    [Header("Camera FOV")]
    [SerializeField] private Camera playerCamera;
    private float baseFOV = 60f;
    private float maxFOV = 120f;
    private float fovLerpSpeed = 10f;

    private Vector3 currentVelocity = Vector3.zero;
    private Vector2 inputDirection;
    private Rigidbody rb;
    private PlayerStats playerStats;

    private bool isDashing = false;
    private float dashTimeRemaining = 0f;

    private Coroutine speedBoostCoroutine;
    private float boostMultiplier = 1.5f;

    private MovingPlatform currentPlatform;
    public MovingPlatform CurrentPlatform => currentPlatform;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
    }

    public void OnMove(Vector2 direction)
    {
        inputDirection = direction;
    }

    private void Update()
    {
        UpdateFOV();
        HandleDashDecay();
    }

    private void FixedUpdate()
    {
        Vector3 platformVelocity = currentPlatform != null ? currentPlatform.DeltaMovement / Time.fixedDeltaTime : Vector3.zero;
        ApplyMovement(platformVelocity);
    }

    private void LateUpdate()
    {
        transform.position = rb.position;
    }

    private void UpdateFOV()
    {
        float speedRatio = currentVelocity.magnitude / maxSpeed;
        float targetFOV = Mathf.Lerp(baseFOV, maxFOV, speedRatio);
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);
        CameraController.Instance?.AdjustOffsetByFOV(playerCamera.fieldOfView, maxFOV);
    }

    private void ApplyMovement(Vector3 platformVelocity)
    {
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            Vector3 targetDirection = GetInputDirectionWorld();
            RotateTowards(targetDirection);

            Vector3 targetVelocity = targetDirection * maxSpeed;
            float alignment = Vector3.Dot(currentVelocity.normalized, targetDirection);

            float dynamicAccel = alignment < -0.5f ? acceleration * 3f : alignment < 0f ? acceleration * 2f : acceleration;
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, dynamicAccel * Time.fixedDeltaTime);
        }
        else
        {
            float speedRatio = currentVelocity.magnitude / maxSpeed;
            float dynamicDecel = deceleration + (1f - speedRatio) * 20f;
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, dynamicDecel * Time.fixedDeltaTime);
        }

        Vector3 velocity = currentVelocity + new Vector3(platformVelocity.x, 0f, platformVelocity.z);
        velocity.y = IsGroundedOnPlatform() ? platformVelocity.y : rb.velocity.y;
        rb.velocity = velocity;
    }

    private void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
    }

    private Vector3 GetInputDirectionWorld()
    {
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0f; camRight.y = 0f;
        camForward.Normalize(); camRight.Normalize();
        return (camForward * inputDirection.y + camRight * inputDirection.x).normalized;
    }

    public void TryDash()
    {
        if (playerStats.GetCurrentStamina() < dashStaminaCost || (isDashing && dashTimeRemaining > 0f)) return;

        playerStats.UseStamina(dashStaminaCost);
        isDashing = true;
        dashTimeRemaining = dashDuration;

        Vector3 dashDir;
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            dashDir = GetInputDirectionWorld();
        }
        else if (currentVelocity.sqrMagnitude > 0.01f)
        {
            dashDir = currentVelocity.normalized;
        }
        else
        {
            dashDir = transform.forward;
        }

        currentVelocity = dashDir * maxSpeed;
    }

    private void HandleDashDecay()
    {
        if (!isDashing) return;

        dashTimeRemaining -= Time.deltaTime;

        if (dashTimeRemaining <= 0f)
        {
            isDashing = false;
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
        Vector3 originalVelocity = currentVelocity;
        currentVelocity = Vector3.ClampMagnitude(currentVelocity * boostAmount, maxSpeed);

        yield return new WaitForSeconds(duration);

        if (currentVelocity.magnitude > originalVelocity.magnitude)
        {
            currentVelocity = originalVelocity;
        }
    }

    public void SetCurrentPlatform(MovingPlatform platform)
    {
        if (currentPlatform != platform)
        {
            currentPlatform = platform;
        }
    }

    bool IsGroundedOnPlatform()
    {
        if (currentPlatform == null) return false;

        Vector3 origin = transform.position + Vector3.up * 0.05f;
        float rayLength = 0.25f;

        return Physics.Raycast(origin, Vector3.down, out RaycastHit hit, rayLength)
               && hit.collider.GetComponent<MovingPlatform>() == currentPlatform;
    }

    private bool IsWallInFront(float distance = 0.6f)
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Vector3 dir = GetInputDirectionWorld();

        return Physics.Raycast(origin, dir, distance);
    }
}