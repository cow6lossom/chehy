using UnityEngine;

public enum PassiveGrade
{
    Normal,
    Boss
}

public enum PassiveType
{
    Attack,
    Buff,
    Control,
    Special
}

public enum ActivationConditions
{
    Always,         // 항상 적용
    CollDown,       // 쿨타임
    OnHit,          // 공격 적중 시
    OnBeingHit,     // 피격 시
    OnKill,         // 적 처치 시
    OnLowHealth,    // 체력이 일정 이하일 때
    OnFullHealth,   // 체력이 최대일 때
    OnDash,         // 대시할 때
    OnMove,         // 이동할 때
}

[CreateAssetMenu(fileName = "PassiveData", menuName = "Passive/PassiveData")]
public class PassiveData : ScriptableObject //패시브 자체의 정보
{
    public string PassiveName;
    public string Description;
    public Sprite Icon;

    public PassiveGrade Grade;
    public PassiveType Type;

    public float Duration;
    public float Range;
}