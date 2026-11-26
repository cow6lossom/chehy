using System;
using System.Collections.Generic;
using Key._00.Scripts.Skill;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanController : AbstractBoss, IStateMachine
    {
        [SerializeField] private DealTimeAnimListSO dealTimeList;
        [SerializeField] private List<AttackSettingSO> firAttacks;
        [SerializeField] private List<AttackSettingSO> secAttacks;
        [SerializeField] private List<AttackMoveSO> attackMoves;
        [SerializeField] private RedBeanLotusMaker lotusMaker;
        
        private BossStateMachine _behaviourStateMachine;
        public GetNoneCoolAttackModule GetNoneCoolAttack { get; private set; }
        public PatternCoolTimeChecker CoolTimeChecker { get; private set; }
        public float lastAtkTime { get; private set; }
        public float dealTime { get; private set; }
        public bool _isSecPhase { get; private set; }
        [SerializeField] private Transform player;

        private void Start()=> Initialize(player);
        
        public override void Initialize(Transform _playerTrm)
        {
            base.Initialize(_playerTrm);
            moveMd.Init();
            healthMd.Initialize(bossStat, this);
            moveMd.Speed = bossStat.FirSpeed;
            healthMd.OnSecondPhase += SecondPhase;
            CoolTimeChecker = new PatternCoolTimeChecker();
            _behaviourStateMachine = new BossStateMachine(this);
            _behaviourStateMachine.AddState(StateEnum.Idle, new RedbeanMouseIdle(_behaviourStateMachine, "Idle",
                renderMd, this));
            _behaviourStateMachine.AddState(StateEnum.Chase, new RedbeanMouseChaseState(_behaviourStateMachine, "Walk",
                renderMd, this, firAttacks, secAttacks));
            _behaviourStateMachine.AddState(StateEnum.SleeveAttack, new RedBeanMouseSoMaeSlash(_behaviourStateMachine, "SleeveAttack",
                this, renderMd, firAttacks));
            _behaviourStateMachine.AddState(StateEnum.Lotus1, new RedbeanMouseLotusOne(_behaviourStateMachine,  "Lotus1", renderMd, this, firAttacks, lotusMaker));
            _behaviourStateMachine.AddState(StateEnum.Lotus2, new RedBeanMouseLotusTwo(_behaviourStateMachine,  "Lotus2", renderMd, this, firAttacks, lotusMaker));

            var dealTime = new DealTimeState(_behaviourStateMachine, renderMd, moveMd, dealTimeList);

            GetNoneCoolAttack = new GetNoneCoolAttackModule(CoolTimeChecker);
            _behaviourStateMachine.Initialize(StateEnum.Idle);
            Debug.Log(3);
            renderMd.Initialize(this, transform);
        }

        private void OnDestroy()
        {
            healthMd.OnSecondPhase -= SecondPhase;
        }

        private void SecondPhase()
        {
            _behaviourStateMachine.ChangeState(StateEnum.Phase2);
        }
        public void EndTrigger()
        {
            _behaviourStateMachine.EndTrigger();
        }

        public void CallBackTrigger()
        {
            _behaviourStateMachine.CallbackTrigger();
        }
        private void Update()
        {
            _behaviourStateMachine.UpdateState();
        }

        public Vector2 GetDirToPlayer()
        {
            Debug.Log("used");
            return (TargetPlayer.position - transform.position).normalized;
        }

    }
}