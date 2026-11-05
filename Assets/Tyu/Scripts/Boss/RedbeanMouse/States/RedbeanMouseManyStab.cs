using System.Collections.Generic;
using PrimeTween;
using Tyu.Modules;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseManyStab : HavingAnimeState
    {
        private RedbeanController _owner;
        private AttackSettingSO _firStab;
        private AttackSettingSO _dashStab;
        private AttackMoveSO _firMove;
        private AttackMoveSO _dashMove;
        private Vector2 _dir;
        private int _attackCount;
        private bool _isDashAttacking;
        
        public RedbeanMouseManyStab(AbstractStateMachine stateMachine, string anim,RenderModule renderMd, 
            RedbeanController owner,List<AttackSettingSO> attacks, List<AttackMoveSO> moves)
            : base(stateMachine, anim, renderMd)
        {
            _owner = owner;
            foreach (var attack in attacks)
            {
                switch (attack.AttackType)
                {
                    case AttackEnum.Stab:
                        _firStab = attack;
                        break;
                    case AttackEnum.DashStab:
                        _dashStab = attack;
                        break;
                }
            }
            foreach (var move in moves)
            {
                switch (move.Type)
                {
                    case AttackEnum.Stab:
                        _firMove = move;
                        break;
                    case AttackEnum.DashStab:
                        _dashMove = move;
                        break;
                }
            }
        }


        public override void Enter()
        {
            base.Enter();
            Vector2 dir = (_owner.transform.position - _owner.TargetPlayer.transform.position).normalized;
            _renderMd.FlipX(dir);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if(_isDashAttacking)
                _owner.attackMd.TryAttack(_dashStab);
        }

        public override void AnimationCallback()
        {
            base.AnimationCallback();
            switch (_attackCount)
            {
                case 0: 
                case 1: 
                    _owner.attackMd.TryAttack(_firStab);
                    _owner.moveMd.UseTween(_firMove, _dir);
                    break;
                case 2:
                    _isDashAttacking = true;
                    _owner.moveMd.UseTween(_dashMove, _dir);
                    break;
            }
        }

        public override void Exit()
        {
            base.Exit();
            _owner.CoolTimeChecker.ResetLastAtk(StateEnum.Stab);
            _owner.ResetLastAttack(_firStab.DealTime);
        }
    }
}