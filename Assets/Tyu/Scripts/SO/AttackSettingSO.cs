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

        [field: Space(10)]
        [Header("Attack")]
        [field: SerializeField] public float Damage { get; private set; }
        [field:SerializeField] public float Cooltime { get; private set; }
        [field:SerializeField] public float DealTime { get; private set; }
    }
}