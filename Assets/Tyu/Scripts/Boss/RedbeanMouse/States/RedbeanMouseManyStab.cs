using PrimeTween;
using Tyu.Modules;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseManyStab : AttackState
    {
        private RedbeanController _owner;
        private AttackSettingSO _firStab;
        private AttackSettingSO _dashStab;
        private Vector2 _dir;
        
        public RedbeanMouseManyStab(AbstractStateMachine _stateMachine, string anim, AttackModule attackMd, RenderModule renderMd, 
            PatternCoolTimeChecker coolChecker, RedbeanController owner,AttackSettingSO firStab, AttackSettingSO dashStab)
            : base(_stateMachine, anim, attackMd, renderMd, coolChecker)
        {
            _owner = owner;
            _firStab = firStab;
            _dashStab = dashStab;
        }


        public override void Enter()
        {
            base.Enter();
            Vector2 dir = (_owner.transform.position - _owner.TargetPlayer.transform.position).normalized;
            _renderMd.FlipX(dir);
            
        }
        
        
        
    }
}