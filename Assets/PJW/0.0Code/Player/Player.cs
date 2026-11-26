using System;
using Tyu.Scripts;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Tyu.Modules;
using Tyu.Scripts.SO;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;

    public Input InputCompo {  get; private set; }

    public PlayerMovment MoveCompo { get; private set; }

    public PlayerAttack AttackCompo { get; private set; }

    public PlayerSkill SkillCompp { get; private set; }


    private void Awake()
    {
        InputCompo = GetComponentInChildren<Input>();

        MoveCompo = GetComponentInChildren<PlayerMovment>();

        AttackCompo = GetComponentInChildren<PlayerAttack>();

        SkillCompp = GetComponentInChildren<PlayerSkill>();
    }


    private void Start()
    {
        InputCompo.OnMoveSet += MoveCompo.SetMoveDir;

        InputCompo.OnAttackPressed += AttackCompo.Attack;
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        InputCompo.OnMoveSet -= MoveCompo.SetMoveDir;

        InputCompo.OnAttackPressed -= AttackCompo.Attack;
    }
}
