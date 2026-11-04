using Tyu.Modules;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class AttackState : AbstractState
    {
        protected AttackModule _attackMd;
        protected int _animHash;
        protected RenderModule _renderMd;
        protected PatternCoolTimeChecker _coolChecker;
        public AttackState(AbstractStateMachine _stateMachine, string anim ,AttackModule attackMd, RenderModule renderMd, PatternCoolTimeChecker coolChecker) : base(_stateMachine)
        {
            
            _animHash = Animator.StringToHash(anim);
            _attackMd = attackMd;
            _renderMd = renderMd;
        }

        public virtual void Enter()
        {
            base.Enter();
            _renderMd.ChangeAnimBool(_animHash, true);
        }

        public virtual void Exit()
        {
            base.Exit();
            _renderMd.ChangeAnimBool(_animHash, false);
        }
    }
}