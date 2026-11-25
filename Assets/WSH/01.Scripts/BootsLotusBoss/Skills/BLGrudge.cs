using System.Collections;
using Tyu.Scripts.Modules;
using UnityEngine;
using WSH._01.Scripts.BootsLotusBoss;
using WSH._09.SO;

public class BLGrudge : MonoBehaviour // 장화의 원한
{
    [Header("StatSO")] private BossStatDataSO _bossStat;

    [SerializeField] private BossAnimation _animation;
    [SerializeField] private Transform _myself;
    [SerializeField] private Transform _playerTrm;

    [Header("Stat")]
    [SerializeField] private float _damage; // 공격력

    [Header("Time")]
    [SerializeField] private float _noticeTime; // 예고 시간
    [SerializeField] private float _dealTime; // 공격 받을 시간
    [SerializeField] private float _enterTime; // 우물 들어가는 시간
    [SerializeField] private float _lastPotectTime;
    [SerializeField] private float _coolPotectTime;

    [Header("Range")]
    [SerializeField] private float _angleRange; // 각도 범위 - 개별
    [SerializeField] private float _radiusRange; // 각도 길이 - 개별
    [SerializeField] private float _segments; // 범위 분할 개수 - 개별

    [SerializeField] private bool _attacking; // 공격 상태
    [SerializeField] private bool _targeting;

    [SerializeField] private LayerMask _whatIsPlayer;
    
    void SettingValue()
    {
        _damage = _bossStat.Damage;
        _noticeTime = _bossStat.NoticeTime;
        _dealTime = _bossStat.DealTime;
        _enterTime = _bossStat.EnterTime;
        _radiusRange = _bossStat.Range;

        _whatIsPlayer = _bossStat.WhatIsPlayer;
    }

    private void Update()
    {
        if (_targeting)
        {
            Vector2 dir = (_playerTrm.position - _myself.position).normalized;
            float playerAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            _myself.rotation = Quaternion.Euler(0f, 0f, playerAngle);
        }
    }

    void Attack()
    { 
        Collider2D[] target = Physics2D.OverlapCircleAll(_myself.position, _radiusRange, _whatIsPlayer);

        foreach (var player in target)
        {
            Vector2 direction = player.transform.position - _myself.position;
            float angle = Vector2.Angle(_myself.right, direction); // 방향 구하기

            if (angle <= _angleRange / 2f)
            {
                if (player.TryGetComponent<HealthModule>(out HealthModule health))
                {
                    health.GetDamage(_damage);
                }
            }
        }
        StartCoroutine(DealTime());
    }

    public IEnumerator NoticeTime()
    {
        Debug.Log(0);
        _targeting = true;
        yield return new WaitForSeconds(_noticeTime);
        Attack();
    }

    IEnumerator DealTime()
    {
        yield return new WaitForSeconds(_dealTime);
        yield return new WaitForSeconds(_enterTime);

        if (_lastPotectTime + _coolPotectTime < Time.time)
        {
            yield return new WaitForSeconds(_enterTime);
            _lastPotectTime = Time.time;
            StartCoroutine(transform.parent.GetComponent<BootsLotusAttack>().dontTouch.NoticeTime());
        }
        else
            StartCoroutine(transform.parent.GetComponent<BootsLotusAttack>().NextSkill());
        
        _targeting = false;
    }

    public void GetBossStat(BossStatDataSO bossStat)
    {
        _bossStat = bossStat;
        SettingValue();
    }

    public void Dead()
    {
        StopCoroutine(NoticeTime());
        StopCoroutine(DealTime());
    }
    
    private void OnDrawGizmos()
    {
        if (_bossStat == null) return;
        
        Gizmos.color = Color.red;

        Vector3 center = _myself.position;
        Vector3 previousPoint = center;
        
        // 캐릭터가 바라보는 방향
        float forwardAngle = Mathf.Atan2(_myself.right.y, _myself.right.x) * Mathf.Rad2Deg; // 플레이어가 바라보는 방향

        float startAngle = forwardAngle - _angleRange / 2f; // 부채꼴이 시작하는 각도

        for (int i = 0; i <= _segments; i++) // 
        {
            float currentAngle = startAngle + (_angleRange / _segments) * i; // 현재 각도를 초기 각도와 i만큼 더함

            float radian = currentAngle * Mathf.Deg2Rad; // 현재 각도를 라디안으로 변환

            Vector3 direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f); // 방향 설정

            Vector3 currentPoint = center + direction * _radiusRange; // 중앙 + 반지름(+방향)

            Gizmos.DrawLine(center, currentPoint); // 중앙에 그리기

            if (i > 0)
                Gizmos.DrawLine(previousPoint, currentPoint); // 채우기

            previousPoint = currentPoint; // 이전 점 좌표를 현재 좌표로 채우기
        }
    }
}
