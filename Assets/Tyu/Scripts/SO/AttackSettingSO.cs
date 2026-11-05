using System;
using Tyu.Scripts.Boss.FSM;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

namespace Tyu.Scripts.SO
{
    [CreateAssetMenu(fileName = "AttackSetting", menuName = "DataSetting/Attack/Data", order = 0)]
    public class AttackSettingSO : ScriptableObject
    {
        [Header("Range")]
        [field:SerializeField, Min(0)] public float Distance { get; private set; }
        [field:SerializeField] public Vector2 Size { get; private set; }
        [field: Space(10)]
        [Header("Attack")]
        [field:SerializeField] public StateEnum Type { get; private set; }
        [field:SerializeField] public AttackEnum AttackType { get; private set; }
        [field:SerializeField] public bool IsSymbol { get; private set; }
        [field: SerializeField, Min(0) ] public float Damage { get; private set; }
        [field:SerializeField,Min(0) ] public float CoolTime { get; private set; }
        [field:SerializeField, Min(0)] public float DealTime { get; private set; }
        [field:SerializeField, Min(0)] public float maxDetect { get; private set; }
        [field:SerializeField, Min(0)] public float minDetect { get; private set; }

        private void OnValidate()
        {
            maxDetect = Mathf.Clamp(maxDetect, 0, float.MaxValue);
        }
    }
}