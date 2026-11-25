using Tyu.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private AttackSettingSO _attackSettingSO;
    [SerializeField] private AttackModule _AttackModule;
    public void Attack()
    {
        _AttackModule.TryAttack(_attackSettingSO);
    }
}
