using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Singleton

    public static InputManager Instance {  get; private set; }



    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeInputActions();


    }
    #endregion

    #region Input System

    // 현재 입력을 받을 객체
    public IPlayerInputReceiver CurrentReceiver { get; set; }

    // PlayerInputActions 인스턴스
    public PlayerInputActions InputActions { get; private set; }

    // 이동 입력값(Vector2)
    public Vector2 MoveInput => InputActions.Player.Move.ReadValue<Vector2>();

    public void InitializeInputActions()
    {
        InputActions = new PlayerInputActions();

        InputActions.Player.Move.performed += OnMovePerformed;
        InputActions.Player.Move.canceled += OnMoveCanceled;

        InputActions.Player.Dash.performed += OnDashPerformed;

        InputActions.Player.Jump.performed += OnJumpPerformed;
        InputActions.Player.Interact.performed += OnInteractPerformed;

        InputActions.Player.SwitchView.performed += OnSwitchView;
        InputActions.Player.UseItem.performed += OnUseItem;
        InputActions.Player.DropItem.performed += OnDropItem;
        InputActions.Player.SwitchSlot.performed += OnSwitchSlot;

        InputActions.Player.Pause.performed += OnPausePerformed;
    }

    private void OnEnable()
    {
        if (InputActions != null)
        {
            InputActions.Enable();
        }
    }
    private void OnDisable()
    {
        if (InputActions != null)
        {
            InputActions.Disable();
        }
    }

    #endregion

    #region Input Event Callbacks

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

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.IsPaused)
            GameManager.Instance.ResumeGame();
        else
            GameManager.Instance.PauseGame();
    }

    #endregion
}
