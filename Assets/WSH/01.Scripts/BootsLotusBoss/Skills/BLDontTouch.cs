using System.Collections;
using UnityEngine;
using WSH._01.Scripts.BootsLotusBoss;
using WSH._09.SO;

public class BLDontTouch : MonoBehaviour
{
    [Header("StatSO")] private BossStatDataSO _bossStat;
    
    [Header("Time")]
    [SerializeField] private float _noticeTime; // 예고 시간
    [SerializeField] private float _protectTime; // 공격 받을 시간
    [SerializeField] private float _enterTime; // 우물 들어가는 시간
    
    void SettingValue()
    {
        _noticeTime = _bossStat.NoticeTime;
        _protectTime = _bossStat.DealTime;
    }
    
    public IEnumerator NoticeTIme()
    {
        Debug.Log(3);
        yield return new WaitForSeconds(_noticeTime);
        yield return new WaitForSeconds(_protectTime);
        yield return new WaitForSeconds(_enterTime);
        transform.parent.GetComponent<BootsLotusAttack>().SkillWork();
        
    }
    
    public void GetBossStat(BossStatDataSO bossStat)
    {
        _bossStat = bossStat;
        SettingValue();
    }
}
