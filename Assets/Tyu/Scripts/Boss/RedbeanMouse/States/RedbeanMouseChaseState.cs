using System.Collections.Generic;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseChaseState : MovementState
    {

        private RedbeanController _owner;
        private PatternCoolTimeChecker _coolChecker;
        private Vector2 dir => _owner.TargetPlayer.position - _owner.transform.position;
        private float distance => Mathf.Abs((dir).magnitude);
        private List<AttackSettingSO> _attacks;

        public RedbeanMouseChaseState(AbstractStateMachine _stateMachine, string anim, MovementModule _moveMD, RenderModule _renderMd
        ,RedbeanController owner, PatternCoolTimeChecker coolTimeChecker, List<AttackSettingSO> attacks
        ) : base(_stateMachine, anim, _moveMD, _renderMd)
        {
            _owner = owner;
            _coolChecker = coolTimeChecker;
            _attacks = attacks;

        }

        public override void UpdateState()
        {
            base.UpdateState();
            foreach (var attack in _attacks)
            {
                if (!_coolChecker.CheckCoolTime(attack.Type)) continue;
                if (distance < attack.minDetect) continue;
                if (distance > attack.maxDetect) continue;
                stateMachine.ChangeState(attack.Type);
            }
            moveMd.SetMoveDir(dir.normalized);
        }

        public override void Exit()
        {
            base.Exit();
            moveMd.StopMove();
        }
    }
}