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

        public override void Initialize(StatementSettingSO data, IHealthHavable owner)
        {
            base.Initialize(data, owner);
            _secPhaseTrigger = (MaxHp / 100) * data.secondPhaseHealth;
            _isInPhaseTwo = false;
        }

        public override void GetDamage(float damage)
        {
            base.GetDamage(damage);
            if (_currentHp > _secPhaseTrigger || _isInPhaseTwo) return;
            OnSecondPhase?.Invoke();
            _isInPhaseTwo = true;
        }
    }
}