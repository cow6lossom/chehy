using System.Collections;
using Tyu.Scripts.Modules;
using UnityEngine;
using WSH._09.SO;

public class BLPondHand : MonoBehaviour
{
    [Header("StatSO")]
    [SerializeField] private BLBossStatDataSO blBossStat;
    
    [SerializeField] private Transform _player;
    
    [Header("Stat")]
    [SerializeField] private float _damage; // 공격력

    [Header("Time")]
    [SerializeField] private float _noticeTime; // 예고 시간
    [SerializeField] private float _coolPassiveTime;
    [SerializeField] private float _lastPassiveTime;
    
    [SerializeField] private float _range; // 범위
    
    [SerializeField] private bool _passiving;
    
    [SerializeField] private LayerMask _whatIsPlayer;
    
    void SettingValue()
    {
        _damage = blBossStat.Damage;
        _noticeTime = blBossStat.NoticeTime;
        _range  = blBossStat.Range;

        _whatIsPlayer = blBossStat.WhatIsPlayer;
    }

    private void Update()
    {
        if ((_lastPassiveTime + _coolPassiveTime < Time.time) && !_passiving)
        {
            StartCoroutine(NoticeTime());
        }
    }

    void Attack()
    {
        if (_player == null) return;
        
        Collider2D target = Physics2D.OverlapCircle(_player.position, _range, _whatIsPlayer);

        if (target != null)
        {
            if (target.TryGetComponent<HealthModule>(out HealthModule health))
            {
                health.GetDamage(_damage);
            }
        }
    }

    IEnumerator NoticeTime()
    {
        _passiving = true;
        Debug.Log(2);
        transform.position = _player.position;
        yield return new WaitForSeconds(_noticeTime);
        _lastPassiveTime = Time.time;
        _passiving = false;
    }

    public void GetBossStat(BLBossStatDataSO blBossStat)
    {
        this.blBossStat = blBossStat;
        SettingValue();
    }

    public void Dead()
    {
        StopCoroutine(NoticeTime());
    }
    
    private void OnDrawGizmos()
    {
        if (_player == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
