using System;
using System.Collections;
using Tyu.Scripts;
using Tyu.Scripts.Modules;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

namespace WSH._01.Scripts.BootsLotusBoss
{
    public class BootsLotus : AbstractBoss
    {
        [Header("Boss")]
        [SerializeField] private BootsBoss _bootsBoss;
        [SerializeField] private LotusBoss _lotusBoss;

        [Header("Scripts")]
        [SerializeField] private BootsLotusHealth _health;
        [SerializeField] private BootsLotusAttack _attack;

        [Header("Time")] [SerializeField] private float _attackBeginTime;

        void Awake()
        {
            InitializeCompo();
            
            StartCoroutine(OnAttack());
        }

        public override void Initialize(Transform _playerTrm)
        {
            base.Initialize(_playerTrm);
        }

        void InitializeCompo()
        {
            _bootsBoss = GetComponentInChildren<BootsBoss>();
            _lotusBoss = GetComponentInChildren<LotusBoss>();
            
            _health = GetComponentInChildren<BootsLotusHealth>();
            _attack = GetComponentInChildren<BootsLotusAttack>();
        }

        IEnumerator OnAttack()
        {
            yield return new WaitForSeconds(_attackBeginTime);
            _attack.SkillWork();
        }
    }
}
