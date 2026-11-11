using System.Collections.Generic;
using System.Reflection.Metadata;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseLotusOne : HavingAnimeState
    {

        private RedbeanController _owner;
        private AttackSettingSO _data;
        
        
        public RedbeanMouseLotusOne(AbstractStateMachine _stateMachine, string anim, RenderModule renderMd, RedbeanController owner,List<AttackSettingSO> attacks) : base(_stateMachine, anim, renderMd)
        {
            _owner = owner;
            foreach (var attack in attacks)
            {
                if (attack.IsSymbol && attack.Type == StateEnum.Lotus1)
                    _data = attack;
            }
        }
        
        
        
        
        
        
    }
}