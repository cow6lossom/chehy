using System.Collections.Generic;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class DealTimeState : AbstractState
    {
        private Dictionary<StateEnum, int> _animDict;
        private RenderModule _renderMd;
        private int _currentHash;
        private float _startTime;
        private float _dealTime;
        
        public DealTimeState(AbstractStateMachine _stateMachine, RenderModule renderMd) : base(_stateMachine)
        {
            _renderMd = renderMd;
        }

        public void AddDealtTimeAnim(StateEnum key, string anim)
        {
            _animDict.TryAdd(key, Animator.StringToHash(anim));
        }
    }
}