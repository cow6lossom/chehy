using Tyu.Scripts.Modules;
using UnityEngine;

namespace WSH._01.Scripts.BootsLotusBoss
{
    public class BootsLotusHealth : HealthModule
    {
        [SerializeField] private BootsLotusAttack _attackCompo;

        private void Awake()
        {
            _attackCompo = transform.parent.GetComponentInChildren<BootsLotusAttack>();
        }

        public override void GetDamage(float damage)
        {
            base.GetDamage(damage);
            
            if (_currentHp <= 0)
            {
                _attackCompo.RaiseDead();
            }
        }
    }
}
