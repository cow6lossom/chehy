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
        private int _attackCount;
        private AttackSettingSO _firAttack;
        private AttackSettingSO _lastAttack;
        
        public RedBeanMouseSoMaeSlash(AbstractStateMachine _stateMachine, string anim,
            RedbeanController owner, RenderModule renderMd, List<AttackSettingSO> attacks) : base(_stateMachine, anim, renderMd)
        {
            _owner = owner;
            GetDatas.GetAttackData(attacks, AttackEnum.Slash, ref _firAttack);
            GetDatas.GetAttackData(attacks, AttackEnum.CircleSlash, ref _lastAttack);
            _owner.CoolTimeChecker.AddCoolTimer(StateEnum.SoMeaAttack, _firAttack);
        }

        public override void Enter()
        {
            
            Vector2 dir = _owner.transform.position - _owner.TargetPlayer.transform.position;
            _renderMd.FlipX(dir);
            base.Enter();
            
        }

        public override void Exit()
        {
            _attackCount = 0;
            base.Exit();
            _owner.CoolTimeChecker.ResetLastAtk(StateEnum.SoMeaAttack);
            _owner.ResetLastAttack(_firAttack.DealTime);
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
                    _owner.attackMd.TryAttack(_lastAttack);
                    break;
            }

            _attackCount++;
        }
        /*
         * 死ぬなら電車は止めないように！
         * はぁ殺してやるよ。。。
         */
    }
}