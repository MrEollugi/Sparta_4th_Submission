using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    #region Movement Settings

    [SerializeField] private Transform[] points;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool isLooping = true;

    private Vector3[] worldPoints;
    private int currentIndex = 0;
    private int direction = 1;

    #endregion

    #region Components & State

    private Rigidbody rb;
    private Vector3 lastPosition;
    public Vector3 DeltaMovement { get; private set; }

    #endregion

    #region Unity Callbacks
    private void Start()
    {
        // 유효한 이동 지점이 2개 이상 존재하는지 확인
        if (points == null || points.Length < 2)
        {
            enabled = false;
            return;
        }

        rb = GetComponent<Rigidbody>();

        // 로컬 포인트를 월드 좌표로 변환
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

        // 도착 확인
        if (Vector3.Distance(nextPosition, target) < 0.01f)
        {
            AdvanceToNextPoint();
        }
    }

    #endregion

    #region Movement Logic

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

    #endregion

    #region Player Syncing

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

    #endregion

    #region Gizmos

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

    #endregion
}