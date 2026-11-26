using System.Collections;
using Tyu.Scripts.Modules;
using UnityEngine;
using UnityEngine.Serialization;
using WSH._01.Scripts.BootsLotusBoss;
using WSH._09.SO;

public class BLDontTouch : MonoBehaviour
{
    [FormerlySerializedAs("_bossStat")]
    [Header("StatSO")]
    [SerializeField] private BLBossStatDataSO blBossStat;
    
    [SerializeField] private Transform _myself;
    
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
        _noticeTime = blBossStat.NoticeTime;
        _protectTime = blBossStat.DealTime;
        _damage = blBossStat.Damage;
    }
    
    public IEnumerator NoticeTime()
    {
        Debug.Log(3);
        yield return new WaitForSeconds(_noticeTime);
        transform.root.GetComponentInChildren<BootsLotusMovement>().DontTouchMove();
        StartCoroutine(Counter());
        yield return new WaitForSeconds(_protectTime);
        yield return new WaitForSeconds(_enterTime);
        transform.root.GetComponent<BootsLotusAttack>().SkillWork();
    }

    public IEnumerator Counter()
    {
        yield return new WaitForSeconds(_dealNoticeTime);
        var target = Physics2D.OverlapBox(transform.position, new Vector2(blBossStat.Range, blBossStat.Range), _whatIsPlayer);
        
        if(target == null)
        {
            if (target.TryGetComponent<HealthModule>(out HealthModule healthModule))
            {
                healthModule.GetDamage(_damage);
            }
        }
    }
    
    public void GetBossStat(BLBossStatDataSO blBossStat)
    {
        this.blBossStat = blBossStat;
        SettingValue();
    }
}
