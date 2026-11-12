using System;
using System.Collections;
using Tyu.Scripts.Modules;
using UnityEngine;
using WSH._09.SO;

public class BLPondHand : ISkill
{
    [Header("StatSO")] private BossStatDataSO _bossStat;
    
    [SerializeField] private Player _player;
    
    [Header("Stat")]
    [SerializeField] private float _damage; // 공격력

    [Header("Time")]
    [SerializeField] private float _noticeTime; // 예고 시간
    
    [SerializeField] private float _range; // 범위
    
    [SerializeField] private LayerMask _whatIsPlayer;

   public void Attack()
    {
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

    IEnumerator NoticeTime()
    {
        yield return new WaitForSeconds(_noticeTime);
        Attack();
    }
    
    public void GetBossStat(BossStatDataSO bossStat)
    {
        _bossStat = bossStat;
    }

    private void OnDrawGizmos()
    {
        if (_player == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_player.transform.position, _range);
    }
}
