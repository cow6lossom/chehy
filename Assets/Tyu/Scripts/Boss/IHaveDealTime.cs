namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public interface IHaveDealTime
    {
        void ResetLastAttack(float dealTime);
        bool IsDealTime();
    }
}