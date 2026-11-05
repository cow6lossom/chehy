namespace Tyu.Scripts.Boss.FSM
{
    public class CanLockStateMachine : AbstractStateMachine
    {
        public CanLockStateMachine(IStateMachine _owner) : base(_owner)
        {
        }

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
    }
}