using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool isLooping = true;

    private Vector3[] worldPoints;
    private int currentIndex = 0;
    private int direction = 1;

    private Rigidbody rb;
    private Vector3 lastPosition;
    public Vector3 DeltaMovement { get; private set; }

    private void Start()
    {
        if (points == null || points.Length < 2)
        {
            enabled = false;
            return;
        }

        rb = GetComponent<Rigidbody>();

        worldPoints = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            worldPoints[i] = points[i].position;
        }
        lastPosition = rb.position;
    }

    private void FixedUpdate()
    {
        Vector3 target = worldPoints[currentIndex];
        Vector3 nextPosition = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(nextPosition);

        DeltaMovement = nextPosition - lastPosition;
        lastPosition = nextPosition;

        if (Vector3.Distance(nextPosition, target) < 0.01f)
        {
            AdvanceToNextPoint();
        }
    }

    private void AdvanceToNextPoint()
    {
        currentIndex += direction;

        if (isLooping)
        {
            if (currentIndex >= points.Length)
                currentIndex = 0;
        }
        else
        {
            if (currentIndex >= points.Length)
            {
                currentIndex = points.Length - 2;
                direction = -1;
            }
            else if (currentIndex < 0)
            {
                currentIndex = 1;
                direction = 1;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        var playerMovement = collision.collider.GetComponent<PlayerMovement>();
        if (playerMovement != null && playerMovement.CurrentPlatform != this)
        {
            playerMovement.SetCurrentPlatform(this);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        var playerMovement = collision.collider.GetComponent<PlayerMovement>();
        if (playerMovement != null && playerMovement.CurrentPlatform == this)
        {
            playerMovement.SetCurrentPlatform(null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (points == null || points.Length < 2) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < points.Length - 1; i++)
        {
            Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }

        if (isLooping)
        {
            Gizmos.DrawLine(points[^1].position, points[0].position);
        }
    }
}