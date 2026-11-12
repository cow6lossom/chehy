using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using Unity.VisualScripting;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseIdle : MovementState
    {
        private RedbeanController _owner;
        private BossStateMachine stateMachine;
        private IHaveDealTime _dealTime;
        public RedbeanMouseIdle(AbstractStateMachine _stateMachine, string anim, 
            RenderModule _renderMd, RedbeanController owner) 
            : base(_stateMachine, anim,_renderMd)
        {
            _owner = owner;
            stateMachine = base.stateMachine as BossStateMachine;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            stateMachine.ChangeState(StateEnum.Chase);
        }
    }
}