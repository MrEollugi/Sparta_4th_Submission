using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    public Transform target;

    [Header("View Settings")]
    private Vector3 thirdPersonOffset = new Vector3(0, 2f, -5f);
    private Vector3 firstPersonOffset = new Vector3(0, 1.6f, 0.2f);
    private Vector3 currentOffset;

    [Header("Rotation")]
    public float sensitivity = 2f;
    public float pitchMin = -30f, pitchMax = 60f;
    private float yaw, pitch;

    private bool isFirstPerson = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }

    private void Start()
    {
        currentOffset = thirdPersonOffset;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
    }

    private void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position + rotation * currentOffset;

        transform.position = desiredPosition;
        transform.rotation = rotation;

        if (!isFirstPerson)
        {
            transform.LookAt(target.position);
        }
    }

    public bool IsFirstPerson()
    {
        return isFirstPerson;
    }

    public Ray? TryGetInspectRay()
    {
        if (!isFirstPerson)
        {
            return null;
        }

        return new Ray(transform.position, transform.forward);
    }

    public void ToggleView()
    {
        isFirstPerson = !isFirstPerson;
        currentOffset = isFirstPerson ? firstPersonOffset : thirdPersonOffset;
    }
}
