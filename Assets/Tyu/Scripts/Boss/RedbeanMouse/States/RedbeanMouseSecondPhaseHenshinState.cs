using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseSecondPhaseHenshinState : HavingAnimeState
    {
        private RedbeanController _owner;
        
        public RedbeanMouseSecondPhaseHenshinState(AbstractStateMachine _stateMachine, string anim, RenderModule renderMd, RedbeanController owner) : base(_stateMachine, anim, renderMd)
        {
            _owner = owner;
        }
    }
}