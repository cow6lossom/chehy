using UnityEngine;

namespace WSH._09.SO
{
    [CreateAssetMenu(fileName = "BossListSO", menuName = "SO/BossListSO")]
    public class BLBossListSO : ScriptableObject
    {
        [field:SerializeField] public float Health { get; private set; }
        [field:SerializeField] public float MaxHealth { get; private set; }

        public BLBossStatDataSO[] list;
    }
}
