using System;
using UnityEngine;

namespace WSH._01.Scripts.SnailBrideBoss
{
    public class BachelorMovement : MovementModule
    {
        [SerializeField] private Vector2 _moveDir;
        
        [SerializeField] private float _speed;

        [SerializeField] private bool _isLive;
        [SerializeField] private bool _canMove;

        private void Awake()
        {
            
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void Update()
        {
            if (!_isLive) return;

            
            // if (!_canMove)
            //     GetDirection();
        }

        Vector2 GetDirection(Transform target)
        {
            return (target.position - transform.position).normalized;
        }
    }
}
