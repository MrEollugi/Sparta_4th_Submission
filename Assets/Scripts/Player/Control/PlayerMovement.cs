using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    #region Movement Settings

    [Header("Movement Settings")]
    private float maxSpeed = 50f;
    private float acceleration = 15f;
    private float deceleration = 30f;

    [Header("Dash Settings")]
    private float dashDuration = 2f;
    private float dashStaminaCost = 50f;
    private bool isDashing = false;
    private float dashTimeRemaining = 0f;

    private Coroutine speedBoostCoroutine;
    private float boostMultiplier = 1.5f;

    #endregion

    #region FOV Settings

    [Header("Camera FOV")]
    [SerializeField] private Camera playerCamera;
    private float baseFOV = 60f;
    private float maxFOV = 120f;
    private float fovLerpSpeed = 10f;

    #endregion

    #region Components & State

    private Vector3 currentVelocity = Vector3.zero;
    private Vector2 inputDirection;
    private Rigidbody rb;
    private PlayerStats playerStats;

    private MovingPlatform currentPlatform;
    public MovingPlatform CurrentPlatform => currentPlatform;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
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

    #endregion

    #region Movement Logic

    private void ApplyMovement(Vector3 platformVelocity)
    {
        Vector3 inputDir = GetInputDirectionWorld();
        Vector3 targetVelocity = inputDir * maxSpeed;

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            RotateTowards(inputDir);
        }

        if (IsWallInFront() && Vector3.Dot(currentVelocity.normalized, inputDir) > 0.5f)
        {
            targetVelocity = Vector3.zero;
        }

        float alignment = Vector3.Dot(currentVelocity.normalized, inputDir);
        float dynamicAccel;
        if (alignment < -0.5f)
        {
            dynamicAccel = acceleration * 3f;
        }
        else if (alignment < 0f)
        {
            dynamicAccel = acceleration * 2f;
        }
        else
        {
            dynamicAccel = acceleration;
        }

        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, dynamicAccel * Time.fixedDeltaTime);

        Vector3 velocity = currentVelocity + new Vector3(platformVelocity.x, 0f, platformVelocity.z);
        velocity.y = (currentPlatform != null && IsGroundedOnPlatform()) ? platformVelocity.y : rb.velocity.y;

        rb.velocity = velocity;
    }

    private void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
    }

    public Vector3 GetInputDirectionWorld()
    {
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0f; camRight.y = 0f;
        camForward.Normalize(); camRight.Normalize();
        return (camForward * inputDirection.y + camRight * inputDirection.x).normalized;
    }

    #endregion

    #region Dash Logic

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

    #endregion

    #region Speed Boost

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

    #endregion

    #region Platform Support

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

    #endregion

    #region Utilities
    private void UpdateFOV()
    {
        float speedRatio = currentVelocity.magnitude / maxSpeed;
        float targetFOV = Mathf.Lerp(baseFOV, maxFOV, speedRatio);
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);
        CameraController.Instance?.AdjustOffsetByFOV(playerCamera.fieldOfView, maxFOV);
    }

    private bool IsWallInFront(float distance = 0.6f)
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Vector3 dir = GetInputDirectionWorld();

        float radius = 0.25f;
        return Physics.SphereCast(origin, radius, dir, out _, distance);
    }

    #endregion
}