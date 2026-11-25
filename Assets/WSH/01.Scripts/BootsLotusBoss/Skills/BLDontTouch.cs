using System.Collections;
using Tyu.Scripts.Modules;
using Unity.VisualScripting;
using UnityEngine;
using WSH._01.Scripts.BootsLotusBoss;
using WSH._09.SO;

public class BLDontTouch : MonoBehaviour
{
    [Header("StatSO")]
    [SerializeField] private BossStatDataSO _bossStat;
    
    [Header("Time")]
    [SerializeField] private float _noticeTime; // 예고 시간
    [SerializeField] private float _protectTime; // 공격 받을 시간
    [SerializeField] private float _enterTime; // 우물 들어가는 시간
    
    [SerializeField] private float _dealNoticeTime;
    [SerializeField] private float _dealTime;

    [SerializeField] private float _damage;

    [SerializeField] private LayerMask _whatIsPlayer;
    
    void SettingValue()
    {
        _noticeTime = _bossStat.NoticeTime;
        _protectTime = _bossStat.DealTime;
        _damage = _bossStat.Damage;
    }
    
    public IEnumerator NoticeTime()
    {
        Debug.Log(3);
        yield return new WaitForSeconds(_noticeTime);
        transform.parent.GetComponentInChildren<BootsLotusMovement>().DontTouchMove();
        StartCoroutine(Counter());
        yield return new WaitForSeconds(_protectTime);
        yield return new WaitForSeconds(_enterTime);
        transform.parent.GetComponent<BootsLotusAttack>().SkillWork();
    }

    public IEnumerator Counter()
    {
        yield return new WaitForSeconds(_dealNoticeTime);
        var target = Physics2D.OverlapBox(transform.position, new Vector2(_bossStat.Range, _bossStat.Range), _whatIsPlayer);
        
        if(target == null)
        {
            if (target.TryGetComponent<HealthModule>(out HealthModule healthModule))
            {
                healthModule.GetDamage(_damage);
            }
        }
    }
    
    public void GetBossStat(BossStatDataSO bossStat)
    {
        _bossStat = bossStat;
        SettingValue();
    }
}
