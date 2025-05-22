using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour, IPlayerInputReceiver
{
    private PlayerInventory inventory;
    private CameraController cameraController;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerInteract playerInteract;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerInteract = GetComponent<PlayerInteract>();

        if (InputManager.Instance != null)
        {
            InputManager.Instance.CurrentReceiver = this;
        }

    }

    private void Start()
    {
        inventory = Player.Instance?.Inventory;
        cameraController = CameraController.Instance;
    }

    public void OnMove(Vector2 direction)
    {
        playerMovement?.OnMove(direction);
    }

    public void OnJump()
    {
        playerJump?.OnJump();
    }

    public void OnInteract()
    {
        playerInteract?.OnInteract();
    }

    public void OnSwitchView()
    {
        cameraController.ToggleView();
    }

    public void OnUseItem()
    {
        inventory.UseCurrentItem();
    }

    public void OnDropItem()
    {
        inventory.DropCurrentItem();
    }

    public void OnSwitchSlot()
    {
        inventory.SwitchSlot();
    }

    public void OnDash()
    {
        playerMovement?.TryDash();
    }

}
