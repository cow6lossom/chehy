using System.Collections;
using UnityEngine;
using WSH._09.SO;

public class BLDontTouch : MonoBehaviour
{
    [Header("StatSO")] private BossStatDataSO _bossStat;
    
    [Header("Time")]
    [SerializeField] private float _noticeTime; // 예고 시간
    [SerializeField] private float _resistanceTime; // 공격 받을 시간
    [SerializeField] private float _enterTime; // 우물 들어가는 시간

    IEnumerator NoticeTIme()
    {
        yield return new WaitForSeconds(_noticeTime);
    }

    IEnumerator ResistanceTime()
    {
        yield return new WaitForSeconds(_resistanceTime);
    }
    
    public void GetBossStat(BossStatDataSO bossStat)
    {
        _bossStat = bossStat;
    }

}
