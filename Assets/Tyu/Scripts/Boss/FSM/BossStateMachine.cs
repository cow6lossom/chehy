using Tyu.Scripts.Boss.RedbeanMouse;
using Tyu.Scripts.SO;

namespace Tyu.Scripts.Boss.FSM
{
    public class BossStateMachine : AbstractStateMachine
    {
        
        
        public BossStateMachine(IStateMachine _owner) : base(_owner)
        {
            
        }

        private DealTimeState _dealTimeState;
        private bool _locked;
        
        public void LockState()
        {
            _locked = true;
        }

        public void UnlockState()
        {
            _locked = false;
        }

        public override void ChangeState(StateEnum keyValue)
        {
            if (_locked) return;
            base.ChangeState(keyValue);
        }
        public void ChangeToDealTime(AttackEnum key, float dealTime)
        {
            currentState.Exit();
            currentState = _dealTimeState;
            _dealTimeState.StartDealTime(key, dealTime);
        }

        public void GetDealTimeState(DealTimeState state) => _dealTimeState = state;
    }
}