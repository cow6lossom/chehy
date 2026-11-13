using System;
using Tyu.Scripts.Results;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Modules
{
    public class HealthModule : MonoBehaviour
    {

        public float MaxHp { get; protected set; }
        public float _currentHp { get; protected set; }
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
            _currentHp -= damage;
            OnDamaged?.Invoke();
            if(_currentHp <= 0)
            {
                _currentHp = 0;
                OnDeath?.Invoke();
            }
            OnHpChanged?.Invoke(_currentHp);
        }
        public virtual void HealHp(float value)
        {
            _currentHp += value;
            _currentHp = Mathf.Clamp(_currentHp, 0, MaxHp);
            OnHpChanged?.Invoke(_currentHp);
        }
    }
}