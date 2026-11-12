using System.Collections.Generic;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss
{
    public class GetNoneCoolAttackModule
    {
        public GetNoneCoolAttackModule(PatternCoolTimeChecker coolTimeChecker)
        {
            _coolTimeChecker = coolTimeChecker;
        }

        private PatternCoolTimeChecker _coolTimeChecker;
        
        public bool TryGetAttackData(float distance, List<AttackSettingSO> list, out AttackSettingSO result)
        {
            result = null;
            foreach (var attack in list)
            {
                if(!attack.IsSymbol) continue;
                if (attack.HaveRandomPatterns)
                    if (GetRandomPattern(attack, distance,out var data))
                    {
                        result = data;
                        return true;
                    }
                if (!_coolTimeChecker.CheckCoolTime(attack.Type)) continue;
                if (distance < attack.minDetect) continue;
                if (distance > attack.maxDetect) continue;
                result = attack;
                return true;
            }
            return false;
        }
        
        
        private bool GetRandomPattern(AttackSettingSO data, float distance,out AttackSettingSO result)
        {
            result = null;
            if (distance < data.minDetect || distance > data.maxDetect) return false;
            var list = data.RandomPatterns;
            for (int i = 0; i < list.Count; ++i)
            {
                int idx = Random.Range(0, list.Count);
                var target = list[idx];
                if (_coolTimeChecker.CheckCoolTime(target.Type))
                {
                    list.RemoveAt(idx);
                    continue;
                }
                result = target;
                return true;
            }
            return false;
        }
    }
}