using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    #region Singleton

    // 전역 접근 가능한 싱글톤 인스턴스
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        InitializeComponents();
    }

    #endregion

    #region Component References

    public PlayerStats Stats { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerJump Jump { get; private set; }
    public PlayerInputHandler InputReceiver { get; private set; }
    public PlayerInventory Inventory { get; private set; }

    private void InitializeComponents()
    {
        Stats = GetComponent<PlayerStats>();
        Movement = GetComponent<PlayerMovement>();
        Jump = GetComponent<PlayerJump>();
        InputReceiver = GetComponent<PlayerInputHandler>();
        Inventory = GetComponent<PlayerInventory>();
    }

    #endregion

    #region Health Accessors
    // 현재 체력 반환
    public float GetCurrentHealth()
    {
        return Stats.GetCurrentHealth();
    }
    // 최대 체력 반환
    public float GetMaxHealth()
    {
        return Stats.GetMaxHealth();
    }

    #endregion

    #region Respawn
    public void Respawn(Vector3 position)
    {
        transform.position = position;

        Stats.ResetHealth();
        Stats.ResetStamina();

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Movement.SetCurrentPlatform(null);
    }
    #endregion
}
