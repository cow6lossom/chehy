using PrimeTween;
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

    public void BossEnter(Transform bossTrm)
    {
        Tween.Position(bossTrm, new Vector3(bossTrm.position.x, -5, bossTrm.position.z), 2f, ease: Ease.InOutBack);
    }

    public void BossExit(Transform bossTrm, Vector3 pos)
    {
        bossTrm.position = pos;
        Tween.Position(bossTrm, new Vector3(pos.x, 0, pos.z), 1f);
    }
    
    public void DontTouchMove()
    {
        _lotusBossTrm.position =
            _bootsBossTrm.position.x > 0 ? _bootsBossTrm.position - new Vector3(5, 0, 0) : _bootsBossTrm.position + new Vector3(5, 0, 0);
    }
}
