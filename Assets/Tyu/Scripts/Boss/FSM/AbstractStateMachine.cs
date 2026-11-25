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
            stateDict = new();
            owner = _owner;
        }
        
        public virtual void Initialize(StateEnum keyValue)
        {
            if (!stateDict.TryGetValue(keyValue, out var value))
            {
                Debug.LogError("존재하지 않는 키값 대입 이유로 상태 추가 취소");
                return;
            }
            currentState = value;
            currentState.Enter();
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
            currentState.Exit();
            currentState = value;
            Debug.Log(currentState);
            currentState.Enter();
            Debug.Log(3423);
        }

        public virtual void UpdateState()
        {
            currentState.UpdateState();
        }

        public virtual void EndTrigger() => currentState.AnimationEndCallback();
        public virtual void CallbackTrigger() => currentState.AnimationCallback();

        
        /*
         * 死んだ瞳で爆弾片手に口を開く
         * さよならだ人類、
         * みんな吹き飛んじまえ
         */
    }
}