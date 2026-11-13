using Tyu.Scripts.Results;
using UnityEngine;

namespace Tyu.Scripts.Test
{
    public class TestHitter : MonoBehaviour, IDamageable
    {
        public float CurrentHp;
        public void GetDamage(float damage)
        {
            CurrentHp -= damage;
        }
    }
}