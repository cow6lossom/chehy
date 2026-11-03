using PrimeTween;
using Tyu.Modules;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedBeanMouseSoMaeSlash : AttackState
    {
        private RedbeanController _owner;
        private int _attackCount;
        private AttackSettingSO _firAttack;
        private AttackSettingSO _lastAttack;
        
        public RedBeanMouseSoMaeSlash(AbstractStateMachine _stateMachine, string anim, AttackModule attackMd, RenderModule renderMd, 
            RedbeanController owner,PatternCoolTimeChecker coolChecker,AttackSettingSO one, AttackSettingSO two) : base(_stateMachine, anim, attackMd, renderMd, coolChecker)
        {
            _owner = owner;
            _firAttack = one;
            _lastAttack = two;
            _coolChecker.AddCoolTimer(StateEnum.SoMeaAttack, _firAttack);
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
            _coolChecker.ResetLastAtk(StateEnum.SoMeaAttack);
        }

        public override void AnimationCallback()
        {
            switch (_attackCount)
            {
                case 0:
                case 1:
                    _attackMd.TryAttack(_firAttack);
                    break;
                case 2:
                    _attackMd.TryAttack(_lastAttack);
                    break;
            }

            _attackCount++;
        }
    }
}