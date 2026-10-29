using Tyu.Scripts.Results;
using UnityEngine;

namespace Tyu.Scripts.Test
{
    public class TestHitter : MonoBehaviour, IHealthHavable
    {
        public float CurrentHp;
        public void GetDamage(AttackResult data)
        {
            CurrentHp -= data.resultDamage;
        }
    }
}