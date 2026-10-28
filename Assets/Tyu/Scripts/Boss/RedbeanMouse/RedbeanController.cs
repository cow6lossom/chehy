using Tyu.Scripts.Boss.FSM;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanController : AbstractBoss, IStateMachine
    {
        private CanLockStateMachine _behaviourStateMachine;
        
        
        
        
        public override void Initialize(Transform _playerTrm)
        {
            base.Initialize(_playerTrm);
            _behaviourStateMachine = new CanLockStateMachine(this);
            
        }

        public void StateTrigger()
        {
            
        }
    }
}