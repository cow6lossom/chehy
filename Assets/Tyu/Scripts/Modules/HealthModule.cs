using System;
using Tyu.Scripts.Results;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Modules
{
    public class HealthModule : MonoBehaviour
    {

        protected float MaxHp;
        protected float _currentHp;
        public event Action<float> OnHpChanged;
        public event Action OnDamaged;
        public event Action OnDeath;
        protected IDamageable _owner;

        public virtual void Initialize(StatementSettingSO data, IDamageable owner)
        {
            MaxHp = data.defaultHealth;
            _currentHp = MaxHp;
            _owner = owner;
        }

        public virtual void GetDamage(float damage)
        {
            if (_currentHp > damage)
            {
                _currentHp -= damage;
                OnDamaged?.Invoke();
            }
            else
            {
                _currentHp -= damage;
                OnDeath?.Invoke();
            }
        }
    }
}