using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Unity.VisualScripting;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseIdle : MovementState
    {
        private RedbeanController _owner;
        private CanLockStateMachine stateMachine;
        public RedbeanMouseIdle(AbstractStateMachine _stateMachine, string anim, MovementModule _moveMD, RenderModule _renderMd, RedbeanController owner) : base(_stateMachine, anim, _moveMD, _renderMd)
        {
            _owner = owner;
            stateMachine = base.stateMachine as CanLockStateMachine;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            
        }
    }
}