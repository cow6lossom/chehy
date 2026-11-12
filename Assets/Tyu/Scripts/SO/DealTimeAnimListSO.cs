using System;
using System.Collections.Generic;
using Tyu.Scripts.Boss.FSM;
using UnityEditor.Animations;
using UnityEngine;

namespace Tyu.Scripts.SO
{
    [CreateAssetMenu(fileName = "DealTimeAnimList", menuName = "DataSetting/Animation/DealTimeList", order = 0)]
    public class DealTimeAnimListSO : ScriptableObject
    {
        [field: SerializeField] public List<DealTimeAnim> DealTimeAnims { get; private set; }
    }

    
    [Serializable]
    public struct DealTimeAnim
    {
        [field: SerializeField] public AttackEnum Type { get; private set; }
        [field: SerializeField] public string dealTimeAnim { get; private set; }
    }
}