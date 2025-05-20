using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    private float moveSpeed = 5f;
    private float currentMoveSpeed;
    //private float iceControlMultiplier = 0.05f;
    //private float airControlMultiplier = 0.5f;
    //private float frictionDamping = 0.9f;

    private Coroutine speedBoostCoroutine;

    private Vector2 inputDirection;
    private Rigidbody rb;

    //private bool isGrounded = false;
    //private bool isOnIce = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentMoveSpeed = moveSpeed;
    }

    public void OnMove(Vector2 direction)
    {
        inputDirection = direction;
    }
    private void FixedUpdate()
    {
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
