using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }
    public IPlayerInputReceiver CurrentReceiver { get; set; }

    public PlayerInputActions inputActions { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Jump.performed += OnJumpPerformed;
        inputActions.Player.Interact.performed += OnInteractPerformed;
    }



    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        CurrentReceiver?.OnMove(context.ReadValue<Vector2>());
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        CurrentReceiver?.OnJump();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        CurrentReceiver?.OnInteract();
    }
}
