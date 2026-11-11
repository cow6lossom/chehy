using System.Collections.Generic;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseHairpinState : HavingAnimeState
    {
        private AttackSettingSO _attackData;
        private RedbeanController _owner;
        private RedbeanMouseHairpin _hairpin;
        
        
        public RedbeanMouseHairpinState(AbstractStateMachine _stateMachine, string anim,
            RenderModule renderMd,List<AttackSettingSO> attacks, RedbeanController owner, RedbeanMouseHairpin hairPin) : base(_stateMachine, anim, renderMd)
        {
            GetAttackSO.GetAttackData(attacks, AttackEnum.HairPin, ref _attackData);
            _owner = owner;
            _hairpin = hairPin;
        }

        public void Returned()
        {
            stateMachine.ChangeState(StateEnum.Chase);
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}