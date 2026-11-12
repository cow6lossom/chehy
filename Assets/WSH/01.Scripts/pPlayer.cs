using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class pPlayer : MonoBehaviour
{
    public Rigidbody2D _rigid;
    
    private void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            _rigid.linearVelocityY = 3;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            _rigid.linearVelocityY = -3;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            _rigid.linearVelocityX = -3;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            _rigid.linearVelocityX = 3;
        }
        else
        {
            _rigid.linearVelocity = Vector2.zero;
        }
    }
}
