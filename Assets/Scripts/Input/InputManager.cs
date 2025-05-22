using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }
    public IPlayerInputReceiver CurrentReceiver { get; set; }

    public PlayerInputActions inputActions { get; private set; }

    public Vector2 MoveInput => inputActions.Player.Move.ReadValue<Vector2>();

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
        inputActions.Player.Move.canceled += OnMoveCanceled;

        inputActions.Player.Dash.performed += OnDashPerformed;

        inputActions.Player.Jump.performed += OnJumpPerformed;
        inputActions.Player.Interact.performed += OnInteractPerformed;

        inputActions.Player.SwitchView.performed += OnSwitchView;
        inputActions.Player.UseItem.performed += OnUseItem;
        inputActions.Player.DropItem.performed += OnDropItem;
        inputActions.Player.SwitchSlot.performed += OnSwitchSlot;
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

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        CurrentReceiver?.OnMove(Vector2.zero);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        CurrentReceiver?.OnJump();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        CurrentReceiver?.OnInteract();
    }

    private void OnSwitchView(InputAction.CallbackContext context)
    {
        CurrentReceiver?.OnSwitchView();
    }

    private void OnUseItem(InputAction.CallbackContext context)
    {
        CurrentReceiver?.OnUseItem();
    }

    private void OnDropItem(InputAction.CallbackContext context)
    {
        CurrentReceiver?.OnDropItem();
    }

    private void OnSwitchSlot(InputAction.CallbackContext context)
    {
        CurrentReceiver?.OnSwitchSlot();
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        CurrentReceiver?.OnDash();
    }
}
