using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovment : Movement
{
    private Rigidbody2D rbCompo;

    private float speed;

    public Vector2 Dir { get; private set; }

    public override void Awake() => Init();

    private void Update() => TryMove();

    public override void Init() => rbCompo = GetComponent<Rigidbody2D>();

    public override void SetMoveDir(Vector2 dir) => this.Dir = dir;

    public override void TryMove() => rbCompo.linearVelocity = Dir.normalized*speed;

    public override void StopMove() => rbCompo.linearVelocity = Vector2.zero;
}
