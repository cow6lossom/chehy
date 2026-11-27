using UnityEngine;

namespace WSH._01.Scripts.SO
{
    [CreateAssetMenu(fileName = "SnailBrideListSO", menuName = "SO/SnailBrideListSO")]
    public class SnailBrideListSO : ScriptableObject
    {
        [field:SerializeField] public float Health { get; private set; }
        [field:SerializeField] public float MaxHealth { get; private set; }

        public SnailBrideStatSO[] list;
    }
}
