using UnityEngine;

namespace WSH._09.SO
{
    [CreateAssetMenu(fileName = "BossStatDataSO", menuName = "SO/BossStatDataSO")]
    public class BossStatDataSO : ScriptableObject
    {
        [Header("Stat")]
        [field:SerializeField] public float Damage { get; private set; } // 공격력
        [field:SerializeField]public float Range { get; private set; }  // 범위 - 원형 기준

        [Header("Time")]
        [field:SerializeField]public float NoticeTime { get; private set; }// 예고 시간
        [field:SerializeField]public float DealTime { get; private set; }// 공격 받을 시간
        [field:SerializeField]public float EnterTime { get; private set; } // 우물 들어가는 시간
    
        [field:SerializeField] public LayerMask WhatIsPlayer { get; private set; } // 플레이어 레이어
    }
}
