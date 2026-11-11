using PrimeTween;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts
{
    public class BossMoveModule : MovementModule
    {
        
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