using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReaderSO : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions, GameInput.IDialogueActions
{
    private GameInput input;

    public void SetCursorState(bool newState)
    {
        //Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !newState;
    }

    private void OnEnable()
    {
        if (input == null)
        {
            input = new GameInput();

            input.Gameplay.SetCallbacks(this);
            input.Dialogue.SetCallbacks(this);
            input.UI.SetCallbacks(this);

            SetGamePlayInput();
        }
    }
    private void OnDisable()
    {
        DisableAllInput();
    }
    public void SetGamePlayInput()
    {
        input.Gameplay.Enable();
        input.Dialogue.Disable();
        input.UI.Disable();
        SetCursorState(true);
    }
    public void SetDialogueInput()
    {
        input.Gameplay.Disable();
        input.Dialogue.Enable();
        input.UI.Disable();
        SetCursorState(false);
    }
    public void SetUIInput()
    {
        input.Gameplay.Disable();
        input.Dialogue.Disable();
        input.UI.Enable();
        SetCursorState(false);
    }
    public void DisableAllInput()
    {
        input.Gameplay.Disable();
        input.UI.Disable();
        input.Dialogue.Disable();
        SetCursorState(false);
    }

    //Gameplay
    public event Action<Vector2> MoveEvent;
    public event Action LeftMouseEvent;
    public event Action LeftMouseCanceledEvent;
    public event Action InteractEvent;
    public event Action InteractAlternateEvent;
    public event Action DropEvent;
    public event Action RightMouseEvent;
    public event Action RightMouseCanceledEvent;
    public event Action OpenInventoryEvent;
    public event Action CLoseInventoryEvent;

    public event Action PauseEvent;

    public event Action<Vector2, bool> CameraMoveEvent;
    public event Action EnableMouseControlCameraEvent;
    public event Action DisableMouseControlCameraEvent;

    //Dialogues
    public event Action NextEvent;

    //UI
    public event Action MenuMouseMoveEvent;
    public event Action ResumeEvent;


    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            LeftMouseEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            LeftMouseCanceledEvent?.Invoke();
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractEvent?.Invoke();
        }
    }
    public void OnInteractAlternate(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractAlternateEvent?.Invoke();
        }
    }
    public void OnDrop(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            DropEvent?.Invoke();
        }
    }
    public void OnRightMouse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            RightMouseEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            RightMouseCanceledEvent?.Invoke(); 
        }
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            //SetUIInput();
        }
    }
    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OpenInventoryEvent?.Invoke();
            //SetUIInput();
        }
    }
    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            CLoseInventoryEvent?.Invoke();
            //SetGamePlayInput();
        }
    }
    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            //SetGamePlayInput();
        }
    }
    public void OnNext(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            NextEvent?.Invoke();
        }
    }



    // Not working
    private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "KeyboardMouse";
    public void OnMouseControlCamera(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            EnableMouseControlCameraEvent?.Invoke();

        if (context.phase == InputActionPhase.Canceled)
            DisableMouseControlCameraEvent?.Invoke();
    }
    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            MenuMouseMoveEvent?.Invoke();
    }
    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        CameraMoveEvent?.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

}
