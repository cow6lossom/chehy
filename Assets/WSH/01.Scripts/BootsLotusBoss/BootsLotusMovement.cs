using UnityEngine;
using WSH._01.Scripts.BootsLotusBoss;

public class BootsLotusMovement : MonoBehaviour
{
    [SerializeField] private BossAnimation _animation;

    [SerializeField] private Transform _bootsBossTrm;
    [SerializeField] private Transform _lotusBossTrm;

    private void Awake()
    {
        _bootsBossTrm = transform.parent.GetComponentInChildren<BootsBoss>().transform;
        _lotusBossTrm = transform.parent.GetComponentInChildren<LotusBoss>().transform;

        _animation = transform.parent.GetComponentInChildren<BossAnimation>();
    }

    public void DontTouchMove()
    {
        _lotusBossTrm.position = _bootsBossTrm.position + new Vector3(5,0,0);
    }
}
