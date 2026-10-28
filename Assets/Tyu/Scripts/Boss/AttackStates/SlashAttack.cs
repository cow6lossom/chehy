using Tyu.Modules;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class SlashAttack : AbstractState
    {
        private AttackModule _attackMd;
        private AttackSettingSO _data;
        private int _animHash;
        public SlashAttack(AbstractStateMachine _stateMachine, string anim ,AttackModule attackMd, AttackSettingSO data) : base(_stateMachine)
        {
            _animHash = Animator.StringToHash(anim);
            _attackMd = attackMd;
            _data = data;
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}