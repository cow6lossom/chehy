using System.Collections.Generic;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseHairpinState : HavingAnimeState
    {
        private AttackSettingSO _attackData;
        private RedbeanController _owner;
        private RedbeanMouseHairpin _hairpin;
        private int _animTriggerCnt;
        private Vector2 _dir;
        
        
        public RedbeanMouseHairpinState(AbstractStateMachine _stateMachine, string anim,
            RenderModule renderMd,List<AttackSettingSO> attacks, RedbeanController owner, RedbeanMouseHairpin hairPin) : base(_stateMachine, anim, renderMd)
        {
            GetDatas.GetAttackData(attacks, AttackEnum.HairPin, ref _attackData);
            _owner = owner;
            _hairpin = hairPin;
        }

        public void Returned()
        {
            stateMachine.ChangeState(StateEnum.Chase);
        }

        public override void Enter()
        {
            base.Enter();
            _dir = (_owner.TargetPlayer.position - _owner.transform.position).normalized;
            _animTriggerCnt = 0;
        }

        public override void AnimationCallback()
        {
            base.AnimationCallback();
            switch (_animTriggerCnt)
            {
                case 1 : 
                    _hairpin.SetLine(_owner.transform.position + (Vector3)(_dir * 2000));
                    break;
                case 2:
                    _hairpin.Shoot(_dir, _owner.transform.position);
                    break;
                case 3:
                    _hairpin.SetLine(_owner.TargetPlayer.position);
                    break;
                case 4:
                    _hairpin.Returning(_owner.TargetPlayer.position);
                    break;
            }

            _animTriggerCnt++;
        }

        public override void Exit()
        {
            base.Exit();
            _owner.CoolTimeChecker.ResetLastAtk(_attackData.Type);
            _owner.ResetLastAttack(_attackData.DealTime);
        }
    }
}