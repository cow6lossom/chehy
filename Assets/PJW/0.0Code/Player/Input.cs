
using System;
using Tyu.Scripts.SO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 Dir {  get; private set; }

    public Vector2 MouseDir { get; private set; }

    private Controls contols;

    //Move Event <Vector2>
    public event Action<Vector2> OnMoved;


    //Attack Event <None>
    public event Action OnAttacked;


    //Dashed Event <Vector2>
    public event Action<Vector2> Dashed;


    //OnSkillUsed Event <None>
    public event Action OnSkillUsed;


    //OnInteractPressed Event <None>
    public event Action OnlnteractPressed;


    //OnBagToggIed Event <None>
    public event Action OnBagToggled;
    

    //OnItemUsed Event <None>
    public event Action OnItemUsed;


    //OnItemThrow Event <None>
    public event Action OnItemThrow;


    private void Awake()
    {
        if(contols == null)
        {
            contols = new();

            contols.Player.SetCallbacks(this);
            contols.Enable();
        }
    }

    //
    public void OnMove(InputAction.CallbackContext context)
    {
            Dir = context.ReadValue<Vector2>();
            OnMoved?.Invoke(Dir);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnAttacked?.Invoke();
        }
    }


    public void OnSkill(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnSkillUsed?.Invoke();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
       if(context.performed)
        {
            MouseDir = Mouse.current.position.ReadValue();
            Dashed?.Invoke(MouseDir);
            Debug.Log(MouseDir);
        }
    }

    public void OnOpen(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnBagToggled?.Invoke();
        }
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnItemUsed?.Invoke();
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnItemUsed?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnlnteractPressed?.Invoke();
        }
    }
}
