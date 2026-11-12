using System;
using System.Collections;
using Tyu.Scripts.Modules;
using UnityEngine;
using WSH._09.SO;

namespace WSH._01.Scripts.BootsLotusBoss
{
    public class BLWaterPillar : MonoBehaviour
    {
        [Header("StatSO")] private BossStatDataSO _bossStat;
        
        [SerializeField] private Transform _centerTrm; // 공격 범위 중심
        [SerializeField] private Vector2 _range; // 범위
        
        [Header("Stat")]
        [SerializeField] private float _damage; // 공격력

        [Header("Time")]
        [SerializeField] private float _noticeTime; // 예고 시간
        [SerializeField] private float _dealTime; // 공격 받을 시간
        [SerializeField] private float _enterTime; // 우물 들어가는 시간
    
        [SerializeField] private LayerMask _whatIsPlayer;

        private void Awake()
        {
            
        }

        void Initialize()
        {
            
        }

        private void EnterState()
        {
        
        }
    
        private void UpdateState()
        {
        
        }

        void ExitState()
        {
        
        }

        public void Attack()
        {
            Collider2D[] target = Physics2D.OverlapBoxAll(_centerTrm.position, _range, _whatIsPlayer);

            foreach (var player in target)
            {
                if (player.TryGetComponent<HealthModule>(out var health))
                {
                    //health.GetDamage(_damage);
                    StartCoroutine(DealTime());
                }
            }
        }
        
        IEnumerator NoticeTime()
        {
            yield return new WaitForSeconds(_noticeTime);
            Attack();
        }

        IEnumerator DealTime()
        {
            yield return new WaitForSeconds(_dealTime);
        }
        
        public void GetBossStat(BossStatDataSO bossStat)
        {
            _bossStat = bossStat;
        }

        private void OnDrawGizmos()
        {
            if(_centerTrm == null) return;
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_centerTrm.position, _range);
        }
    }
}
