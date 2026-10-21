using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public virtual void Init() { }

    public virtual void SetMoveDir() { }

    public virtual void TryMove() { }

    public virtual void StopMove() { }
}
