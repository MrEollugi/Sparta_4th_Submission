using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IPlayerInputReceiver
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Vector2 inputDirection;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(inputDirection.x, 0f, inputDirection.y);
        rb.AddForce(move * moveSpeed, ForceMode.Force);
    }

    public void OnMove(Vector2 direction)
    {
        inputDirection = direction;
    }

    public void OnJump()
    {

    }

    public void OnInteract()
    {

    }
}
