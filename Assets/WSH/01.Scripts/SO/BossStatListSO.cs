using UnityEngine;

namespace WSH._09.SO
{
    [CreateAssetMenu(fileName = "BossListSO", menuName = "SO/BossListSO")]
    public class BossListSO : ScriptableObject
    {
        public BossStatDataSO[] list;
    }
}
