using Tyu.Scripts.SO;

namespace Tyu.Scripts.Results
{
    public class AttackResult
    {
        private float _defaultDamage;
        //private PassiveEnums _enabledPassives;
        public AttackSettingSO myData { get; private set; }

        public float resultDamage
        {
            get;
            private set;
        }
        
        
        public AttackResult(AttackSettingSO settingData /*PassiveEnums enabledPassive*/)
        {
            _defaultDamage = settingData.DefaultDamage;
            //_enabledPassives && enabledPassive;
            //
            //
            myData = settingData;
            resultDamage = myData.DefaultDamage;
        }
        
    }
}