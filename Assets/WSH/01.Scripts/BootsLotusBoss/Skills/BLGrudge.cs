using System.Collections;
using Key._00.Scripts;
using Tyu.Scripts.Boss.FSM;
using Tyu.Scripts.Modules;
using UnityEngine;
using WSH._09.SO;

public class BLGrudge : ISkill // 장화의 원한
{
    [Header("StatSO")] private BossStatDataSO _bossStat;
    
    [Header("Stat")]
    [SerializeField] private float _damage; // 공격력

    [Header("Time")]
    [SerializeField] private float _noticeTime; // 예고 시간
    [SerializeField] private float _dealTime; // 공격 받을 시간
    [SerializeField] private float _enterTime; // 우물 들어가는 시간
    
    [Header("Range")]
    [SerializeField] private float _angleRange; // 각도 범위
    [SerializeField] private float _radiusRange; // 각도 길이
    [SerializeField] private float _segments; // 범위 분할 개수

    [SerializeField] private bool _attacking; // 공격 상태

    [SerializeField] private LayerMask _whatIsPlayer;

    private void EnterState()
    {
        
    }
    
    private void UpdateState()
    {
        
    }

    void ExitState()
    {
        
    }

    protected void Attack()
    {
        Collider2D[] target = Physics2D.OverlapCircleAll(transform.position, _radiusRange, _whatIsPlayer);

        foreach (var player in target)
        {
            Vector2 direction = player.transform.position - transform.position; // 플레이어와 보스의 거리
            float angle = Vector2.Angle(transform.right, direction); // 방향 구하기

            if (angle <= _angleRange / 2f)
            {
                Debug.Log(1);
                
                if (player.TryGetComponent<HealthModule>(out HealthModule health))
                {
                    //health.GetDamage(_damage);
                    StartCoroutine(DealTime());
                }
            }
            else
            {
                Debug.Log(0);
            }
        }
    }

    IEnumerator NoticeTime()
    {
        yield return new WaitForSeconds(_noticeTime);
        Attack();
    }
    
    IEnumerator DealTime()
    {
        yield return new WaitForSeconds(_dealTime);
    }

    public void GetBossStat(BossStatDataSO bossStat)
    {
        _bossStat = bossStat;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position;
        Vector3 previousPoint = center;

        // 캐릭터가 바라보는 방향
        float forwardAngle = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg; // 플레이어가 바라보는 방향

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
