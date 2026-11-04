using Tyu.Modules;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseFlowerWay : AttackState
    {
        public RedbeanMouseFlowerWay(AbstractStateMachine _stateMachine, string anim, AttackModule attackMd, RenderModule renderMd, PatternCoolTimeChecker coolChecker) : base(_stateMachine, anim, attackMd, renderMd, coolChecker)
        {
        }
    }
}