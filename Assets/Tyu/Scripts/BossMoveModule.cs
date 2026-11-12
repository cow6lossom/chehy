using System;
using PrimeTween;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts
{
    public class BossMoveModule : MovementModule
    {
        private bool _isStopped;

        private void FixedUpdate()
        {
            if(!_isStopped)
                TryMove();
        }

        public override void StopMove()
        {
            base.StopMove();
            _isStopped = true;
        }

        public void StartMove()
        {
            _isStopped = false;
        }

        public void UseTween(AttackMoveSO data, Vector2 dir)
        {
            dir.Normalize();
            var twData = data.Setting;
            twData.startFromCurrent = true;
            twData.endValue = RbCompo.position + dir * data.Distance;
            Tween.RigidbodyMovePosition(RbCompo, twData);
        }
    }
}