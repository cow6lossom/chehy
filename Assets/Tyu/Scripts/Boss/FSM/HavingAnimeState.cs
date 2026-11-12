using Tyu.Modules;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class HavingAnimeState : AbstractState
    {
        protected int _animHash;
        protected RenderModule _renderMd;
        public HavingAnimeState(AbstractStateMachine _stateMachine, string anim, RenderModule renderMd) : base(_stateMachine)
        {
            
            _animHash = Animator.StringToHash(anim);
            _renderMd = renderMd;
        }

        public override void Enter()
        {
            base.Enter();
            _renderMd.ChangeAnimBool(_animHash, true);
        }

        public override void Exit()
        {
            base.Exit();
            _renderMd.ChangeAnimBool(_animHash, false);
        }
    }
}