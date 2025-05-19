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

        InputManager.Instance.CurrentReceiver = this;
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
