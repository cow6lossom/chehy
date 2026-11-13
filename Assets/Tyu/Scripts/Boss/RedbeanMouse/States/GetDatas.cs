using System;
using System.Collections.Generic;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.SO;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public static class GetDatas
    {
        public static void GetAttackData(List<AttackSettingSO> list, AttackEnum target, ref AttackSettingSO owner)
        {
            foreach (var data in list)
            {
                if (data.AttackType == target)
                {
                    owner = data;
                    return;
                }
            }
        }
        public static void GetMoveData(List<AttackMoveSO> list, AttackEnum target, ref AttackMoveSO owner)
        {
            foreach (var data in list)
            {
                if (data.Type == target)
                {
                    owner = data;
                    return;
                }
            }
        }
    }
}