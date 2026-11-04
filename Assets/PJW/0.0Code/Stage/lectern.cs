using UnityEngine;

public class lectern : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask isPlayer;

    public bool IsGameOver { get; set; }

    private void Update()
    {
        if(RangePlayer())
        {
            ShowTheSelectStage();
        }
    }

    public bool RangePlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectRange,isPlayer);
        return player != null;
    }

    public void ShowTheSelectStage()
    {
        Debug.Log("상호작용 UI만들기");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
