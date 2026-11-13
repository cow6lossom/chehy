namespace Tyu.Scripts.Modules
{
    public interface IHealthHavable : IDamageable
    {
        public float GetCurrentHp();
        public float GetMaxHp();
        public void HealHp(float value);
    }
}