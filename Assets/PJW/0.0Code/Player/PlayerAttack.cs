using Tyu.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

public class PlayerAttack : AttackModule
{
    [SerializeField]private AttackSettingSO _attackSettingSO;
    public void Attack()
    {
        TryAttack(_attackSettingSO);
    }
}
