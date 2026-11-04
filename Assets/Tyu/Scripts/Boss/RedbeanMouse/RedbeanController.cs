using System;
using System.Collections.Generic;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanController : AbstractBoss, IStateMachine, IHaveDealTime
    {
        [SerializeField] private List<AttackSettingSO> attacks;
        private CanLockStateMachine _behaviourStateMachine;
        public PatternCoolTimeChecker CoolTimeChecker { get; private set; }
        public float lastAtkTime { get; private set; }
        public float dealTime { get; private set; }

        public override void Initialize(Transform _playerTrm)
        {
            CoolTimeChecker = new PatternCoolTimeChecker();
            base.Initialize(_playerTrm);
            _behaviourStateMachine = new CanLockStateMachine(this);
            _behaviourStateMachine.AddState(StateEnum.Idle, new RedbeanMouseIdle(_behaviourStateMachine, "Idle", moveMd,
                renderMd, this, this));
            _behaviourStateMachine.AddState(StateEnum.Chase, new RedbeanMouseChaseState(_behaviourStateMachine, "Chase", moveMd, 
                renderMd, this, CoolTimeChecker, new List<AttackSettingSO>()
                {
                    
                }));
        }
        
        private void MakeStates()
        {
        }

        
        public void EndTrigger()
        {
            _behaviourStateMachine.EndTrigger();
        }

        public void CallBackTrigger()
        {
            _behaviourStateMachine.CallbackTrigger();
        }

        public void ResetLastAttack(float dealTimeSet) //딜타임 초기화
        {
            lastAtkTime = Time.time;
            dealTime = dealTimeSet;
        }
        public bool IsDealTime() => lastAtkTime + dealTime <= Time.time;

        private void Update()
        {
        }

    }
}