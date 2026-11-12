using System.Collections.Generic;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class DealTimeState : AbstractState
    {
        private Dictionary<AttackEnum, int> _dealTimeAnimDict;
        private RenderModule _renderMd;
        private BossMoveModule _moveMd;
        private int _currentHash;
        private float _startTime;
        private float _dealTime;
        private BossStateMachine stateMachine;
        
        public DealTimeState(AbstractStateMachine _stateMachine, RenderModule renderMd, BossMoveModule moveMd, DealTimeAnimListSO data) : base(_stateMachine)
        {
            _renderMd = renderMd;
            _moveMd = moveMd;
            foreach (var anim in data.DealTimeAnims)
                AddDealtTimeAnim(anim.Type, anim.dealTimeAnim);
            stateMachine = _stateMachine as BossStateMachine;
            stateMachine.GetDealTimeState(this);
        }

        private void AddDealtTimeAnim(AttackEnum key, string dealtTimeAnim) => _dealTimeAnimDict.TryAdd(key, Animator.StringToHash(dealtTimeAnim));
        public void StartDealTime(AttackEnum key, float dealTime)
        {
            _startTime = Time.time;
            _dealTime = dealTime;
            _moveMd.StopMove();
            _currentHash = _dealTimeAnimDict[key];
            _renderMd.ChangeAnimBool(_currentHash, true);
        }

        public override void UpdateState()
        {
            if (_startTime + _dealTime < Time.time)
                _renderMd.ChangeAnimBool(_currentHash, false);
        }

        public override void AnimationEndCallback()
        {
            base.AnimationEndCallback();
            stateMachine.ChangeState(StateEnum.Idle);
        }
    }
}
