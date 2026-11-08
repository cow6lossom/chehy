using System;
using Tyu.Scripts.Results;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Modules
{
    public class HealthModule : MonoBehaviour
    {

        protected float MaxHp;
        protected float _currentHp
        {
            set => _currentHp = Mathf.Clamp(value, 0, MaxHp);
            get => _currentHp;
        }

        public event Action OnDamaged;
        public event Action OnDeath;
        protected IHealthHavable _owner;

        public virtual void Initialize(StatementSettingSO data, IHealthHavable owner)
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