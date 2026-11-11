using System;
using System.Collections.Generic;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanController : AbstractBoss, IStateMachine, IHaveDealTime
    {
        [SerializeField] private List<AttackSettingSO> firAttacks;
        [SerializeField] private List<AttackSettingSO> secAttacks;
        [SerializeField] private List<AttackMoveSO> attackMoves;
        private CanLockStateMachine _behaviourStateMachine;
        public GetNoneCoolAttackModule GetNoneCoolAttack { get; private set; }
        public PatternCoolTimeChecker CoolTimeChecker { get; private set; }
        public float lastAtkTime { get; private set; }
        public float dealTime { get; private set; }
        public bool _isSecPhase { get; private set; }

        public override void Initialize(Transform _playerTrm)
        {
            healthMd.OnSecondPhase += SecondPhase;
            CoolTimeChecker = new PatternCoolTimeChecker();
            base.Initialize(_playerTrm);
            _behaviourStateMachine = new CanLockStateMachine(this);
            _behaviourStateMachine.AddState(StateEnum.Idle, new RedbeanMouseIdle(_behaviourStateMachine, "Idle",
                renderMd, this, this));
            _behaviourStateMachine.AddState(StateEnum.Chase, new RedbeanMouseChaseState(_behaviourStateMachine, "Chase",
                renderMd, this, firAttacks, secAttacks));
            _behaviourStateMachine.AddState(StateEnum.SoMeaAttack, new RedBeanMouseSoMaeSlash(_behaviourStateMachine, "Somea",
                this, renderMd, firAttacks));
            _behaviourStateMachine.AddState(StateEnum.Stab, new RedbeanMouseManyStab(_behaviourStateMachine, "Stab",
                renderMd, this, firAttacks, attackMoves));


            GetNoneCoolAttack = new GetNoneCoolAttackModule(CoolTimeChecker);
        }

        private void OnDestroy()
        {
            healthMd.OnSecondPhase -= SecondPhase;
        }

        private void SecondPhase()
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