using System;
using System.Collections;
using Tyu.Scripts.Modules;
using UnityEngine;
using WSH._01.Scripts.BootsLotusBoss;
using WSH._09.SO;

public class BLPondHand : MonoBehaviour
{
    [Header("StatSO")] private BossStatDataSO _bossStat;
    
    [SerializeField] private Player _player;
    
    [Header("Stat")]
    [SerializeField] private float _damage; // 공격력

    [Header("Time")]
    [SerializeField] private float _noticeTime; // 예고 시간
    [SerializeField] private float _coolPassiveTime;
    [SerializeField] private float _lastPassiveTime;
    
    [SerializeField] private float _range; // 범위
    
    [SerializeField] private bool _passiving;
    
    [SerializeField] private LayerMask _whatIsPlayer;

    public void EnterState()
    {
        
    }
    
    public void UpdateState()
    {
            
    }

    public void ExitState()
    {
        transform.parent.GetComponent<BootsLotusAttack>().SkillWork();
    }

    private void Update()
    {
        if (_lastPassiveTime + _coolPassiveTime > Time.time && !_passiving)
        {
            StartCoroutine(NoticeTime());
        }
    }

    void Attack()
    {
        if (_player == null) return;
        Collider2D target = Physics2D.OverlapCircle(_player.transform.position, _range, _whatIsPlayer);

        if (target != null)
        {
            if (target.TryGetComponent<HealthModule>(out HealthModule health))
            {
                health.GetDamage(_damage);
                //1초 속박 디버프
            }
        }
    }

    public IEnumerator NoticeTime()
    {
        _passiving = true;
        yield return new WaitForSeconds(_noticeTime);
        Attack();
        _lastPassiveTime = Time.time;
    }

    public void GetBossStat(BossStatDataSO bossStat)
    {
        _bossStat = bossStat;
    }

    public void Dead()
    {
        StopCoroutine(NoticeTime());
    }
    
    private void OnDrawGizmos()
    {
        if (_player == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_player.transform.position, _range);
    }
}
