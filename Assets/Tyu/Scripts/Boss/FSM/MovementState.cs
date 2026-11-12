using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class MovementState : AbstractState
    {
        protected int animHash;
        protected RenderModule renderMd;
        public MovementState(AbstractStateMachine _stateMachine, string anim,RenderModule _renderMd) : base(_stateMachine)
        {
            animHash = Animator.StringToHash(anim);
            renderMd = _renderMd;
        }

        public override void Enter()
        {
            base.Enter();
            renderMd.ChangeAnimBool(animHash, true);
        }

        public override void Exit()
        {
            base.Exit();
            renderMd.ChangeAnimBool(animHash, false);
        }
    }
}