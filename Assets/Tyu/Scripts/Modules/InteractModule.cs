using System;
using UnityEngine;

namespace Tyu.Scripts.Modules
{
    public class InteractModule : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private bool debug;
        [SerializeField] private ContactFilter2D target;
        private readonly int _maxBuffer = 5;
        private Collider2D[] _resultArr;
        
        private void Awake()
        {
            _resultArr = new Collider2D[_maxBuffer];
        }

        public void TryInteract()
        {
            int cnt = Physics2D.OverlapCircle(transform.position, radius, target, _resultArr);
            if (cnt < 1) return;
            float _minDistance = float.MaxValue;
            IInteratable _target = null;
            for (int i = 0; i < cnt; ++i)
            {
                var cur = _resultArr[i];
                if (!cur.TryGetComponent<IInteratable>(out var interact)) continue;
                var dis = (transform.position - cur.transform.position).magnitude;
                if (_minDistance < dis) continue;
                _minDistance = dis;
                _target = interact;
            }

            if (_target == null) return;
            _target.InteractEffect();
        }

        private void OnDrawGizmos()
        {
            if (!debug) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}