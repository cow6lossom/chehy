using Key._00.Scripts.Skill;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedBeanLotusMaker : MonoBehaviour
    {
        [SerializeField] private LotusFlower lotusOneBase;
        [SerializeField] private LotusFlower2 lotusTwoBase;

        public LotusFlower MakeLotus(Vector2 pos)
        {
            return Instantiate(lotusOneBase, pos, Quaternion.identity);
        }
        
        public LotusFlower2 MakeLotus2(Vector2 pos)
        {
            return Instantiate(lotusTwoBase, pos, Quaternion.identity);
        }
    }
}