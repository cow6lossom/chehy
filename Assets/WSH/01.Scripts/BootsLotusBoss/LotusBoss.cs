using Tyu.Scripts.Modules;
using UnityEngine;

namespace WSH._01.Scripts.BootsLotusBoss
{
    public class LotusBoss : HealthModule
    {
        [SerializeField] private BootsLotusHealth _healthCompo;
            
        private void Awake()
        {
            _healthCompo = transform.parent.GetComponentInChildren<BootsLotusHealth>();
        }

        public override void GetDamage(float damage)
        {
            _healthCompo.GetDamage(damage);
        }
    }
}
