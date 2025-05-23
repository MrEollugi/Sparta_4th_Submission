using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Singleton
    public static CameraController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }

    #endregion

    #region Camera Settings

    public Transform target;

    [Header("View Settings")]
    private Vector3 thirdPersonOffset = new Vector3(0, 2f, -5f);
    private Vector3 firstPersonOffset = new Vector3(0, 1.6f, 0.2f);
    private Vector3 currentOffset;

    private bool isFirstPerson = false;

    [Header("Rotation")]
    public float sensitivity = 2f;
    public float pitchMin = -30f, pitchMax = 60f;
    private float yaw, pitch;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        // �⺻ ī�޶� ������ �� Ŀ�� ����
        currentOffset = thirdPersonOffset;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // ���콺 �Է����� ī�޶� ȸ��
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
    }

    private void LateUpdate()
    {
        // ȸ�� ���� �� ī�޶� ��ġ ����
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position + rotation * currentOffset;

        transform.position = desiredPosition;
        transform.rotation = rotation;

        if (!isFirstPerson)
        {
            // 3��Ī�� ��� Ÿ��(�÷��̾�)�� �ٶ󺸵���
            transform.LookAt(target.position);
        }
    }

    #endregion

    #region View Mode Methods
    public bool IsFirstPerson()
    {
        return isFirstPerson;
    }

    public void ToggleView()
    {
        isFirstPerson = !isFirstPerson;
        currentOffset = isFirstPerson ? firstPersonOffset : thirdPersonOffset;
    }

    public void AdjustOffsetByFOV(float fov, float maxFov)
    {
        float ratio = Mathf.InverseLerp(60f, maxFov, fov);
        Vector3 fastOffset = new Vector3(0, 1.5f, -1.5f);
        Vector3 normalOffset = isFirstPerson ? firstPersonOffset : thirdPersonOffset;
        
        currentOffset = Vector3.Lerp(normalOffset, fastOffset, ratio);
    }

    #endregion

    #region Inspection Ray

    public Ray? TryGetInspectRay()
    {
        if (!isFirstPerson)
        {
            return null;
        }

        return new Ray(transform.position, transform.forward);
    }

    #endregion
}
