using System.Collections.Generic;
using PrimeTween;
using Tyu.Modules;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseManyStab : HavingAnimeState
    {
        private RedbeanController _owner;
        private AttackSettingSO _firStab;
        private AttackSettingSO _dashStab;
        private Vector2 _dir;
        
        public RedbeanMouseManyStab(AbstractStateMachine stateMachine, string anim,RenderModule renderMd, 
            RedbeanController owner,List<AttackSettingSO> attacks)
            : base(stateMachine, anim, renderMd)
        {
            _owner = owner;
            foreach (var attack in attacks)
            {
                switch (attack.AttackType)
                {
                    case AttackEnum.Stab:
                        _firStab = attack;
                        break;
                    case AttackEnum.DashStab:
                        _dashStab = attack;
                        break;
                }
            }
        }


        public override void Enter()
        {
            base.Enter();
            Vector2 dir = (_owner.transform.position - _owner.TargetPlayer.transform.position).normalized;
            _renderMd.FlipX(dir);
            
        }
        
        
        
    }
}