using UnityEngine;

namespace WSH._09.SO
{
    [CreateAssetMenu(fileName = "BLBossListSO", menuName = "SO/BLBossListSO")]
    public class BLBossListSO : ScriptableObject
    {
        [field:SerializeField] public float Health { get; private set; }
        [field:SerializeField] public float MaxHealth { get; private set; }

        public BLBossStatDataSO[] onePageList;
        public BLBossStatDataSO[] twoPageList;
    }
}
