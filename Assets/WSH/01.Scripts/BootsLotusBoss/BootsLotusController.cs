using System;
using Tyu.Scripts;
using Tyu.Scripts.Modules;
using UnityEngine;

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

        private void Update()
        {
            
        }
    }
}
