using Tyu.Scripts.Modules;
using UnityEngine;

namespace WSH._01.Scripts.SnailBride
{
    public class SnailBrideHealth :  HealthModule, IHealthHavable
    {
        public override void GetDamage(float damage)
        {
            _currentHp = GetDamage(damage, true);
            
            // OnDamaged?.Invoke(); -- 이거 보류점
            // if(_currentHp <= 0)
            // {
            //     _currentHp = 0;
            //     OnDeath?.Invoke();
            // }
            // OnHpChanged?.Invoke(_currentHp);
        }

        float GetDamage(float damage, bool secondPage)
        {
            _currentHp -= damage;
            if (secondPage)
                return _currentHp = Mathf.Clamp(_currentHp, 2501, MaxHp);
            return _currentHp = Mathf.Clamp(_currentHp, 0, MaxHp);
        }

        public float GetCurrentHp()
        {
            return _currentHp;
        }

        public float GetMaxHp()
        {
            return MaxHp;
        }

        public void HealHp(float value)
        {
            _currentHp = Mathf.Clamp(_currentHp + value, 0, MaxHp);
        }

        public HealthModule GetHealthModule()
        {
            return this;
        }
    }
}
