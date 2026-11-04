using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse.AttackScript
{
    public class FlowerWayAttack : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private SpriteRenderer sr;
        
        
        private AttackSettingSO _data;

        public void MakeWay()
        {
            var dir = target.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Deg2Rad;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            
            
            
        }



    }
}