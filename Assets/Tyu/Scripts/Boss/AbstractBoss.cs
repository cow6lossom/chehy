using System;
using UnityEngine;

public abstract class AbstractBoss : MonoBehaviour
{
    //아마도 필요한거
    //private MovementModule moveMd
    //private AttackModule attackMd
    //private HealthModule healthMd
    //

    // rigidbody 필요한건 무브먼트 모듈에서 쓰면 되서 이닛 할 때 GetComponent로 넣어주면 될듯

    private void Awake()
    {
        //moveMd = GetComponentInChildren<MovementModule> moveMd;
        //attackMd = GetComponentInChildren<AttackModule> attackMd;
        //healthMd = GetComponentInChildren<HealthModule> healthMd;
        
    }

    private void Initialize()
    {
        
    }
    
}
