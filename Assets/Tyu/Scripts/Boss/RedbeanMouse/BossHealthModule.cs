using System;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class BossHealthModule : HealthModule
    {

        public event Action OnSecondPhase;
        private float _secPhaseTrigger;
        private bool _isInPhaseTwo;
        private bool _isDead;

        public override void Initialize(StatementSettingSO data, IDamageable owner)
        {
            base.Initialize(data, owner);
            _secPhaseTrigger = (MaxHp / 100) * data.secondPhaseHealth;
            _isInPhaseTwo = false;
            OnDeath += ActiveDeath;
        }
        
        private void ActiveDeath() //2페이즈 넘어가기 전에 죽으면 2페이지 이벤트 취소
        {
            _isDead = true;
            OnDeath -= ActiveDeath;
        }

        public override void GetDamage(float damage)
        {
            base.GetDamage(damage);
            if (_currentHp > _secPhaseTrigger || _isInPhaseTwo) return;
            if (_isDead) return;
            OnSecondPhase?.Invoke();
            _isInPhaseTwo = true;
        }
    }
}