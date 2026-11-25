using System;
using System.Collections;
using Tyu.Scripts.Modules;
using UnityEngine;
using WSH._09.SO;

namespace WSH._01.Scripts.BootsLotusBoss
{
    public class BLWaterPillar : MonoBehaviour
    {
        [Header("StatSO")]
        [SerializeField] private BossStatDataSO _bossStat;
        
        [SerializeField] private Transform _centerTrm; // 공격 범위 중심
        [SerializeField] private Vector2 _range; // 범위
        
        [Header("Stat")]
        [SerializeField] private float _damage; // 공격력

        [Header("Time")]
        [SerializeField] private float _noticeTime; // 예고 시간
        [SerializeField] private float _dealTime; // 공격 받을 시간
        [SerializeField] private float _enterTime; // 우물 들어가는 시간
    
        [SerializeField] private LayerMask _whatIsPlayer;
        
        void SettingValue()
        {
            _range = new Vector2(_bossStat.Range, 1.5f);
            
            _damage = _bossStat.Damage;
            _noticeTime = _bossStat.NoticeTime;
            _dealTime = _bossStat.DealTime;
            _enterTime = _bossStat.EnterTime;

            _whatIsPlayer = _bossStat.WhatIsPlayer;
        }

        void Attack()
        {
            Collider2D[] target = Physics2D.OverlapBoxAll(_centerTrm.position, _range, _whatIsPlayer);

            foreach (var player in target)
            {
                if (player.TryGetComponent<HealthModule>(out var health))
                {
                    health.GetDamage(_damage);
                }
            }
            StartCoroutine(DealTime());
        }
        
        public IEnumerator NoticeTime()
        {
            Debug.Log(1);
            yield return new WaitForSeconds(_noticeTime);
            Attack();
        }

        IEnumerator DealTime()
        {
            yield return new WaitForSeconds(_dealTime);

            yield return new WaitForSeconds(_enterTime);
            StartCoroutine(transform.parent.GetComponent<BootsLotusAttack>().NextSkill());
        }

        public void GetBossStat(BossStatDataSO bossStat)
        {
            _bossStat = bossStat;
            SettingValue();
        }

        public void Dead()
        {
            StopCoroutine(NoticeTime());
            StopCoroutine(DealTime());
        }

        private void OnDrawGizmos()
        {
            if(_centerTrm == null || _bossStat == null) return;
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_centerTrm.position, _range);
        }
    }
}
