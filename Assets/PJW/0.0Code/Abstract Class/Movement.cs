using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public virtual void Awake() { }

    public virtual void Init() { }

    public virtual void SetMoveDir(Vector2 dir) { }

    public virtual void TryMove() { }

    public virtual void StopMove() { }
}
