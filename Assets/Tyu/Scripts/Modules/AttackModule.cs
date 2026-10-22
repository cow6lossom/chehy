using Tyu.Scripts;
using Tyu.Scripts.Results;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Modules
{
    public class AttackModule : MonoBehaviour
    {
        [SerializeField] private int maxBuffer;
        [SerializeField] private ContactFilter2D target;
        private Collider2D[] _resultArray;
        
        
        public void TryAttack(AttackResult resultData)
        {
                Vector2 pos = transform.position + transform.right * resultData.myData.Distance;
                int cnt = Physics2D.OverlapBox(pos, resultData.myData.Size, transform.eulerAngles.z, target, _resultArray);
                if (cnt < 1) return;
            
                for (int i = 0; i < cnt; i++)
                {
                    var target = _resultArray[i];

                    if (target.TryGetComponent<IHealthHavable>(out var compo))
                        compo.GetDamage(resultData);
                }
        }
    }
}