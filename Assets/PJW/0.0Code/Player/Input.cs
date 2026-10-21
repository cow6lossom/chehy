
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    public Vector2 Dir {  get; private set; }

    public void OnMove(InputValue value)
    {
        Dir = value.Get<Vector2>();
    }
}
