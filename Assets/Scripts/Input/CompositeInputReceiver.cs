using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CompositeInputReceiver : MonoBehaviour, IPlayerInputReceiver
{
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
            Debug.Log("[CompositeInputReceiver] Set as current receiver.");
        }

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
}
