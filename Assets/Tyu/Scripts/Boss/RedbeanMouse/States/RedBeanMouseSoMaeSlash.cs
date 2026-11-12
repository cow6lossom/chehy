using System.Collections.Generic;
using PrimeTween;
using Tyu.Modules;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedBeanMouseSoMaeSlash : HavingAnimeState
    {
        private RedbeanController _owner;
        private int _attackCount =0;
        private AttackSettingSO _firAttack;
        private AttackSettingSO _lastAttack;
        private BossStateMachine stateMachine;
        private bool _isAttacked= false;
        
        public RedBeanMouseSoMaeSlash(AbstractStateMachine _stateMachine, string anim,
            RedbeanController owner, RenderModule renderMd, List<AttackSettingSO> attacks) : base(_stateMachine, anim, renderMd)
        {
            _owner = owner;
            GetDatas.GetAttackData(attacks, AttackEnum.Slash, ref _firAttack);
            GetDatas.GetAttackData(attacks, AttackEnum.CircleSlash, ref _lastAttack);
            Debug.Log(_lastAttack);
            _owner.CoolTimeChecker.AddCoolTimer(AttackEnum.Slash, _firAttack);
            stateMachine = _stateMachine as BossStateMachine;
        }

        public override void Enter()
        {
            
            var dir = _owner.GetDirToPlayer();
            _renderMd.FlipX(dir);
            base.Enter();
            
        }

        public override void Exit()
        {
            _attackCount = 0;
            _isAttacked = false;
            base.Exit();
            _owner.CoolTimeChecker.ResetLastAtk(AttackEnum.Slash);
        }

        public override void AnimationCallback()
        {
            switch (_attackCount)
            {
                case 0:
                case 1:
                    _owner.attackMd.TryAttack(_firAttack);
                    break;
                case 2: 
                case 3:
                    if (_isAttacked) break;
                    _isAttacked = true;
                    _owner.attackMd.TryAttack(_lastAttack);
                    break;
            }
            _attackCount++;
        }

        public override void AnimationEndCallback()
        {
            stateMachine.ChangeToDealTime(AttackEnum.Slash, _firAttack.DealTime);
        }
        /*
         * 死ぬなら電車は止めないように！
         * はぁ殺してやるよ。。。
         */
    }
}