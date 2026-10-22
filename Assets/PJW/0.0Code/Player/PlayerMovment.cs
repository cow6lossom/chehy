using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovment : MovementModule
{
    private void Start()
    {
        base.Init();
    }

    private void FixedUpdate()
    {
        TryMove();
    }

    public override void Init()
    {
        base.Init();
        Speed = 10;
    }

    public override void SetMoveDir(Vector2 dir)
    {
        base.SetMoveDir(dir);
    }

    public override void TryMove()
    {
        base.TryMove();
    }

    public override void StopMove()
    {
        base.StopMove();
    }
}
