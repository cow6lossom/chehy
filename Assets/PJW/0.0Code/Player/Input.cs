
using System;
using Tyu.Scripts.SO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 Dir {  get; private set; }

    private Controls contols;

    //Move Event <Vector2>
    public event Action<Vector2> OnMoveSet;

    //Attack Event <None>
    public event Action Attack;


    public event Action Dash;


    public event Action Skill;


    public event Action IsInteractPressed;


    public event Action OpenThebag;
    


    public event Action UseItem;


    public event Action UseSkill;




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
            OnMoveSet?.Invoke(Dir);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Attack?.Invoke();
        }
    }


    public void OnSkill(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("스킬");
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
       if(context.performed)
        {
            Debug.Log("대시");
        }
    }

    public void OnOpen(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("복주머니 열기");
            Debug.Log("복주머니 닫기");   
        }
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("아이템 사용하기");
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("아이템 버리기");
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("대시");
        }
    }
}
