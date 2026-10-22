using UnityEngine;

public abstract class MovementModule : MonoBehaviour
{
    public Rigidbody2D RbCompo { get; private set; }

    public Vector2 Dir {  get; private set; }

    private float speed;

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            if (value > 0)
            {
                speed = value;
            }
        }
    }

    public virtual void Init() 
    {
        RbCompo = GetComponentInParent<Rigidbody2D>();
    }

    public virtual void SetMoveDir(Vector2 dir)
    {
        this.Dir = dir;
    }

    public virtual void TryMove() 
    {
        RbCompo.linearVelocity = (Dir.normalized)*speed;
    }

    public virtual void StopMove() 
    {
        RbCompo.linearVelocity = Vector2.zero;
    }
}
