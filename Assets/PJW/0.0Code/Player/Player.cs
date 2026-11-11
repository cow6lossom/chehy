using System;
using Tyu.Scripts;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IHealthHavable
{
    [SerializeField] private float health = 100f;

    public Input InputCompo {  get; private set; }

    public PlayerMovment MoveCompo { get; private set; }

    private void Awake()
    {
        InputCompo = GetComponentInChildren<Input>();

        MoveCompo = GetComponentInChildren<PlayerMovment>();
    }

    private void Start()
    {
        InputCompo.OnMoveSet += MoveCompo.SetMoveDir;
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
    }
}
