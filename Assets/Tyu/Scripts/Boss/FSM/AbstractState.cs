namespace Tyu.Scripts.Boss.FSM
{
    public abstract class AbstractState
    {

        protected AbstractStateMachine stateMachine;
        
        
        
        public AbstractState(AbstractStateMachine _stateMachine)
        {
            stateMachine = _stateMachine;
        }
        
        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }

        public virtual void UpdateState()
        {
            
        }

        public virtual void AnimationCallback()
        {
            
        }
    }
}