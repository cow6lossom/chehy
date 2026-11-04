using System;
using Tyu.Scripts.Boss.FSM;
using UnityEngine;

namespace Tyu.Scripts.SO
{
    [CreateAssetMenu(fileName = "AttackSetting", menuName = "DataSetting/AttackData", order = 0)]
    public class AttackSettingSO : ScriptableObject
    {
        [Header("Range")]
        [field:SerializeField] public float Distance { get; private set; }
        [field:SerializeField] public Vector2 Size { get; private set; }
        [field:SerializeField] public float maxDetect { get; private set; }
        [field:SerializeField] public float minDetect { get; private set; }
        [field: Space(10)]
        [Header("Attack")]
        [field:SerializeField] public StateEnum Type { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field:SerializeField] public float Cooltime { get; private set; }
        [field:SerializeField] public float DealTime { get; private set; }

        private void OnValidate()
        {
            minDetect = Mathf.Clamp(minDetect, 0, maxDetect);
            maxDetect = Mathf.Clamp(maxDetect, 0, float.MaxValue);
        }
    }
}