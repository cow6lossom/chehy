using UnityEngine;

namespace Tyu.Scripts.SO
{
    [CreateAssetMenu(fileName = "AttackSetting", menuName = "DataSetting/AttackData", order = 0)]
    public class AttackSettingSO : ScriptableObject
    {
        [Header("Range")]
        [field:SerializeField] public float Distance { get; private set; }
        [field:SerializeField] public Vector2 Size { get; private set; }
        [field:Space(10)]
        [Header("Damage")]
        [field: SerializeField] public float DefaultDamage { get; private set; }
        

    }
}