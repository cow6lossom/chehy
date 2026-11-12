using System.Collections.Generic;
using System.Reflection.Metadata;
using Key._00.Scripts.Skill;
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
        private LotusFlower _usingFlower;
        private RedBeanLotusMaker _maker;
        
        public RedbeanMouseLotusOne(AbstractStateMachine _stateMachine, string anim, RenderModule renderMd, 
            RedbeanController owner,List<AttackSettingSO> attacks, RedBeanLotusMaker maker) : base(_stateMachine, anim, renderMd)
        {
            _maker = maker;
            _owner = owner;
            GetDatas.GetAttackData(attacks, AttackEnum.Lotus, ref _data);
            GetDatas.GetAttackData(_data.RandomPatterns, AttackEnum.Lotus1, ref _data);
            
        }

        public override void Enter()
        {
            base.Enter();
            _usingFlower = _maker.MakeLotus(_owner.transform.position);
            _usingFlower.gameObject.SetActive(true);
            _usingFlower.Initialize(_owner.TargetPlayer, _data);
        }
    }
}