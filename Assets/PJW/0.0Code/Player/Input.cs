
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 Dir {  get; private set; }

    public bool IsAttack {  get; private set; }

    public bool IsInteract {  get; private set; }

    private Controls contols;

    public event Action<Vector2> OnMoveSet;

    public event Action OnAttackPressed;

    public event Action OnIsInteractPressed;

    private void Awake()
    {
        if(contols == null)
        {
            contols = new();

            contols.Player.SetCallbacks(this);
            contols.Enable();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Dir = context.ReadValue<Vector2>();
            OnMoveSet?.Invoke(Dir);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnAttackPressed?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnIsInteractPressed?.Invoke();
        }
    }
}
