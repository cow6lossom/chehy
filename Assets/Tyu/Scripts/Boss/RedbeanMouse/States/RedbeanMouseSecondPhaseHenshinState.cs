using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseSecondPhaseHenshinState : HavingAnimeState
    {
        private RedbeanController _owner;
        private int _phaseTwoAnimBool;
        
        public RedbeanMouseSecondPhaseHenshinState(AbstractStateMachine _stateMachine, string anim, RenderModule renderMd, RedbeanController owner,
            string phaseTwoAnimBool) : base(_stateMachine, anim, renderMd)
        {
            _owner = owner;
            _phaseTwoAnimBool = Animator.StringToHash(phaseTwoAnimBool);
        }

        public override void Enter()
        {
            base.Enter();
            _renderMd.ChangeAnimBool(_phaseTwoAnimBool, true);
        }
    }
}