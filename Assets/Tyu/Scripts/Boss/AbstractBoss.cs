using System;
using Tyu.Modules;
using Tyu.Scripts;
using Tyu.Scripts.Boss.RedbeanMouse;
using Tyu.Scripts.Modules;
using Tyu.Scripts.Results;
using Tyu.Scripts.SO;
using UnityEngine;

public abstract class AbstractBoss : MonoBehaviour, IHealthHavable
{
    //아마도 필요한거
    //private MovementModule moveMd
    [field : SerializeField] public AttackModule attackMd{get;private set;}
    [field : SerializeField] public BossHealthModule healthMd{get;private set;}
    [field : SerializeField] public StatementSettingSO bossStat{get;private set;}
    [field : SerializeField] public RenderModule renderMd{get;private set;}
    [field : SerializeField] public BossMoveModule moveMd{get;private set;}
    
    public Transform TargetPlayer { get; private set; }
    // rigidbody 필요한건 무브먼트 모듈에서 쓰면 되서 이닛 할 때 GetComponent로 넣어주면 될듯

    
    
    private void Awake()
    {
    }

    public virtual void Initialize(Transform _playerTrm)
    {
        TargetPlayer = _playerTrm;
        
    }

    public void GetDamage(float damage) => healthMd.GetDamage(damage);
}
