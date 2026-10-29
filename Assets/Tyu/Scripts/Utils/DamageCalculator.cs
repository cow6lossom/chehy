using Tyu.Scripts.Results;
using Tyu.Scripts.SO;

namespace Tyu.Scripts.Utils
{
    public static class DamageCalculator
    {
        public static float Calculate(AttackResult attackData, StatementSettingSO statData)
        {
            float resultDamage = attackData.resultDamage;
            resultDamage -= resultDamage * (statData.damageReducePercentage / 100);
            resultDamage -= statData.damageReduceNum;
            return resultDamage;
        }
    }
}