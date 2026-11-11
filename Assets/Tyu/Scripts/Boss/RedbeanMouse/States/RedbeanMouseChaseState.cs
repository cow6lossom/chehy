using System.Collections.Generic;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseChaseState : MovementState
    {

        private RedbeanController _owner;
        private Vector2 dir => _owner.TargetPlayer.position - _owner.transform.position;
        private float distance => Mathf.Abs((dir).magnitude);
        private List<AttackSettingSO> _firAttacks;
        private List<AttackSettingSO> _secAttacks;

        public RedbeanMouseChaseState(AbstractStateMachine _stateMachine, string anim,RenderModule _renderMd
        ,RedbeanController owner,List<AttackSettingSO> firstAttacks, List<AttackSettingSO> secAttacks
        ) : base(_stateMachine, anim,_renderMd)
        {
            _owner = owner;
            _firAttacks = firstAttacks;
            _secAttacks = secAttacks;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (_owner._isSecPhase)
            {
                if (!_owner.GetNoneCoolAttack.TryGetAttackData(distance, _firAttacks, out var data))
                    stateMachine.ChangeState(data.Type);
            }
            else
            {
                if(!_owner.GetNoneCoolAttack.TryGetAttackData(distance, _secAttacks, out var data))
                    stateMachine.ChangeState(data.Type);
            }
                
            
            _owner.moveMd.SetMoveDir(dir.normalized);
        }

        

        public override void Exit()
        {
            base.Exit();
            _owner.moveMd.StopMove();
        }
    }
}