using UnityEngine;

namespace Tyu.Scripts.SO
{
    [CreateAssetMenu(fileName = "HealthSetting", menuName = "DataSetting/Health", order = 0)]
    public class StatementSettingSO : ScriptableObject
    {

        [SerializeField] private string ownerName;
        [field: SerializeField] public float defaultHealth { get; private set; }
        [field:SerializeField, Range(0,100)] public int secondPhaseHealth { get; private set; } //2 페이즈 조건, 퍼센티지로
        
        
        [Header("First Phase")] //1페이즈란 뜻 ㅇㅇ
        [field:SerializeField] public float FirSpeed { get; private set; }
        [field:SerializeField] public float FirDistance { get; private set; }
        
        [Header("Second Phase")] //2페이즈란 뜻 ㅇㅇ
        [field:SerializeField] public float SecSpeed { get; private set; }
        [field:SerializeField] public float SecDistance { get; private set;}
        
        
    }
}