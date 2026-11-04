using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class MovementState : AbstractState
    {
        protected int animHash;
        protected MovementModule moveMd;
        protected RenderModule renderMd;
        public MovementState(AbstractStateMachine _stateMachine, string anim, MovementModule _moveMD, RenderModule _renderMd) : base(_stateMachine)
        {
            animHash = Animator.StringToHash(anim);
            moveMd = _moveMD;
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