using UnityEngine;

namespace WSH._01.Scripts.SO
{
    [CreateAssetMenu(fileName = "SnailBrideStatSO", menuName = "SO/SnailBrideStatSO")]
    public class SnailBrideStatSO : ScriptableObject
    {
        public float Damage { get; set; }
    }
}
