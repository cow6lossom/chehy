using System;
using Tyu.Scripts.Results;
using Tyu.Scripts.SO;
using Tyu.Scripts.Utils;
using UnityEngine;

namespace Tyu.Scripts.Modules
{
    public class HealthModule : MonoBehaviour
    {
        private float MaxHp;

        private float _currentHp
        {
            set => _currentHp = Mathf.Clamp(value, 0, MaxHp);
        }

        public event Action OnDamaged;
        public event Action OnDeath;
        private IHealthHavable _owner;

        public void Initialize(StatementSettingSO data, IHealthHavable owner)
        {
            MaxHp = data.defaultHealth;
            _currentHp = MaxHp;
            _owner = owner;
        }

        public void GetDamage(AttackResult data)
        {
            // float resultDamage = DamageCalculator
        }
    }
}