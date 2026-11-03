namespace Tyu.Scripts
{
    public interface IStateMachine
    {
        void EndTrigger();
        void CallBackTrigger();
    }
}