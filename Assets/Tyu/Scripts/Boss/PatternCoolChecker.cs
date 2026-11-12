using System.Collections.Generic;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss
{
    public class PatternCoolTimeChecker
    {
        public Dictionary<StateEnum, PatternCoolTimer> dict;

        public void AddCoolTimer(StateEnum type, AttackSettingSO data)
        {
            PatternCoolTimer timer = new(data);
            dict.Add(type, timer);
        }
        public bool CheckCoolTime(StateEnum type) => dict[type].CheckCoolTime();
        public void ResetLastAtk(StateEnum type) => dict[type].SetLastAtk();
        
    }

    public class PatternCoolTimer
    {
        private float _lastAtkTime;
        private float _coolTime;
        
        public PatternCoolTimer(AttackSettingSO setting)
        {
            _lastAtkTime = 0f;
            _coolTime = setting.CoolTime;
        }
        
        public bool CheckCoolTime()
        {
            return _lastAtkTime + _coolTime < Time.time;
        }
        
        public void SetLastAtk()
        {
            _lastAtkTime = Time.time;
        }
        
        
        
    }
}