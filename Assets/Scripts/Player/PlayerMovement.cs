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
    private float iceControlMultiplier = 0.05f;
    private float airControlMultiplier = 0.5f;
    private float frictionDamping = 0.9f;

    private Vector2 inputDirection;
    private Rigidbody rb;

    private bool isGrounded = false;
    private bool isOnIce = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //private void Update()
    //{
    //    inputDirection = InputManager.Instance.MoveInput;
    //}



    public void OnMove(Vector2 direction)
    {
        inputDirection = direction;
    }
    private void FixedUpdate()
    {
        Vector3 move = new Vector3(inputDirection.x, 0f, inputDirection.y);
        rb.velocity = new Vector3(move.x * moveSpeed, rb.velocity.y, move.z * moveSpeed);
    }

    private void UpdateGroundCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit, 1.1f))
        {
            isGrounded = true;
            isOnIce = hit.collider.CompareTag("Ice");
        }
        else
        {
            isGrounded= false;
            isOnIce= false;
        }
    }
}
