using UnityEngine;

namespace Tyu.Scripts.SO
{
    [CreateAssetMenu(fileName = "HealthSetting", menuName = "DataSetting/Health", order = 0)]
    public class StatementSettingSO : ScriptableObject
    {

        [SerializeField] private string ownerName;
        [field: SerializeField] public float defaultHealth { get; private set; }
        [field: SerializeField, Range(0,100)] public float damageReducePercentage { get; private set; }
        [field:SerializeField] public float damageReduceNum { get; private set; }
    }
}