using System;
using Tyu.Modules;
using Tyu.Scripts;
using Tyu.Scripts.Modules;
using Tyu.Scripts.Results;
using Tyu.Scripts.SO;
using UnityEngine;

public abstract class AbstractBoss : MonoBehaviour, IHealthHavable
{
    //아마도 필요한거
    //private MovementModule moveMd
    [SerializeField] private AttackModule attackMd;
    [SerializeField] private HealthModule healthMd;
    [SerializeField] private StatementSettingSO bossStat;
    private Transform _targetPlayer;
    // rigidbody 필요한건 무브먼트 모듈에서 쓰면 되서 이닛 할 때 GetComponent로 넣어주면 될듯

    
    
    private void Awake()
    {
    }

    public virtual void Initialize(Transform _playerTrm)
    {
        _targetPlayer = _playerTrm;
        
    }

    public void GetDamage(AttackResult data) => healthMd.GetDamage(data);
}
