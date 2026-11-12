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

        public virtual void Enter()
        {
            _renderMd.ChangeAnimBool(_animHash, true);
        }

        public virtual void Exit()
        {
            _renderMd.ChangeAnimBool(_animHash, false);
        }
    }
}