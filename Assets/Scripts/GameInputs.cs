
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_Performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_Performed;
    }

    private void InteractAlternate_Performed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
 
}
