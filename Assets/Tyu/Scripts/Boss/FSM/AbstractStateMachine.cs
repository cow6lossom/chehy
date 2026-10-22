using System.Collections.Generic;
using UnityEngine;

namespace Tyu.Scripts.Boss.FSM
{
    public abstract class AbstractStateMachine
    {
        protected Dictionary<StateEnum, AbstractState> stateDict;
        protected AbstractState currentState;
        protected IStateMachine owner;

        public AbstractStateMachine(IStateMachine _owner)
        {
            owner = _owner;
        }
        
        public virtual void Initialize(StateEnum keyValue)
        {
            ChangeState(keyValue);
        }
        
        public void AddState(StateEnum keyValue, AbstractState state)
        {
            if (!stateDict.TryAdd(keyValue, state))
                Debug.LogError("중복된 키값 존재 이유로 상태 추가 취소");
        }
        
        public virtual void ChangeState(StateEnum keyValue)
        {
            if (!stateDict.TryGetValue(keyValue, out var value))
            {
                Debug.LogError("존재하지 않는 키값 대입 이유로 상태 추가 취소");
                return;
            }
            currentState.Enter();
            currentState = value;
            currentState.Exit();
        }

        public virtual void UpdateState()
        {
            currentState.UpdateState();
        }
        
    }
}