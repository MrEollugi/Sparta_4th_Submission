using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    public static Player Instance { get; private set; }

    public PlayerStats Stats { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerJump Jump { get; private set; }
    public PlayerInputHandler InputReceiver { get; private set; }

    public PlayerInventory Inventory { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Stats = GetComponent<PlayerStats>();
        Movement = GetComponent<PlayerMovement>();
        Jump = GetComponent<PlayerJump>();
        InputReceiver = GetComponent<PlayerInputHandler>();
        Inventory = GetComponent<PlayerInventory>();
    }

    public float GetCurrentHealth()
    {
        return Stats.GetCurrentHealth();
    }

    public float GetMaxHealth()
    {
        return Stats.GetMaxHealth();
    }

    public void Respawn(Vector3 position)
    {
        transform.position = position;
        Stats.ResetHealth();
    }

}
